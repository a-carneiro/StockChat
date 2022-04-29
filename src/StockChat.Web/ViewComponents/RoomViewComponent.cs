using Microsoft.AspNetCore.Mvc;
using StockChat.Interface.Application;
using System.Security.Claims;

namespace StockChat.Web.ViewComponents
{
    public class RoomViewComponent : ViewComponent
    {
        private readonly IChatApplication _chatApplication;
        public RoomViewComponent(IChatApplication chatApplication)
        {
            _chatApplication = chatApplication;
        }

        public IViewComponentResult Invoke()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            var chats = _chatApplication.GetAllUserChatsbyUserId(userId);

            return View(chats);
        }
    }
}