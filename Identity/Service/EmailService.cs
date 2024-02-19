using Identity.Models;
using Identity.Service.IService;
using Microsoft.AspNetCore.Connections;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client;

namespace Identity.Service
{
    public class EmailService : IEmailService
    {
        public void SendEmail(Notification notifications)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "admin",
                Password = "admin",
                VirtualHost = "/"
            };

            //Rabbit Mq create Connection
            var conn = factory.CreateConnection();

            // Channel for Rabbit Mq
            using var channel = conn.CreateModel();

            // Create Queue

            channel.QueueDeclare("notifications", durable: true, exclusive: false);

            // convert to byte array to be sent

            var jsonString = JsonSerializer.Serialize(notifications);
            var body = Encoding.UTF8.GetBytes(jsonString);

            // Send Message on RabbitMQ
            channel.BasicPublish("", "notifications", body: body);
        }
    }
}
