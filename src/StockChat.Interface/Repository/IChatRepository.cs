using StockChat.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockChat.Interface.Repository
{
    public interface IChatRepository
    {
        Task CreateChatRoom(Chat chat);
        IEnumerable<Chat> GetAllChats();
        Chat GetChatById(string id);
        IEnumerable<Chat> GetAllNotInChats(string userId);
        IEnumerable<Chat> GetAllUserChatsbyUserId(string userId);
    }
}
