namespace Portfolio.Groceries;

using System.Net;
using Infrastructure;
using Microsoft.Azure.Cosmos;

public class GroceriesService(IContainerRetriever containerRetriever, IGroceriesStateComposer stateComposer, IGroceriesCategorizer categorizer) : IGroceriesService
{
    private readonly Container _container = containerRetriever.GetContainer("groceries");

    public async Task<GroceriesState> GetGroceries()
    {
        var iterator = _container.GetItemQueryIterator<GroceryItem>();
        List<GroceryItem> groceries = [];

        while (iterator.HasMoreResults)
            foreach (var item in await iterator.ReadNextAsync().ConfigureAwait(false))
                groceries.Add(item);

        return stateComposer.ComposeState(groceries);
    }

    public async Task<GroceryItem> AddGroceryItem(AddGroceryItemDto addGroceryItem)
    {
        if (string.IsNullOrWhiteSpace(addGroceryItem.MealSection))
            addGroceryItem.MealSection = Sections.Uncategorized;
        
        if (string.IsNullOrWhiteSpace(addGroceryItem.StoreSection))
            addGroceryItem.StoreSection = categorizer.GetStoreSection(addGroceryItem.Name);
        
        var groceryItem = new GroceryItem(addGroceryItem.Id, addGroceryItem.Name, false, addGroceryItem.MealSection,
            addGroceryItem.StoreSection);

        var addedGroceryItem = await _container.CreateItemAsync(groceryItem);

        return addedGroceryItem;
    }

    public async Task<HttpStatusCode> DeleteGroceryItem(Guid id)
    {
        try
        {
            var response = await _container.DeleteItemAsync<GroceryItem>(id.ToString(),  new PartitionKey(id.ToString()));

            if (response.StatusCode == HttpStatusCode.NoContent)
                return HttpStatusCode.NoContent;

            return HttpStatusCode.InternalServerError;
        }
        catch (Exception ex)
        {
            if (ex is CosmosException { StatusCode: HttpStatusCode.NotFound })
                return HttpStatusCode.NotFound;

            return HttpStatusCode.InternalServerError;
        }
    }

    public async Task<HttpStatusCode> CheckGroceryItem(CheckGroceryItemDto checkGroceryItem)
    {
       var iterator = _container.GetItemQueryIterator<GroceryItem>();
       GroceryItem? groceryItemToUpdate = null;

       while (iterator.HasMoreResults)
           foreach (var item in await iterator.ReadNextAsync().ConfigureAwait(false))
               if (item.id == checkGroceryItem.Id)
               {
                   groceryItemToUpdate = item;
                   break;
               }

       if (groceryItemToUpdate == null)
           return HttpStatusCode.NotFound;

       groceryItemToUpdate.IsChecked = checkGroceryItem.IsChecked;
       await _container.UpsertItemAsync(groceryItemToUpdate);
       return HttpStatusCode.Accepted;
    }
}