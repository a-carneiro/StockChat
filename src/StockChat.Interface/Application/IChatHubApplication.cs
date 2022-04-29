using StockChat.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockChat.Interface.Application
{
    public interface IChatHubApplication
    {
        Task RecievedMessageAsync(string chatId, Message message);
        Task JoinGroupAsync(string connectionId, string chatId);
        Task LeaveGroupAsync(string connectionId, string chatId);
    }
}
