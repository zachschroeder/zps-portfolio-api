namespace Portfolio.Test;

using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Portfolio.Book;
using System.Net;

public class BookFunctionTests
{
    private readonly BookFunction _bookFunction;
    private readonly Mock<IBookService> _mockBookService;

    private readonly List<Portfolio.Book.Book> _mockBooks = new()
    {
        new Portfolio.Book.Book(Guid.NewGuid(), "Green Eggs and Ham", "Dr. Seuss"),
        new Portfolio.Book.Book(Guid.NewGuid(), "The Hobbit", "J.R.R Tolkien")
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

        this._mockBookService.Setup(s => s.AddBook(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new Portfolio.Book.Book(Guid.NewGuid(), addBook.title, addBook.author));

        // Act
        var response = await _bookFunction.AddBook(request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var addedBook = TestHelpers.GetObjectFromStream<Portfolio.Book.Book>(response.Body);
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

    [Theory]
    [InlineData(HttpStatusCode.NoContent)]
    [InlineData(HttpStatusCode.NotFound)]
    public async Task DeleteBookReturnsStatusFromService(HttpStatusCode statusCode)
    {
        // Arrange
        var deleteBook = new DeleteBookDto(Guid.NewGuid());
        var request = TestHelpers.CreateRequest(deleteBook);

        this._mockBookService.Setup(s => s.DeleteBook(It.IsAny<Guid>()))
            .ReturnsAsync(statusCode);

        // Act
        var response = await _bookFunction.DeleteBook(request);

        // Assert
        Assert.Equal(statusCode, response.StatusCode);
    }

    [Fact]
    public async Task DeleteBookReturnsBadRequestForInvalidRequestData()
    {
        // Arrange
        var request = TestHelpers.CreateRequest("InvalidDataDummyString");

        // Act
        var response = await _bookFunction.DeleteBook(request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}