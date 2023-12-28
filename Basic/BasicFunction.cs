namespace Portfolio.Basic;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

public class BasicFunction
{
    private readonly ILogger _logger;
    private readonly IBookService _bookService;

    public BasicFunction(ILoggerFactory loggerFactory, IBookService bookService)
    {
        _logger = loggerFactory.CreateLogger<BasicFunction>();
        this._bookService = bookService;
    }

    [Function("Books")]
    public async Task<HttpResponseData> GetBooks([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var bookList = await this._bookService.GetBooks();

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(bookList);

        return response;
    }

    [Function("AddBook")]
    public async Task<HttpResponseData> AddBook([HttpTrigger(AuthorizationLevel.Function, "post", Route = "book")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        AddBookDto addBook = await JsonSerializer.DeserializeAsync<AddBookDto>(req.Body);

        if(string.IsNullOrEmpty(addBook.title) || string.IsNullOrEmpty(addBook.author))
        {
            var badRequestResponse = req.CreateResponse(HttpStatusCode.BadRequest);
            badRequestResponse.WriteString("Fields 'title' and 'author' must not be null or empty");
            return badRequestResponse;
        }

        Book addedBook = await this._bookService.AddBook(addBook.title, addBook.author);

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(addedBook);

        return response;
    }
}
