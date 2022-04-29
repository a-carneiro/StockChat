using StockChat.Domain.Entity;

namespace StockChat.Interface.Infrastructure
{
    public interface IRabbitMqService
    {
        public void SendToQueue<T>(T data);
    }
}
