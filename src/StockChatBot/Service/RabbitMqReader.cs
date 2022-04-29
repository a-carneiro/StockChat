using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StockChat.Domain.Entity;
using StockChatBot.Interface;
using System;
using System.Text;
using System.Text.Json;

namespace StockChatBot.Service
{
    public class RabbitMqReader
    {
        private readonly IProcessMessageService _processMessageService;
        public RabbitMqReader(IProcessMessageService processMessageService)
        {
            _processMessageService = processMessageService;
        }
        public void Read()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "StockBotProcessPrice",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var data = Encoding.UTF8.GetString(body);
                        var message = JsonSerializer.Deserialize<Message>(data);
                        _processMessageService.ProcessMessage(message);
                        channel.BasicConsume(queue: "StockBotProcessPrice",
                                     autoAck: true,
                                     consumer: consumer);
                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                    catch (Exception)
                    {
                        //Logger
                        channel.BasicNack(ea.DeliveryTag, false, true);
                        throw;
                    }
                    
                };
                channel.BasicConsume(queue: "StockBotProcessPrice",
                                     autoAck: false,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
