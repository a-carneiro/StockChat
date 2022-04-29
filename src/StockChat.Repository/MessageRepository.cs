using StockChat.Domain.Entity;
using StockChat.Interface.Repository;
using StockChat.Repository.Infrastructure;
using System.Threading.Tasks;

namespace StockChat.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly StockChatDbContext _stockChatDbContext;
        public MessageRepository(StockChatDbContext stockChatDbContext)
        {
            _stockChatDbContext = stockChatDbContext;
        }

        public async Task CreateMessage(Message message)
        {
            await _stockChatDbContext.Messages.AddAsync(message);
            await _stockChatDbContext.SaveChangesAsync();
        }
    }
}
