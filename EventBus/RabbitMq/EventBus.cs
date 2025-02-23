using System.Text;
using EventBus.Abstractions;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EventBus.RabbitMq;

public class EventBus : IEventBus
{
    private readonly string _hostname;
    private readonly string _exchangeName;
    private readonly int _port;
    private readonly ConnectionFactory _factory;
    private IConnection _connection;
    private IChannel _channel;

    public EventBus(string hostname, string exchangeName, int port)
    {
        _hostname = hostname;
        _exchangeName = exchangeName;
        _port = port;
        _factory = new ConnectionFactory() { HostName = _hostname, Port = _port };
    }

    public async Task InitializeAsync()
    {
        _connection = await _factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync(); 

        await _channel.ExchangeDeclareAsync(_exchangeName, ExchangeType.Direct, durable: true);
    }

    public async Task PublishAsync<T>(T @event) where T : IntegrationEvent
    {
        var message = JsonConvert.SerializeObject(@event);
        var body = Encoding.UTF8.GetBytes(message);

        var properties = new BasicProperties
        {
            Persistent = true
        };
        properties.Persistent = true;

        await _channel.BasicPublishAsync(exchange: _exchangeName,
                              routingKey: typeof(T).Name,
                              mandatory: true,
                              basicProperties: properties,
                              body: body);
    }

    public async Task SubscribeAsync<T>(string consumerTag, string queueName, Func<T, Task> handler, CancellationToken cancellationToken) where T : IntegrationEvent
    {
        await _channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete: false);

        await _channel.QueueBindAsync(queueName, _exchangeName, typeof(T).Name);

         var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (model, args) =>
        {
            try
            {
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var @event = JsonConvert.DeserializeObject<T>(message);

                if (@event != null)
                {
                    await handler(@event);
                }

                await _channel.BasicAckAsync(args.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обработке сообщения: {ex.Message}");
            }
        };

        await _channel.BasicConsumeAsync(
            queue: queueName,
            autoAck: false,
            consumerTag: consumerTag,
            noLocal: false,
            exclusive: false,
            arguments: null,
            consumer: consumer,
            cancellationToken: cancellationToken
        );
    }
}

