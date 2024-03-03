using Microsoft.Azure.Cosmos;

namespace Portfolio.Book;

public class BookContainer : IBookContainer
{
    private readonly CosmosClient _client;
    private readonly Database _database;
    private readonly Container _container;

    public BookContainer()
    {
        var EndpointUri = Environment.GetEnvironmentVariable("CosmosEndpoint");
        var PrimaryKey = Environment.GetEnvironmentVariable("CosmosPrimaryKey");

        _client = new CosmosClient(EndpointUri, PrimaryKey);
        _database = _client.GetDatabase("basic-db");
        _container = _database.GetContainer("books");
    }

    public Task<ItemResponse<Book>> CreateItemAsync(Book book)
    {
        return _container.CreateItemAsync<Book>(book);
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
