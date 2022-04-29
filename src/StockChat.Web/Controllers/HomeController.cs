using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockChat.Interface.Application;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StockChat.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IChatApplication _chatApplication;
        private string userId => User.FindFirst(ClaimTypes.NameIdentifier).Value;
        public HomeController(IChatApplication chatApplication)
        {
            _chatApplication = chatApplication;
        }

        public IActionResult Index()
        {
            return View(_chatApplication.GetAllNotInChats(userId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoon(string name)
        {
            await _chatApplication.CreateRoom(name, User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return RedirectToAction("Index");
        }

        [HttpGet("{id}")]
        public IActionResult GetMessagesByChatId(string id)
        {
            var messages = _chatApplication.GetChatById(id);
            return View("Chat", messages);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> JoinChat(string id)
        {
            await _chatApplication.JoinChat(id, userId);
            return RedirectToAction("Chat", "Home", new { id = id});
        }
    }
}