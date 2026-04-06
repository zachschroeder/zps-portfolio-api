namespace Portfolio.Infrastructure;

using Microsoft.Azure.Cosmos;

public class ContainerService : IContainerService
{
    private Database _database;
    
    public ContainerService()
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
    
    public async Task<Container> RecreateContainer(string containerName)
    {
        var currentContainer = _database.GetContainer(containerName);
        await currentContainer.DeleteContainerAsync();
        var newContainerResponse = await _database.CreateContainerIfNotExistsAsync(containerName, "/id");
        return newContainerResponse.Container;
    }
}