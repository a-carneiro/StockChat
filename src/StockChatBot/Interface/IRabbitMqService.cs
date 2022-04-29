namespace StockChatBot.Interface
{
    public interface IRabbitMqService
    {
        void SendToQueue<T>(T data);
    }
}
