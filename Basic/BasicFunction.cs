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

    [Function("CreateBook")]
    public async Task<HttpResponseData> CreateBook([HttpTrigger(AuthorizationLevel.Function, "post", Route = "book")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        CreateBookDto createBook = await JsonSerializer.DeserializeAsync<CreateBookDto>(req.Body);

        Book createdBook = await this._bookService.CreateBook(createBook.title, createBook.author);

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.WriteString(createdBook.ToString());

        return response;
    }
}
