using StockChat.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockChat.Interface.Application
{
    public interface IChatApplication
    {
        Task CreateRoom(string name, string userId);
        IEnumerable<Chat> GetAllChats();
        Chat GetChatById(string id);
        Task JoinChat(string id, string userId);
        IEnumerable<Chat> GetAllUserChatsbyUserId(string userId);
        IEnumerable<Chat> GetAllNotInChats(string userId);
        Task AddMessagetToChat(string chatId, string content, string userName);
    }
}