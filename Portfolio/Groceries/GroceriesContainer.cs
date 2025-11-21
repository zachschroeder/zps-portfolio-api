namespace Portfolio.Groceries;

using Microsoft.Azure.Cosmos;

public class GroceriesContainer : IGroceriesContainer
{
    private readonly Container _container;
    
    public GroceriesContainer()
    {
        var cosmosEndpoint = Environment.GetEnvironmentVariable("CosmosEndpoint");
        var cosmosPrimaryKey = Environment.GetEnvironmentVariable("CosmosPrimaryKey");

        var client = new CosmosClient(cosmosEndpoint, cosmosPrimaryKey);
        var database = client.GetDatabase("basic-db");
        _container = database.GetContainer("groceries");
    }
    
    public Task<ItemResponse<GroceryItem>> CreateItemAsync(GroceryItem groceryItem)
    {
        return _container.CreateItemAsync(groceryItem);
    }

    public Task<ItemResponse<GroceryItem>> DeleteItemAsync(Guid id)
    {
        return _container.DeleteItemAsync<GroceryItem>(id.ToString(), new PartitionKey(id.ToString()));
    }

    public FeedIterator<GroceryItem> GetItemQueryIterator()
    {
        return _container.GetItemQueryIterator<GroceryItem>();
    }
}