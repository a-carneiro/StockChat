namespace StockChatBot.Interface
{
    public interface ISendMessageService
    {
        void SendMessage(bool isSuccess, string chatId, string code, string value);
        void DefaultMessage(string chatId);
    }
}
