using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Portfolio.Book;
using Portfolio.Groceries;
using Portfolio.Infrastructure;
using Portfolio.Infrastructure.Serialization;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddSingleton<CamelCaseSerializer>()
            .AddSingleton<IContainerService, ContainerService>() 
            .AddScoped<IBookService, BookService>()
            .AddScoped<IGroceriesService, GroceriesService>()
            .AddScoped<IGroceriesStateComposer, GroceriesStateComposer>()
            .AddSingleton<IGroceriesCategorizer, GroceriesCategorizer>();
    })
    .Build();

host.Run();
