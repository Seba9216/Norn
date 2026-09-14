namespace Norn.EmailService.Consumer;

public interface IBookingApprovedConsumer
{
    public Task ConsumeMessageQueFromEmailService(); 

}