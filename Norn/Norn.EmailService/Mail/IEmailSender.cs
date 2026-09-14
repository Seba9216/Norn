namespace Norn.EmailService.Mail;

public interface IEmailSender
{
    public Task SendBookingApprovedEmail(Models.Models.Booking booking); 

}
