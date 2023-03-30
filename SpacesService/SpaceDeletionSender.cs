using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace SpacesService
{
    public class SpaceDeletionSender
    {
        private readonly IConnection? _connection;
        private readonly IModel? _channel;


        private const string ExchangeName = "DeletionExchange";
        private const string RoutingKey = "deletion-routing-key";
        private const string QueueName = "DeletionQueue";

        public SpaceDeletionSender(IConfiguration config)
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
            var obj = new { Name = "SpaceDeleteEvent", Type = 1, Id = id };
            var message = JsonSerializer.Serialize(obj);

            var messageBodyBytes = Encoding.UTF8.GetBytes(message);
            if(_connection is { IsOpen: true })
            {
                _channel?.BasicPublish(ExchangeName, RoutingKey, null, messageBodyBytes);
                _channel?.Close();
                _connection?.Close();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("No connection to RabbitMQ");
                Console.ResetColor();
            }
        }
    }
}
