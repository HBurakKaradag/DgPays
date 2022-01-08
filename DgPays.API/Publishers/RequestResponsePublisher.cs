using System;
using System.Text;
using DgPays.Domain;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace DgPays.API.Publishers
{
    public class RequestResponsePublisher
    {
        
        public  static async Task Publish(LogModel logModel)
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
                      
                    var message  = JsonConvert.SerializeObject(logModel);
                    var byteMessage = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange : "DGPays", 
                                         routingKey : logModel.RouteKey,
                                         body : byteMessage);


                 }
            }



        }




    }
}
