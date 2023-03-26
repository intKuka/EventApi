using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace SpacesService
{
    public class SpaceDeletionSender
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        private const string ExchangeName = "DeletionExchange";
        private const string RoutingKey = "deletion-routing-key";
        private const string QueueName = "DeletionQueue";

        public SpaceDeletionSender()
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672"),
                ClientProvidedName = "Event RabbitMQ"
            };
            _connection = factory.CreateConnection();
            var channel = _connection.CreateModel();
            channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct);
            channel.QueueDeclare(QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind(QueueName, ExchangeName, RoutingKey, null);
            _channel = channel;
        }

        public void SendEvent(Guid id)
        {
            var obj = new { Name = "SpaceDeleteEvent", Type = 1, Id = id };
            var message = JsonSerializer.Serialize(obj);

            var messageBodyBytes = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(ExchangeName, RoutingKey, null, messageBodyBytes);

            _channel.Close();
            _connection.Close();
        }
    }
}
