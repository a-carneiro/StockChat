using StockChat.Domain.Entity;
using StockChat.Domain.Enum;
using StockChat.Interface.Application;
using StockChat.Interface.Repository;
using System.Threading.Tasks;

namespace StockChat.Application
{
    public class UserChatApplication : IUserChatApplication
    {
        private readonly IUserChatRepository _userChatRepository;

        public UserChatApplication(IUserChatRepository userChatRepository)
        {
            _userChatRepository = userChatRepository;
        }

        public async Task JoinChat(string id, string userId)
        {
            var userChat = new UserChat
            {
                ChatId = id,
                UserId = userId,
                UserRole = UserRoleEnum.Member
            };

            await _userChatRepository.Create(userChat);
        }

    }
}
