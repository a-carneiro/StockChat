using StockChat.Domain.Entity;
using StockChat.Interface.Application;
using StockChat.Interface.Service;
using System.Threading.Tasks;

namespace StockChat.Application
{
    public class ChatHubApplication : IChatHubApplication
    {
        private readonly IChatHubService _chatHubService;
        public ChatHubApplication(IChatHubService chatHubService)
        {
            _chatHubService = chatHubService;
        }

        public async Task JoinGroupAsync(string connectionId, string chatId)
        {
            await _chatHubService.JoinGroup(connectionId, chatId);
        }

        public async Task LeaveGroupAsync(string connectionId, string chatId)
        {
            await _chatHubService.LeaveGroup(connectionId, chatId);
        }

        public async Task RecievedMessageAsync(string chatId, Message message)
        {
            await _chatHubService.RecievedMessage(chatId, message);
        }
    }
}
