using Microsoft.EntityFrameworkCore;
using StockChat.Domain.Entity;
using StockChat.Interface.Repository;
using StockChat.Repository.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockChat.Repository
{
    public class UserChatRepository : IUserChatRepository
    {
        private readonly StockChatDbContext _stockChatDbContext;
        public UserChatRepository(StockChatDbContext stockChatDbContext)
        {
            _stockChatDbContext = stockChatDbContext;
        }

        public async Task Create(UserChat userChat)
        {
            await _stockChatDbContext.UserChats.AddAsync(userChat);
            await _stockChatDbContext.SaveChangesAsync();
        }

        public IEnumerable<UserChat> GetAllUserChatsbyUserId(string userId)
        {
            return _stockChatDbContext.UserChats
                .Include(x => x.Chat)
                .Where(x => x.UserId.Equals(userId))
                .ToList();
        }
        public IEnumerable<UserChat> GetAllNotInChats(string userId)
        {
            return _stockChatDbContext.UserChats
                .Include(x => x.Chat)
                .Where(x => !x.UserId.Equals(userId))
                .ToList();
        }
    }
}
