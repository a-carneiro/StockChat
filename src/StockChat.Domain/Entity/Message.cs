using System;

namespace StockChat.Domain.Entity
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime SendAt { get; set; }
        public string ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
