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
        var mockFeedResponse = new Mock<FeedResponse<Book>>();
        mockFeedResponse.Setup(r => r.GetEnumerator()).Returns(_mockBooks.GetEnumerator());

        var mockFeedIterator = new Mock<FeedIterator<Book>>();
        mockFeedIterator.SetupSequence(r => r.HasMoreResults).Returns(true).Returns(false);
        mockFeedIterator.Setup(i => i.ReadNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockFeedResponse.Object);

        _mockBookContainer.Setup(c => c.GetItemQueryIterator())
            .Returns(mockFeedIterator.Object);

        // Act
        var books = await _bookService.GetBooks();

        // Assert
        Assert.Equal(_mockBooks.Count, books.Count);
        Assert.Equivalent(_mockBooks.Select(b => b.title), books.Select(b => b.title));
    }

    [Fact]
    public async Task AddBookShouldReturnBook()
    {
        // Arrange
        var mockResponse = new Mock<ItemResponse<Book>>();
        mockResponse.Setup(r => r.Resource).Returns(_mockBooks[0]);

        this._mockBookContainer.Setup(s => s.CreateItemAsync(It.IsAny<Book>()))
            .ReturnsAsync(mockResponse.Object);

        // Act
        var book = await this._bookService.AddBook(_mockBooks[0].title, _mockBooks[0].author);

        // Assert
        Assert.Equal(_mockBooks[0].title, book.title);
        Assert.Equal(_mockBooks[0].author, book.author);
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
