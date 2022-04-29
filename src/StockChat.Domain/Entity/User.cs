using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace StockChat.Domain.Entity
{
    public class User : IdentityUser
    {
        public ICollection<UserChat> Chats { get; set; }
    }
}
