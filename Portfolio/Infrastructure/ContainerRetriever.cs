namespace Portfolio.Infrastructure;

using Microsoft.Azure.Cosmos;

public class ContainerRetriever : IContainerRetriever
{
    private Database _database;
    
    public ContainerRetriever()
    {
        var cosmosEndpoint = Environment.GetEnvironmentVariable("CosmosEndpoint");
        var cosmosPrimaryKey = Environment.GetEnvironmentVariable("CosmosPrimaryKey");

        var client = new CosmosClient(cosmosEndpoint, cosmosPrimaryKey);
        _database = client.GetDatabase("basic-db");
    }
    
    public Container GetContainer(string containerName)
    {
        return _database.GetContainer(containerName);
    }
}