using StockChat.Domain.Entity;
using StockChatBot.Interface;
using System;

namespace StockChatBot.Service
{
    public class SendMessageService : ISendMessageService
    {
        private readonly IRabbitMqService _rabbitMqService;
        public SendMessageService(IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }
        private DateTime UtcNow => DateTime.UtcNow;

        public void SendMessage(bool isSuccess, string chatId, string code, string value)
        {
            if (isSuccess)
                SuccessMessage(chatId, code, value);
            else
                InsuccessMessage(chatId, code);
        }

        public void DefaultMessage(string chatId)
        {
            var message = new Message
            {
                Id = Guid.NewGuid(),
                ChatId = chatId,
                Name = "--BOT--",
                Content = "processing....",
                SendAt = UtcNow
            };
            _rabbitMqService.SendToQueue(message);
        }

        private void SuccessMessage(string chatId, string code, string value)
        {
            var message = new Message
            {
                Id = Guid.NewGuid(),
                ChatId = chatId,
                Name = "--BOT--",
                Content = $"{code.ToUpper()} quote is ${value} per share.",
                SendAt = UtcNow

            };
            _rabbitMqService.SendToQueue(message);
        }

        private void InsuccessMessage(string chatId, string code)
        {
            var message = new Message
            {
                Id = Guid.NewGuid(),
                ChatId = chatId,
                Name = "--BOT--",
                Content = $"{code.ToUpper()} was not found, try again",
                SendAt = UtcNow
            };
            _rabbitMqService.SendToQueue(message);
        }

    }
}
