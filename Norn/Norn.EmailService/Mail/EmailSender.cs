using MailKit.Net.Smtp;
using MimeKit;
using System.Net;

namespace Norn.EmailService.Mail;

public class EmailSender : IEmailSender
{
    IConfiguration _configuration;
    private int _smptServerPort;
    private string _smptServerHost;
    private string _smtpServerUserName;
    private string _smtpServerPassword;
    private string _FromEmailAdress;
    private SmtpClient _smtpClient;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;

        _smtpClient = new();

        _smptServerPort = Convert.ToInt32(_configuration["CLOUDFLARE_SERVER_PORT"]);
        _smptServerHost =  _configuration["CLOUDFLARE_SERVER_HOST"];
        _smtpServerUserName = _configuration["CLOUDFLARE_SERVER_USERNAME"];
        _smtpServerPassword = _configuration["CLOUDFLARE_TOKEN"];
        _FromEmailAdress = _configuration["CLOUDFLARE_EMAIL_FROM"];
    }

    public async Task SendBookingApprovedEmail(Models.Models.Booking booking)
    {
        await _smtpClient.ConnectAsync(_smptServerHost, _smptServerPort, true);
        await _smtpClient.AuthenticateAsync(_smtpServerUserName, _smtpServerPassword); 
        var message = new MimeMessage();
        message.Subject = "Booking Approved";
        message.From.Add(new MailboxAddress(_FromEmailAdress, _FromEmailAdress));
        message.Sender = new MailboxAddress(_FromEmailAdress, _FromEmailAdress);
        message.To.Add(MailboxAddress.Parse((booking.User.Email))); 
        string emailMessage = $"Hello {booking.User.Email}. \n Your booking at {booking.Room.RoomName} From : {booking.TimeInterval.From} To " +
            $"{booking.TimeInterval.To} \n has been approved";
        message.Body = new TextPart()
        {
            Text = emailMessage
        }; 
        await _smtpClient.SendAsync(message);
        await _smtpClient.DisconnectAsync(true);
    }
}
