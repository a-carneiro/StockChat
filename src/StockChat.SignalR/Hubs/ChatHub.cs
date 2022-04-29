using Microsoft.AspNetCore.SignalR;

namespace StockChat.SignalR.Hubs
{
    public class ChatHub : Hub
    {
        public string GetConnectionId() => Context.ConnectionId;
    }
}
