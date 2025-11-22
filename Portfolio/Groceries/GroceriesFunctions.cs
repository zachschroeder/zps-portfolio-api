namespace Portfolio.Groceries;

using System.Net;
using System.Text.Json;
using Infrastructure.Serialization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

public class GroceriesFunctions(IGroceriesService groceriesService, CamelCaseSerializer serializer)
{
    [Function(nameof(GetGroceries))]
    public async Task<HttpResponseData> GetGroceries([HttpTrigger(AuthorizationLevel.Function, "get", Route = "groceries")] HttpRequestData req)
    {
        var groceries = await groceriesService.GetGroceries();
        
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(groceries, serializer);
        return response;
    }
    
    [Function(nameof(AddGroceryItem))]
    public async Task<HttpResponseData> AddGroceryItem([HttpTrigger(AuthorizationLevel.Function, "post", Route = "grocery-item")] HttpRequestData req)
    {
        try
        {
            var addGroceryItem = await req.ReadFromJsonAsync<AddGroceryItemDto>();
            if (addGroceryItem == null)
                return req.CreateResponse(HttpStatusCode.BadRequest);

            var addedGroceryItem = await groceriesService.AddGroceryItem(addGroceryItem);

            var response = req.CreateResponse(HttpStatusCode.Created);
            await response.WriteAsJsonAsync(addedGroceryItem, serializer);
            return response;
        }
        catch (JsonException)
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }        
    }
}