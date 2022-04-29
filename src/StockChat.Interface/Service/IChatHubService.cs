using StockChat.Domain.Entity;
using System.Threading.Tasks;

namespace StockChat.Interface.Service
{
    public interface IChatHubService
    {
        Task RecievedMessage(string chatId, Message message);
        Task JoinGroup(string connectionId, string chatId);
        Task LeaveGroup(string connectionId, string chatId);
    }
}