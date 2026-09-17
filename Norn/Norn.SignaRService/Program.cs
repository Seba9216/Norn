using Infrastructure.Connections;
using Norn.SignaRService.Consumer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});


builder.WebHost.UseUrls("http://0.0.0.0:8008");
builder.Services.AddSignalR();

builder.Services.AddHostedService<BookingApprovedConsumer>();
builder.Services.AddScoped<IRabbitConnector, RabbitConnector>();

var app = builder.Build();


app.UseCors("AllowAll");

app.MapHub<NornHub>("/nornHub");



app.Run();
