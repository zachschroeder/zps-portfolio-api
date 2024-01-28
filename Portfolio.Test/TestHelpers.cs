namespace Portfolio.Test;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Text;

public static class TestHelpers
{
    public static HttpRequestData CreateRequest<T>(T requestData) where T : class
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddFunctionsWorkerDefaults();

        var serializedData = JsonConvert.SerializeObject(requestData);
        var bodyDataStream = new MemoryStream(Encoding.UTF8.GetBytes(serializedData));

        var context = new Mock<FunctionContext>();
        context.SetupProperty(context => context.InstanceServices, serviceCollection.BuildServiceProvider());

        var request = new Mock<HttpRequestData>(context.Object);
        request.Setup(r => r.Body).Returns(bodyDataStream);
        request.Setup(r => r.CreateResponse()).Returns(new MockHttpResponseData(context.Object));

        return request.Object;
    }
}

public class MockHttpResponseData : HttpResponseData
{
    public MockHttpResponseData(FunctionContext functionContext) : base(functionContext)
    { }


    public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

    public override HttpHeadersCollection Headers { get; set; } = new HttpHeadersCollection();

    public override Stream Body { get; set; } = new MemoryStream();

    public override HttpCookies Cookies { get; }
}