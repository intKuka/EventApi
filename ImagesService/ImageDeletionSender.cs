using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace ImagesService
{
    public class ImageDeletionSender
    {
        private readonly IConnection _connection = null!;
        private readonly IModel _channel = null!;

        private const string ExchangeName = "DeletionExchange";
        private const string RoutingKey = "deletion-routing-key";
        private const string QueueName = "DeletionQueue";

        public ImageDeletionSender(IConfiguration config)
        {
            var factory = new ConnectionFactory
            {
                HostName = config["RabbitMQHost"],
                Port = int.Parse(config["RabbitMQPort"]!)
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
            var obj = new { Name = "ImageDeleteEvent", Type = 2, Id = id };
            var message = JsonSerializer.Serialize(obj);

            var messageBodyBytes = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(ExchangeName, RoutingKey, null, messageBodyBytes);

            _channel.Close();
            _connection.Close();
        }
        
        
    }
}
