using RabbitMQ.Client;

namespace Infrastructure.Connections;

public interface IRabbitConnector
{
    public Task PublishMessageToExchangeForServices(object  message);
    public Task<IChannel> GetChannel(); 

}