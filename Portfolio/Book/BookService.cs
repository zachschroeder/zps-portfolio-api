﻿namespace Portfolio.Book;

using Microsoft.Azure.Cosmos;

public class BookService : IBookService
{
    private CosmosClient client;
    private Database database;
    private Container container;

    public BookService()
    {
        var EndpointUri = Environment.GetEnvironmentVariable("CosmosEndpoint");
        var PrimaryKey = Environment.GetEnvironmentVariable("CosmosPrimaryKey");

        this.client = new CosmosClient(EndpointUri, PrimaryKey);
        this.database = client.GetDatabase("basic-db");
        this.container = database.GetContainer("books");
    }

    public async Task<List<Book>> GetBooks()
    {
        var iterator = this.container.GetItemQueryIterator<Book>();
        List<Book> bookList = new();

        while (iterator.HasMoreResults)
            foreach (var item in await iterator.ReadNextAsync().ConfigureAwait(false))
                bookList.Add(item);

        return bookList;
    }

    public async Task<Book> AddBook(string title, string author)
    {
        Book bookToCreate = new(Guid.NewGuid(), title, author);

        await this.container.CreateItemAsync(bookToCreate);
        
        return bookToCreate;
    }
}