namespace Portfolio.Test.Book;

using Microsoft.Azure.Cosmos;
using Moq;
using Portfolio.Book;
using System.Net;

public class BookServiceTests
{
    private readonly BookService _bookService;
    private readonly Mock<IBookContainer> _mockBookContainer;

    private readonly List<Book> _mockBooks = new()
    {
        new Book(Guid.NewGuid(), "Green Eggs and Ham", "Dr. Seuss"),
        new Book(Guid.NewGuid(), "The Hobbit", "J.R.R Tolkien")
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

        // Assert
    }

    [Fact]
    public async Task AddBookShouldReturnBook()
    {
        // Arrange

        // Act

        // Assert
    }

    [Fact]
    public async Task DeleteBookShouldReturnNoContent()
    {
        // Arrange
        var mockResponse = new Mock<ItemResponse<Book>>();
        mockResponse.Setup(r => r.StatusCode).Returns(HttpStatusCode.NoContent);

        this._mockBookContainer.Setup(s => s.DeleteItemAsync(It.IsAny<Guid>()))
            .ReturnsAsync(mockResponse.Object);

        // Act
        var status = await this._bookService.DeleteBook(Guid.NewGuid());

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, status);
    }

    [Fact]
    public async Task DeleteBookShouldReturnsNotFoundOnCosmosException()
    {
        // Arrange
        this._mockBookContainer.Setup(s => s.DeleteItemAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new CosmosException("BookNotFound", HttpStatusCode.NotFound, 0, "0", 0));

        // Act
        var status = await this._bookService.DeleteBook(Guid.NewGuid());

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, status);
    }

    [Fact]
    public async Task DeleteBookShouldReturnsInternalServerErrorWhenStatusInternalServerError()
    {
        // Arrange
        var mockResponse = new Mock<ItemResponse<Book>>();
        mockResponse.Setup(r => r.StatusCode).Returns(HttpStatusCode.InternalServerError);

        this._mockBookContainer.Setup(s => s.DeleteItemAsync(It.IsAny<Guid>()))
            .ReturnsAsync(mockResponse.Object);

        // Act
        var status = await this._bookService.DeleteBook(Guid.NewGuid());

        // Assert
        Assert.Equal(HttpStatusCode.InternalServerError, status);
    }

    [Fact]
    public async Task DeleteBookShouldReturnsInternalServerErrorOnException()
    {
        // Arrange
        this._mockBookContainer.Setup(s => s.DeleteItemAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new Exception("GenericException"));

        // Act
        var status = await this._bookService.DeleteBook(Guid.NewGuid());

        // Assert
        Assert.Equal(HttpStatusCode.InternalServerError, status);
    }
}
