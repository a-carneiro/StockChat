using StockChat.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockChat.Interface.Repository
{
    public interface IUserChatRepository
    {
        Task Create(UserChat userChat);
        IEnumerable<UserChat> GetAllUserChatsbyUserId(string userId);
        IEnumerable<UserChat> GetAllNotInChats(string userId);
    }
}