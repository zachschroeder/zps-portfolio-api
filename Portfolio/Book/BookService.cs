namespace Portfolio.Book;

using System.Net;
using Infrastructure;
using Microsoft.Azure.Cosmos;

public class BookService(IContainerService containerService) : IBookService
{
    private readonly Container _container = containerService.GetContainer("books");
    
    public async Task<List<Book>> GetBooks()
    {
        var iterator = _container.GetItemQueryIterator<Book>();
        List<Book> bookList = [];

        while (iterator.HasMoreResults)
            foreach (var item in await iterator.ReadNextAsync().ConfigureAwait(false))
                bookList.Add(item);

        return bookList;
    }

    public async Task<Book> AddBook(string title, string author)
    {
        Book bookToAdd = new(Guid.NewGuid(), title, author);

        var addedBook = await _container.CreateItemAsync(bookToAdd);

        return addedBook;
    }

    public async Task<HttpStatusCode> DeleteBook(Guid id)
    {
        try
        {
            var response = await _container.DeleteItemAsync<Book>(id.ToString(), new PartitionKey(id.ToString()));

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
