using StockChat.Domain.Entity;
using System.Threading.Tasks;

namespace StockChat.Interface.Application
{
    public interface IMessageApplication
    {
        Task CreateMessage(string chatId, string content, string userName);
        Task CreateMessage(Message message);
    }
}