using System.Text;
using System.Text.Json;
using EventsApi.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace EventsApi.RabbitMq;

public class EventDeletionSender
{
    private readonly IConnection? _connection;
    private readonly IModel? _channel;

    private const string ExchangeName = "DeletionExchange";
    private const string RoutingKey = "deletion-routing-key";
    private const string QueueName = "DeletionQueue";

    public EventDeletionSender(IOptions<RabbitMqSettings> settings)
    {
        var factory = new ConnectionFactory
        {
            HostName = settings.Value.Host,
            Port = settings.Value.Port
        };
        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct);
            _channel.QueueDeclare(QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(QueueName, ExchangeName, RoutingKey, null);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"---------------- Connection to RMQ is failed: {ex}");
        }
    }

    public void SendEvent(Guid id)
    {
        var obj = new { Name = "EventDeleteEvent", Type = 3, Id = id };
        var message = JsonSerializer.Serialize(obj);

        var messageBodyBytes = Encoding.UTF8.GetBytes(message);
        _channel?.BasicPublish(ExchangeName, RoutingKey, null, messageBodyBytes);

        _channel?.Close();
        _connection?.Close();
    }
}