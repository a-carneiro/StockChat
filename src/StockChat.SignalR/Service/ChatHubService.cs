using Microsoft.AspNetCore.SignalR;
using StockChat.Domain.Entity;
using StockChat.Interface.Service;
using StockChat.SignalR.Hubs;
using System.Threading.Tasks;

namespace StockChat.SignalR.Service
{
    public class ChatHubService : IChatHubService
    {
        private readonly IHubContext<ChatHub> _chatHub;
        public ChatHubService(IHubContext<ChatHub> chatHub)
        {
            _chatHub = chatHub;
        }
        public async Task RecievedMessage(string chatId, Message message)
        {
            await _chatHub.Clients.Group(chatId).SendAsync("RecievedMessage", message);
        }
        public async Task JoinGroup(string connectionId, string chatId)
        {
            await _chatHub.Groups.AddToGroupAsync(connectionId, chatId);
        }
        public async Task LeaveGroup(string connectionId, string chatId)
        {
            await _chatHub.Groups.RemoveFromGroupAsync(connectionId, chatId);
        }
    }
}