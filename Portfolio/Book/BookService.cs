namespace Portfolio.Book;

using Microsoft.Azure.Cosmos;
using System.Net;

public class BookService : IBookService
{
    private IBookContainer _bookContainer;

    public BookService(IBookContainer bookContainer)
    {
        this._bookContainer = bookContainer;
    }

    public async Task<List<Book>> GetBooks()
    {
        var iterator = this._bookContainer.GetItemQueryIterator();
        List<Book> bookList = new();

        while (iterator.HasMoreResults)
            foreach (var item in await iterator.ReadNextAsync().ConfigureAwait(false))
                bookList.Add(item);

        return bookList;
    }

    public async Task<Book> AddBook(string title, string author)
    {
        Book bookToCreate = new(Guid.NewGuid(), title, author);

        await this._bookContainer.CreateItemAsync(bookToCreate);

        return bookToCreate;
    }

    public async Task<HttpStatusCode> DeleteBook(Guid id)
    {
        try
        {
            var response = await this._bookContainer.DeleteItemAsync(id);

            if (response.StatusCode == HttpStatusCode.NoContent)
                return HttpStatusCode.NoContent;

            return HttpStatusCode.InternalServerError;
        }
        catch (Exception ex)
        {
            if (ex is CosmosException cosmosException && cosmosException.StatusCode.Equals(HttpStatusCode.NotFound))
                return HttpStatusCode.NotFound;

            return HttpStatusCode.InternalServerError;
        }
    }
}
