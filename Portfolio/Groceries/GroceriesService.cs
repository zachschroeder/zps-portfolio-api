namespace Portfolio.Groceries;

using System.Net;
using Microsoft.Azure.Cosmos;

public class GroceriesService(IGroceriesContainer groceriesContainer) : IGroceriesService
{
    public async Task<List<GroceryItem>> GetGroceries()
    {
        var iterator = groceriesContainer.GetItemQueryIterator();
        List<GroceryItem> groceries = [];

        while (iterator.HasMoreResults)
            foreach (var item in await iterator.ReadNextAsync().ConfigureAwait(false))
                groceries.Add(item);

        return groceries;
    }

    public async Task<GroceryItem> AddGroceryItem(AddGroceryItemDto addGroceryItem)
    {
        if (string.IsNullOrWhiteSpace(addGroceryItem.MealSection))
            addGroceryItem.MealSection = "Uncategorized";
        
        if (string.IsNullOrWhiteSpace(addGroceryItem.StoreSection))
            addGroceryItem.StoreSection = "Uncategorized";
        
        var groceryItem = new GroceryItem(Guid.NewGuid(), addGroceryItem.Name, false, addGroceryItem.MealSection,
            addGroceryItem.StoreSection);

        var addedGroceryItem = await groceriesContainer.CreateItemAsync(groceryItem);

        return addedGroceryItem;
    }

    public async Task<HttpStatusCode> DeleteGroceryItem(Guid id)
    {
        try
        {
            var response = await groceriesContainer.DeleteItemAsync(id);

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
}