using Infrastructure.Connections;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Norn.EmailService.Consumer;

public class BookingApprovedConsumer : BackgroundService
{
    IConfiguration _configuration;
    private string _mailExchange;
    private readonly string _mailQue = "EmailQueue";
    private IRabbitConnector _rabbitConnector;
    private Mail.IEmailSender _emailSender;
    public BookingApprovedConsumer(IConfiguration configuration, IRabbitConnector rabbitConnector, Mail.IEmailSender emailSender)
    {
        _configuration = configuration;
        _mailExchange = _configuration["RABBITMQ_EMAIL_CHANNEL"];
        _rabbitConnector = rabbitConnector;
        _emailSender = emailSender;
    }

    public async Task ConsumeMessageQueFromEmailService()
    {
        IChannel channel = await _rabbitConnector.GetEmailChannel();
        await RabbitConnector.BindExchangesAndQues(channel, _mailQue, _mailExchange);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (sender, ea) =>
        {
            var message = Encoding.UTF8.GetString(ea.Body.ToArray());
            var booking = JsonConvert.DeserializeObject<Models.Models.Booking>(message);

            await _emailSender.SendBookingApprovedEmail(booking);

            await channel.BasicAckAsync(ea.DeliveryTag, false);
        };

        await RabbitConnector.ConsumeBasicMessage(channel,_mailQue,consumer);
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await ConsumeMessageQueFromEmailService();
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}
