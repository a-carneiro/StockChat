using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StockChat.Domain.Entity;
using StockChat.Interface.Application;
using System.Text;
using System.Text.Json;

namespace StockChat.Infrastructure
{
    public class ReadEvent
    {
        private readonly IMessageApplication _messageApplication;

        public ReadEvent(IMessageApplication messageApplication)
        {
            _messageApplication = messageApplication;
        }

        public void Read(IHost host)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", DispatchConsumersAsync = true };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "ReceiveBotResponse",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var data = Encoding.UTF8.GetString(body);
                        var message = JsonSerializer.Deserialize<Message>(data);

                        await _messageApplication.CreateMessage(message);

                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                    catch (System.Exception)
                    {
                        //Logger
                        channel.BasicNack(ea.DeliveryTag, false, true);
                        throw;
                    }
                };
                channel.BasicConsume(queue: "ReceiveBotResponse",
                                     autoAck: false,
                                     consumer: consumer);

                host.Run();
            }
        }
    }
}
