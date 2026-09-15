
using Infrastructure.Connections;
using Norn.EmailService.Consumer;
using Norn.EmailService.Mail;
using IEmailSender = Norn.EmailService.Mail.IEmailSender;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IRabbitConnector, RabbitConnector>();
builder.Services.AddScoped<IEmailSender, EmailSender>(); 
builder.Services.AddHostedService<BookingApprovedConsumer>();
var app = builder.Build();


app.Run();

