using System.Text;
using DgPays.Domain;
using DgPays.Domain.ConfigurationModels;
using DgPays.ProductConsumer.ConfigurationModels;
using DgPays.ProductConsumer.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DgPays.ProductConsumer.Receivers
{
    public class ProductConsumeReceiver : BackgroundService
    {

        private IConnection _connection;
        private IModel _channel;
        private readonly RabbitSettings _rabbitSettings;
        private readonly ConsumeQueueSettings _consumeQueueSettings;

        private readonly IProductService _productService;

        public ProductConsumeReceiver(IOptions<RabbitSettings> rabbitSettings,
                                      IOptions<ConsumeQueueSettings> consumeQueueSettings,
                                      IProductService productService
                                     )
        {
            _rabbitSettings = rabbitSettings.Value;
            _consumeQueueSettings = consumeQueueSettings.Value;
            _productService = productService;

            InitializeRabbitMqListener();
        }


        public void InitializeRabbitMqListener()
        {
            IConnectionFactory connectionFactory = new ConnectionFactory()
            {
                HostName = _rabbitSettings.Host,
                UserName = _rabbitSettings.UserName,
                Password = _rabbitSettings.Password,
                Port = _rabbitSettings.Port,
                Ssl = new SslOption { Enabled = _rabbitSettings.Ssl }
            };

            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.BasicQos(0, 10, false);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                Enum.TryParse(ea.RoutingKey, out RouteKeysEnum routeKey);
                HandleRouteKeyMessage(routeKey, content);

                _channel.BasicAck(ea.DeliveryTag, false);

            };

            _channel.BasicConsume(_consumeQueueSettings.ConsumeQueueName, false, consumer);
            return Task.CompletedTask;
        }

        public void HandleRouteKeyMessage(RouteKeysEnum routeKey, string content)
        {
            switch (routeKey)
            {
                case RouteKeysEnum.ProductEvent:
                    _productService.SetProduct(content).GetAwaiter().GetResult();
                    break;
                case RouteKeysEnum.CartEvent:
                    break;
                default:
                    break;
            }
        }
    }
}