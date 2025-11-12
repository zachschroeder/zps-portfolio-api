using Microsoft.Azure.Cosmos;

namespace Portfolio.Book;

public class BookContainer : IBookContainer
{
    private readonly Container _container;

    public BookContainer()
    {
        var cosmosEndpoint = Environment.GetEnvironmentVariable("CosmosEndpoint");
        var cosmosPrimaryKey = Environment.GetEnvironmentVariable("CosmosPrimaryKey");

        var client = new CosmosClient(cosmosEndpoint, cosmosPrimaryKey);
        var database = client.GetDatabase("basic-db");
        _container = database.GetContainer("books");
    }

    public Task<ItemResponse<Book>> CreateItemAsync(Book book)
    {
        return _container.CreateItemAsync(book);
    }

    public Task<ItemResponse<Book>> DeleteItemAsync(Guid id)
    {
        return _container.DeleteItemAsync<Book>(id.ToString(), new PartitionKey(id.ToString()));
    }

    public FeedIterator<Book> GetItemQueryIterator()
    {
        return _container.GetItemQueryIterator<Book>();
    }
}
