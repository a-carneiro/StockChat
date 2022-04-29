using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using StockChat.Application.Options;
using StockChat.Domain.Entity;
using StockChat.Interface.Application;
using StockChat.Interface.Infrastructure;
using StockChat.Interface.Repository;
using System;
using System.Threading.Tasks;

namespace StockChat.Application
{
    public class MessageApplication : IMessageApplication
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IChatHubApplication _chatHubApplication;
        private readonly BotConfigurationOptions _BotConfigurationOptions;
        private readonly IRabbitMqService _rabbitMqService;

        public MessageApplication(IMessageRepository messageRepository, IChatHubApplication chatHubApplication, IOptions<BotConfigurationOptions> options, IRabbitMqService rabbitMqService)
        {
            _messageRepository = messageRepository;
            _chatHubApplication = chatHubApplication;
            _BotConfigurationOptions = options.Value;
            _rabbitMqService = rabbitMqService;
        }

        public async Task CreateMessage(string chatId, string content, string userName)
        {
            var message = new Message
            {
                Id = Guid.NewGuid(),
                ChatId = chatId,
                Content = content,
                Name = userName,
                SendAt = DateTime.UtcNow
            };

            await _messageRepository.CreateMessage(message);

            await _chatHubApplication.RecievedMessageAsync(chatId, message);

            if (content.Contains(_BotConfigurationOptions.MessageInitializer))
            {
                _rabbitMqService.SendToQueue(message);
            }
        }
        public async Task CreateMessage(Message message)
        {
            await _messageRepository.CreateMessage(message);
            await _chatHubApplication.RecievedMessageAsync(message.ChatId, message);
        }
    }
}
