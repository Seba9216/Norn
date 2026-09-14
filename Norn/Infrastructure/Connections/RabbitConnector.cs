using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Infrastructure.Connections;

public class RabbitConnector : IRabbitConnector
{
    IConfiguration _configuration;
    private string _mailQue;
    public RabbitConnector(IConfiguration configuration)
    {
        _configuration = configuration;
        _mailQue = _configuration["RABBITMQ_EMAIL_CHANNEL"];
    }
    public async Task<IConnection?> GetRabbitConnection()
    {
        var connectionFactory = new ConnectionFactory();
        connectionFactory.UserName = _configuration["RABBITMQ_DEFAULT_USER"];
        connectionFactory.Password = _configuration["RABBITMQ_DEFAULT_PASS"];
        connectionFactory.HostName = _configuration["RABBITMQ_DEFAULT_HOSTNAME"];

        var connection = await connectionFactory.CreateConnectionAsync();
        return connection;
    }
    public async Task PublishMessageQueForEmailService(object message)
    {
        var channel = await GetEmailChannel();
        await channel.ExchangeDeclareAsync(_mailQue, ExchangeType.Fanout);
        await channel.QueueDeclareAsync(_mailQue, true, false, false, null);
        await channel.QueueBindAsync(queue: _mailQue,exchange: _mailQue,routingKey: string.Empty);
        var Serilazied = JsonConvert.SerializeObject(message);
        var messageAsBytes = Encoding.UTF8.GetBytes(Serilazied);

        await channel.BasicPublishAsync(_mailQue, string.Empty, messageAsBytes);
    }
  

    public async Task<IChannel> GetEmailChannel()
    {
        var connection = await GetRabbitConnection();
        var emailQue = _mailQue;
        var channel = await connection.CreateChannelAsync();
        return channel;
    }
}
