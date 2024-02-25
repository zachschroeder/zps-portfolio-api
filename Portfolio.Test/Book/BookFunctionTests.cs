namespace Portfolio.Test;

using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Newtonsoft.Json;
using Portfolio.Book;
using System.IO;
using System.Net;

public class BookFunctionTests
{
    private readonly BookFunction _bookFunction;
    private readonly Mock<IBookService> _mockBookService;

    private readonly List<Book> _mockBooks = new List<Book>()
    {
        new Book(Guid.NewGuid(), "Green Eggs and Ham", "Dr. Seuss"),
        new Book(Guid.NewGuid(), "The Hobbit", "J.R.R Tolkien")
    };

    public BookFunctionTests()
    {
        _mockBookService = new Mock<IBookService>();
        _bookFunction = new(new NullLoggerFactory(), _mockBookService.Object);
    }

    [Fact]
    public async Task GetBooksReturnsOkWithBooks()
    {
        // Arrange
        var request = TestHelpers.CreateRequest();
        this._mockBookService.Setup(s => s.GetBooks()).ReturnsAsync(_mockBooks);

        // Act
        var response = await _bookFunction.GetBooks(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task AddBookReturnsOkWithBookData()
    {
        // Arrange
        var addBook = new AddBookDto("Dune", "Frank Herbert");
        var request = TestHelpers.CreateRequest(addBook);

        // Act
        var response = await _bookFunction.AddBook(request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
       
        Book addedBook = TestHelpers.GetObjectFromStream<Book>(request.Body);
        Assert.Equal(addBook.title, addedBook.title);
    }

    [Fact]
    public async Task AddBookReturnsBadRequestForEmptyString()
    {
        // Arrange
        var addBook = new AddBookDto("ExampleTitle", "");
        var request = TestHelpers.CreateRequest(addBook);

        // Act
        var response = await _bookFunction.AddBook(request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}