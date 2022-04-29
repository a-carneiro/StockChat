using System.Threading.Tasks;

namespace StockChat.Interface.Application
{
    public interface IUserChatApplication
    {
        Task JoinChat(string id, string userId);
    }
}
