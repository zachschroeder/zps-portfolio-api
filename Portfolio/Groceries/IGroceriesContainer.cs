namespace Portfolio.Groceries;

using Microsoft.Azure.Cosmos;

public interface IGroceriesContainer
{
    public Task<ItemResponse<GroceryItem>> CreateItemAsync(GroceryItem groceryItem);
    public Task<ItemResponse<GroceryItem>> DeleteItemAsync(Guid id);
    public FeedIterator<GroceryItem> GetItemQueryIterator();
}