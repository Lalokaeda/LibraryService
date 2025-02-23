using BookRentService.Infrastructure;
using BookRentService.Infrastructure.Messaging.Consumers;
using EventBus.Abstractions;
using EventBus.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var eventBusInstance = app.Services.GetRequiredService<IEventBus>();
var consumer = app.Services.GetRequiredService<BookDeletedEventConsumer>();

await eventBusInstance.SubscribeAsync<BookExemplarDeletedEvent>(
    consumerTag: "book_rent_service",
    queueName: "book_rent_queue",
    handler: consumer.Handle,
    cancellationToken: new CancellationToken()
);

app.Run();
