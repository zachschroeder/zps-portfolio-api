namespace Portfolio.Basic;

using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

public class BasicFunction
{
    private readonly ILogger _logger;

    public BasicFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<BasicFunction>();
    }

    [Function("Books")]
    public async Task<HttpResponseData> GetBooks([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var EndpointUri = Environment.GetEnvironmentVariable("CosmosEndpoint");
        var PrimaryKey = Environment.GetEnvironmentVariable("CosmosPrimaryKey");

        CosmosClient client = new CosmosClient(EndpointUri, PrimaryKey);
        Database database = client.GetDatabase("basic-db");
        Container container = database.GetContainer("books");

        var iterator = container.GetItemQueryIterator<Book>();
        List<Book> bookList = new();

        while (iterator.HasMoreResults)
            foreach (var item in await iterator.ReadNextAsync().ConfigureAwait(false))
                bookList.Add(item);

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(bookList);

        return response;
    }
}
