namespace Portfolio.Basic;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

public class BasicFunction
{
    private readonly ILogger _logger;
    private readonly BookService _bookService;

    public BasicFunction(ILoggerFactory loggerFactory, BookService bookService)
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
}
