namespace Portfolio.Book;

using System.Net;
using System.Text.Json;
using Infrastructure;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

public class BookFunction
{
    private readonly ILogger _logger;
    private readonly IBookService _bookService;

    public BookFunction(ILoggerFactory loggerFactory, IBookService bookService)
    {
        _logger = loggerFactory.CreateLogger<BookFunction>();
        _bookService = bookService;
    }

    [Function("Books")]
    public async Task<HttpResponseData> GetBooks([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var bookList = await _bookService.GetBooks();

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(bookList);

        return response;
    }

    [Function("AddBook")]
    public async Task<HttpResponseData> AddBook([HttpTrigger(AuthorizationLevel.Function, "post", Route = "book")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        try
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new StrictStringConverter());

            var addBook = await JsonSerializer.DeserializeAsync<AddBookDto>(req.Body, options);
            
            var addedBook = await _bookService.AddBook(addBook.title, addBook.author);

            var response = req.CreateResponse();
            await response.WriteAsJsonAsync(addedBook);
            response.StatusCode = HttpStatusCode.Created;

            return response;
        }
        catch (JsonException)
        {
            var badRequestResponse = req.CreateResponse(HttpStatusCode.BadRequest);
            badRequestResponse.WriteString($"Fields '{nameof(AddBookDto.title)}' and '{nameof(AddBookDto.author)}' are required");
            return badRequestResponse;
        }
    }

    [Function("DeleteBook")]
    public async Task<HttpResponseData> DeleteBook([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "book")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        DeleteBookDto deleteBook;
        try
        {
            deleteBook = await JsonSerializer.DeserializeAsync<DeleteBookDto>(req.Body);

        }
        catch (Exception ex)
        {
            var badRequestResponse = req.CreateResponse(HttpStatusCode.BadRequest);
            badRequestResponse.WriteString("Invalid ID given");
            return badRequestResponse;
        }

        var resultStatus = await _bookService.DeleteBook(deleteBook.id);

        var response = req.CreateResponse(resultStatus);
        return response;
    }
}
