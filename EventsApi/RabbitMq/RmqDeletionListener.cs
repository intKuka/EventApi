using System;
using System.Text;
using EventsApi.Features.Events.DeleteEvent;
using EventsApi.MongoDb;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ThirdParty.Json.LitJson;

namespace EventsApi.RabbitMq
{
    public class RmqDeletionListener : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IEventRepo _eventRepo;

        private const string ExchangeName = "DeletionExchange";
        private const string RoutingKey = "deletion-routing-key";
        private const string QueueName = "DeletionQueue";

        public RmqDeletionListener(IEventRepo eventRepo, IConfiguration config)
        {
            _eventRepo = eventRepo;
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
                _channel.BasicQos(0, 1, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"---------------- Connection to RMQ is failed: {ex}");
            }

        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            try
            {
                consumer.Received += (sender, args) =>
                {
                    var body = args.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    dynamic obj = JObject.Parse(message);
                    if (obj.Type == 1)
                    {
                        DeleteSpace(new Guid(obj.Id.ToString()));
                    }
                    else if (obj.Type == 2)
                    {
                        DeleteImage(new Guid(obj.Id.ToString()));
                    }
                    else if (obj.Type == 3)
                    {
                        var v = obj.Id;
                        DeleteEvent(new Guid(obj.Id.ToString()));
                    }

                    _channel.BasicAck(args.DeliveryTag, false);
                };

                _channel.BasicConsume(QueueName, false, consumer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"---------------- Could not handle events: {ex}");
            }

            return Task.CompletedTask;
        }
        
        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
        private void DeleteSpace(Guid id)
        {

            var events = _eventRepo.GetAllEvents().Result.ToList();
            foreach (var e in events.Where(e => e.SpaceId == id))
            {
                 _eventRepo.DeleteEvent(e.Id);
                DeleteEvent(e.Id);
            }
            
        }

        private void DeleteImage(Guid id)
        {
            var events = _eventRepo.GetAllEvents().Result.ToList();
            foreach (var e in events.Where(e => e.ImageId == id))
            {
                e.ImageId = null;
                _eventRepo.UpdateEvent(e);
            }

        }

        private static void DeleteEvent(Guid id)
        {
            Console.WriteLine($"Событие {id} удалено");
        }
    }
}
