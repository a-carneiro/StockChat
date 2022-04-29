using Microsoft.EntityFrameworkCore;
using StockChat.Domain.Entity;
using StockChat.Interface.Repository;
using StockChat.Repository.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockChat.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly StockChatDbContext _stockChatDbContext;
        public ChatRepository(StockChatDbContext stockChatDbContext)
        {
            _stockChatDbContext = stockChatDbContext;
        }

        public async Task CreateChatRoom(Chat chat)
        {
            await _stockChatDbContext.Chats.AddAsync(chat);
            await _stockChatDbContext.SaveChangesAsync();
        }
        public IEnumerable<Chat> GetAllChats()
        {
            return _stockChatDbContext.Chats.ToList();
        }

        public Chat GetChatById(string id)
        {
            return _stockChatDbContext.Chats
                .Include(x => x.Messages)
                .FirstOrDefault(x => x.Id.Equals(id));
        }

        public IEnumerable<Chat> GetAllNotInChats(string userId)
        {
            return _stockChatDbContext.Chats
                .Include(x => x.UserChats)
                .Where(y => !y.UserChats
                    .Any(z => z.UserId == userId))
                .ToList();
        }
        public IEnumerable<Chat> GetAllUserChatsbyUserId(string userId)
        {
            return _stockChatDbContext.Chats
                .Include(x => x.UserChats)
                .Where(y => y.UserChats
                    .Any(z => z.UserId == userId))
                .ToList();
        }
    }
}
