using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using StockChat.Application.Options;
using StockChat.Interface.Infrastructure;
using System.Text;
using System.Text.Json;

namespace StockChat.Infrastructure
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly BotConfigurationOptions _BotConfigurationOptions;
        public RabbitMqService(IOptions<BotConfigurationOptions> options)
        {
            _BotConfigurationOptions = options.Value;
        }

        public void SendToQueue<T>(T data)
        {
            var factory = new ConnectionFactory() { HostName = _BotConfigurationOptions.HostName };
            using (var connection =  factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _BotConfigurationOptions.QueueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = JsonSerializer.Serialize(data);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: _BotConfigurationOptions.QueueName,
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
