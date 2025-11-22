using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Portfolio.Book;
using Portfolio.Groceries;
using Portfolio.Infrastructure;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddScoped<IBookService, BookService>()
            .AddScoped<IGroceriesService, GroceriesService>()
            .AddSingleton<IContainerRetriever, ContainerRetriever>();
    })
    .Build();

host.Run();
