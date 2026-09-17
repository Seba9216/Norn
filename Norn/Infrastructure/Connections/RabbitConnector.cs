using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace Infrastructure.Connections;

public class RabbitConnector : IRabbitConnector
{
    IConfiguration _configuration;
    private string _mailExchange;
    public RabbitConnector(IConfiguration configuration)
    {
        _configuration = configuration;
        _mailExchange = _configuration["RABBITMQ_EMAIL_CHANNEL"];
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
        await channel.ExchangeDeclareAsync(_mailExchange, ExchangeType.Fanout);
        var Serilazied = JsonConvert.SerializeObject(message);
        var messageAsBytes = Encoding.UTF8.GetBytes(Serilazied);
        await channel.BasicPublishAsync(_mailExchange, string.Empty, messageAsBytes);
    }
    public static async Task BindExchangesAndQues(IChannel channel, string queName, string exchangeName)
    {
        await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Fanout);
        await channel.QueueDeclareAsync(
            queue: queName,
            durable: true,
            exclusive: false,
            autoDelete: false);
        await channel.QueueBindAsync(queue: queName, exchange: exchangeName, routingKey: string.Empty);
    }
    public static async Task ConsumeBasicMessage(IChannel channel, string queName, AsyncEventingBasicConsumer consumer) 
    {
        await channel.BasicConsumeAsync(
          queue: queName,
          autoAck: false,
          consumer: consumer);
    }



    public async Task<IChannel> GetEmailChannel()
    {
        var connection = await GetRabbitConnection();
        var channel = await connection.CreateChannelAsync();
        return channel;
    }
}
