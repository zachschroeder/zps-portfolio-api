using Moq;
using Portfolio.Book;

namespace Portfolio.Test.Book;

public class BookServiceTests
{
    private readonly BookService _bookService;
    private readonly Mock<IBookContainer> _mockBookContainer;

    private readonly List<Portfolio.Book.Book> _mockBooks = new()
    {
        new Portfolio.Book.Book(Guid.NewGuid(), "Green Eggs and Ham", "Dr. Seuss"),
        new Portfolio.Book.Book(Guid.NewGuid(), "The Hobbit", "J.R.R Tolkien")
    };

    public BookServiceTests()
    {
        this._mockBookContainer = new Mock<IBookContainer>();
        this._bookService = new BookService(_mockBookContainer.Object);
    }

    [Fact]
    public async Task GetBooksShouldReturnBooks()
    {
        // Arrange


        // Act
        var books = this._bookService.GetBooks();

        // Assert
        Assert.NotNull(books);
    }

    [Fact]
    public async Task AddBookShouldReturnBook()
    {
        // Arrange


        // Act
        var books = this._bookService.AddBook(_mockBooks[0].title, _mockBooks[0].author);

        // Assert
        Assert.NotNull(books);
    }
}
