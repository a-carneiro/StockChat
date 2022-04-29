using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StockChat.Domain.Entity;
using StockChatBot.Interface;
using System;
using System.Text;
using System.Text.Json;

namespace StockChatBot.Service
{
    public class RabbitMqService : IRabbitMqService
    {
        public void SendToQueue<T>(T data)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "ReceiveBotResponse",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = JsonSerializer.Serialize(data);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "ReceiveBotResponse",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
