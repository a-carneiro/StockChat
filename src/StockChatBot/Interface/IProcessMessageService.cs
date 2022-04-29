using StockChat.Domain.Entity;

namespace StockChatBot.Interface
{
    public interface IProcessMessageService
    {
        void ProcessMessage(Message receivedMessage);
    }
}