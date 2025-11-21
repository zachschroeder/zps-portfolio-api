namespace Portfolio.Groceries;

using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

public class GroceriesFunctions(IGroceriesService groceriesService)
{
    [Function(nameof(GetGroceries))]
    public async Task<HttpResponseData> GetGroceries([HttpTrigger(AuthorizationLevel.Function, "get", Route = "groceries")] HttpRequestData req)
    {
        var groceries = groceriesService.GetGroceries();
        
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(groceries);
        return response;
    }

}