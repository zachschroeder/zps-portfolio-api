namespace Portfolio.Infrastructure;

using Microsoft.Azure.Cosmos;

public interface IContainerService
{
   public Container GetContainer(string containerName); 
   public Task<Container> RecreateContainer(string containerName);
}