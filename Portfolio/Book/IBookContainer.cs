namespace Portfolio.Book;

using Microsoft.Azure.Cosmos;

public interface IBookContainer
{
    public Task<ItemResponse<Book>> CreateItemAsync(Book book);
    public Task<ItemResponse<Book>> DeleteItemAsync(Guid id);
    public FeedIterator<Book> GetItemQueryIterator();
}
