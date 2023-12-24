namespace Portfolio.Basic;

public interface IBookService
{
    public Task<List<Book>> GetBooks();
    public Task<Book> CreateBook(string title, string author);
}
