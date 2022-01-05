using System.Text;
using DgPays.Domain;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace DgPays.API.Publishers
{
    public class ProductPublisher
    {
        public static async Task Publish(List<Product> products)
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "admin",
                Password = "123456",
                Port = 5672
            };

            using (IConnection connection = connectionFactory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    //channel.ExchangeDeclare()git
                    channel.QueueDeclare(queue: "ProductQueue",
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null
                                        );

                    //  channel.ExchangeBind()

                    var message = JsonConvert.SerializeObject(products);
                    var byteMessage = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "DGPays",
                                         routingKey: "ProductEvent",
                                         body: byteMessage
                    );
                }
            }
        }
    }
}