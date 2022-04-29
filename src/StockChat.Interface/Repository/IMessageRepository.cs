using StockChat.Domain.Entity;
using System.Threading.Tasks;

namespace StockChat.Interface.Repository
{
    public interface IMessageRepository
    {
        Task CreateMessage(Message message);
    }
}