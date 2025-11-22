using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Portfolio.Book;
using Portfolio.Groceries;
using Portfolio.Infrastructure;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddSingleton<IContainerRetriever, ContainerRetriever>() 
            .AddScoped<IBookService, BookService>()
            .AddScoped<IGroceriesService, GroceriesService>()
            .AddScoped<IGroceriesStateComposer, GroceriesStateComposer>();
    })
    .Build();

host.Run();
