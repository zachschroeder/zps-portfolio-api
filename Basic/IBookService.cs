namespace Portfolio.Basic;

public interface IBookService
{
    public Task<List<Book>> GetBooks();
    public Task<Book> AddBook(string title, string author);
}
