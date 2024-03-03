using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Portfolio.Book;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddScoped<IBookService, BookService>();
        services.AddSingleton<IBookContainer, BookContainer>();
    })
    .Build();

host.Run();
