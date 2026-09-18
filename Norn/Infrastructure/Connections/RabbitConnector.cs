using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Infrastructure.Connections;

public class RabbitConnector : IRabbitConnector
{
    IConfiguration _configuration;
    private string _bookingApprovedExchange;
    private IChannel _currentChannel;
    public RabbitConnector(IConfiguration configuration)
    {
        _configuration = configuration;
        _bookingApprovedExchange = _configuration["RABBITMQ_EMAIL_CHANNEL"];
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
    public async Task PublishMessageToExchangeForServices(object message)
    {
        var channel = await GetChannel();
        await channel.ExchangeDeclareAsync(_bookingApprovedExchange, ExchangeType.Fanout);
        var Serilazied = JsonConvert.SerializeObject(message);
        var messageAsBytes = Encoding.UTF8.GetBytes(Serilazied);
        await channel.BasicPublishAsync(_bookingApprovedExchange, string.Empty, messageAsBytes);
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



    public async Task<IChannel> GetChannel()
    {
        if (_currentChannel != null)
        {
            var connection = await GetRabbitConnection();
            var channel = await connection.CreateChannelAsync();
            return channel;
        }
        return _currentChannel;
    }
}
