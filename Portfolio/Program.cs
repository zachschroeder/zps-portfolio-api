using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Portfolio.Book;
using Portfolio.Groceries;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddScoped<IBookService, BookService>()
            .AddSingleton<IBookContainer, BookContainer>()
            .AddScoped<IGroceriesService, GroceriesService>()
            .AddSingleton<IGroceriesContainer, GroceriesContainer>();
    })
    .Build();

host.Run();
