namespace Portfolio.Book;

using System.Net;

public interface IBookService
{
    public Task<List<Book>> GetBooks();
    public Task<Book> AddBook(string title, string author);
    public Task<HttpStatusCode> DeleteBook(Guid id);
}
