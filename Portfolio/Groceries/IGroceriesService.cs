namespace Portfolio.Groceries;

using System.Net;

public interface IGroceriesService
{
    public Task<List<GroceryItem>> GetGroceries();
    public Task<GroceryItem> AddGroceryItem(AddGroceryItemDto addGroceryItem);
    public Task<HttpStatusCode> DeleteGroceryItem(Guid id);
}
