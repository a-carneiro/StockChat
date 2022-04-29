using StockChat.Domain.Enum;

namespace StockChat.Domain.Entity
{
    public class UserChat
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public string ChatId { get; set; }
        public Chat Chat { get; set; }

        public UserRoleEnum UserRole { get; set; }

    }
}