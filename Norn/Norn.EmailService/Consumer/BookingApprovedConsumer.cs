using Infrastructure.Connections;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Norn.EmailService.Consumer;

public class BookingApprovedConsumer : BackgroundService
{
    IConfiguration _configuration;
    private string _mailQue;
    private IRabbitConnector _rabbitConnector;
    private Mail.IEmailSender _emailSender;
    public BookingApprovedConsumer(IConfiguration configuration, IRabbitConnector rabbitConnector, Mail.IEmailSender emailSender)
    {
        _configuration = configuration;
        _mailQue = _configuration["RABBITMQ_EMAIL_CHANNEL"];
        _rabbitConnector = rabbitConnector;
        _emailSender = emailSender;
    }

    public async Task ConsumeMessageQueFromEmailService()
    {
        IChannel channel = await _rabbitConnector.GetEmailChannel();
        await channel.ExchangeDeclareAsync(_mailQue, ExchangeType.Fanout);
        await channel.QueueDeclareAsync(
            queue: _mailQue,
            durable: true,
            exclusive: false,
            autoDelete: false);
        await channel.QueueBindAsync(queue: _mailQue, exchange: _mailQue, routingKey: string.Empty);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (sender, ea) =>
        {
            var message =
                Encoding.UTF8.GetString(ea.Body.ToArray());

            var booking =
                JsonConvert.DeserializeObject<Models.Models.Booking>(message);

            await _emailSender.SendBookingApprovedEmail(booking);

            await channel.BasicAckAsync(ea.DeliveryTag, false);
        };

        await channel.BasicConsumeAsync(
            queue: _mailQue,
            autoAck: false,
            consumer: consumer);
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await ConsumeMessageQueFromEmailService();
        await Task.Delay(Timeout.Infinite, stoppingToken); 
       }
}
