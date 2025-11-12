namespace Portfolio.Book;

using System.Net;
using Microsoft.Azure.Cosmos;

public class BookService(IBookContainer bookContainer) : IBookService
{
    public async Task<List<Book>> GetBooks()
    {
        var iterator = bookContainer.GetItemQueryIterator();
        List<Book> bookList = [];

        while (iterator.HasMoreResults)
            foreach (var item in await iterator.ReadNextAsync().ConfigureAwait(false))
                bookList.Add(item);

        return bookList;
    }

    public async Task<Book> AddBook(string title, string author)
    {
        Book bookToAdd = new(Guid.NewGuid(), title, author);

        var addedBook = await bookContainer.CreateItemAsync(bookToAdd);

        return addedBook;
    }

    public async Task<HttpStatusCode> DeleteBook(Guid id)
    {
        try
        {
            var response = await bookContainer.DeleteItemAsync(id);

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
