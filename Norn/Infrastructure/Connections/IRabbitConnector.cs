using RabbitMQ.Client;

namespace Infrastructure.Connections;

public interface IRabbitConnector
{
    public Task PublishMessageQueForEmailService(object  message);
    public Task<IChannel> GetEmailChannel(); 

}