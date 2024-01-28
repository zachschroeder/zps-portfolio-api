namespace Portfolio.Test;

using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Portfolio.Book;
using System.Net;

public class BookFunctionTests
{
    BookFunction bookFunction;
    Mock<IBookService> bookService;

    public BookFunctionTests()
    {
        bookService = new Mock<IBookService>();
        bookFunction = new(new NullLoggerFactory(), bookService.Object);
    }

    [Fact]
    public async Task AddBookReturnsBadRequestForEmptyString()
    {
        var addBook = new AddBookDto("ExampleTitle", "");

        var request = TestHelpers.CreateRequest(addBook);

        var response = await bookFunction.AddBook(request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}