using StockChat.Domain.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StockChat.Domain.Entity
{
    public class Chat
    {
        public Chat()
        {
            Messages = new List<Message>();
            UserChats = new List<UserChat>();
        }

        public string Id { get; set; }
        [MaxLength(10)]
        public string Name { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<UserChat> UserChats { get; set; }
        public ChatTypeEnum ChatType { get; set; }
        


    }
}