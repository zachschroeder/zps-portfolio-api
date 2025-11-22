namespace Portfolio.Infrastructure;

using Microsoft.Azure.Cosmos;

public interface IContainerRetriever
{
   public Container GetContainer(string containerName); 
}