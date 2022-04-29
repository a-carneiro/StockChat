using StockChat.Domain.Entity;
using StockChat.Domain.Enum;
using System;
using System.Collections.Generic;

namespace StockChat.Application.tests.Fixture
{
    public class MessageApplicationFixture
    {
        public Message Message { get; set; }
        public IEnumerable<Message> Messages { get; set; }

        public MessageApplicationFixture()
        {
            SetUpData();

        }
        private void SetUpData()
        {
            Message = new Message
            {
                Id = Guid.NewGuid(),
                ChatId = "1010",
                Content = "01",
                Name = "name",
                SendAt = DateTime.UtcNow,
                Chat = new Chat
                {
                    Name = "name",
                    Id = Guid.NewGuid().ToString(),
                    ChatType = ChatTypeEnum.Private,
                }
            };

            Messages = new List<Message>
            {
                Message
            };
        }
    }
}
