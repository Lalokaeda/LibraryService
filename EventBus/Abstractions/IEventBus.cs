namespace EventBus.Abstractions;

public interface IEventBus
{
    Task PublishAsync<T>(T @event) where T : IntegrationEvent;
    Task SubscribeAsync<T>(string consumerTag, string queueName, Func<T, Task> handler, CancellationToken cancellationToken) where T : IntegrationEvent;
}
