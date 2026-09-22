using Infrastructure.Connections;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Channels;

namespace Norn.SignaRService.Consumer;

public class BookingApprovedConsumer : BackgroundService
{
    IConfiguration _configuration;
    IRabbitConnector _rabbitConnector;
    IHubContext<NornHub> _nornHub;
    private string _bookingApprovedExchange;
    private readonly string _signalRQue = "SignalRQueue";


    public BookingApprovedConsumer(IConfiguration configuration, IRabbitConnector rabbitConnector, IHubContext<NornHub> nornHub)
    {
        _configuration = configuration;
        _rabbitConnector = rabbitConnector;
        _bookingApprovedExchange = _configuration["RABBITMQ_EXCHANGE_APPROVED"];
        _nornHub = nornHub;
    }


    public async Task ConsumeMessageQueFromEmailService()
    {
        IChannel channel = await _rabbitConnector.GetChannel();
        await RabbitConnector.BindExchangesAndQues(channel, _signalRQue, _bookingApprovedExchange);

        var consumer = new RabbitMQ.Client.Events.AsyncEventingBasicConsumer(channel);
        
        consumer.ReceivedAsync += async (sender, ea) =>
        {
            var message = Encoding.UTF8.GetString(ea.Body.ToArray());
            var booking = JsonConvert.DeserializeObject<Models.Models.Booking>(message);

            await _nornHub.Clients.All.SendAsync("BookingApproved", booking);

            await channel.BasicAckAsync(ea.DeliveryTag, false);
        };
        await RabbitConnector.ConsumeBasicMessage(channel, _signalRQue, consumer);
    }



    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await ConsumeMessageQueFromEmailService();
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}
