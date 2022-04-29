using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StockChat.Interface.Application;
using System.Threading.Tasks;

namespace StockChat.Web.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ChatController : Controller
    {
        private readonly IChatHubApplication _chatHubApplication;
        private readonly IChatApplication _chatApplication;
        public ChatController(IChatHubApplication chatHubApplication, IChatApplication chatApplication)
        {
            _chatHubApplication = chatHubApplication;
            _chatApplication = chatApplication;
        }

        [HttpPost("[action]/{connectionId}/{chatId}")]
        public async Task<IActionResult> JoinGroup(string connectionId, string chatId)
        {
            await _chatHubApplication.JoinGroupAsync(connectionId, chatId);
            return Ok();
        }

        [HttpPost("[action]/{connectionId}/{chatId}")]
        public async Task<IActionResult> LeaveGroup(string connectionId, string chatId)
        {
            await _chatHubApplication.LeaveGroupAsync(connectionId, chatId);
            return Ok();
        }

        [HttpPost("[action]")]
        public  async Task<IActionResult> SendMessage(string message, string chatId)
        {
            await _chatApplication.AddMessagetToChat(chatId, message, User.Identity.Name);
            return Ok();
        }
    }
}