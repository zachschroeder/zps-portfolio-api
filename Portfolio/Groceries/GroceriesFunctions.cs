namespace Portfolio.Groceries;

using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

public class GroceriesFunctions
{
    [Function(nameof(GetGroceries))]
    public HttpResponseData GetGroceries([HttpTrigger(AuthorizationLevel.Function, "get", Route = "groceries")] HttpRequestData req)
    {
        return req.CreateResponse(HttpStatusCode.OK);
    }

}