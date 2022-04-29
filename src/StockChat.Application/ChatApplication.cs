using StockChat.Domain.Entity;
using StockChat.Domain.Enum;
using StockChat.Interface.Application;
using StockChat.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockChat.Application
{
    public class ChatApplication : IChatApplication
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserChatApplication _userChatApplication;
        private readonly IMessageApplication _messageApplication;

        public ChatApplication(IChatRepository chatRepository, IUserChatApplication userApplication, IMessageApplication messageApplication)
        {
            _chatRepository = chatRepository;
            _userChatApplication = userApplication;
            _messageApplication = messageApplication;
        }

        public async Task CreateRoom(string name, string userId)
        {
            var chat = new Chat()
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                ChatType = ChatTypeEnum.Room
            };

            chat.UserChats.Add(new UserChat
            {
                UserId = userId,
                UserRole = UserRoleEnum.Admin,
            });

            await _chatRepository.CreateChatRoom(chat);
        }

        public IEnumerable<Chat> GetAllChats()
        {
            return _chatRepository.GetAllChats();
        }

        public IEnumerable<Chat> GetAllUserChatsbyUserId(string userId)
        {
            return _chatRepository.GetAllUserChatsbyUserId(userId);
        }

        public Chat GetChatById(string id)
        {
            var chat = _chatRepository.GetChatById(id);
            if (chat != null)
            {
                chat.Messages = chat.Messages?.OrderBy(x => x.SendAt).Take(50).ToList();
            }

            return chat;
        }
        
        public async Task JoinChat(string chatId, string userId)
        {
            await _userChatApplication.JoinChat(chatId, userId);
        }

        public IEnumerable<Chat> GetAllNotInChats(string userId)
        {
            return _chatRepository.GetAllNotInChats(userId);
        }
        public async Task AddMessagetToChat(string chatId, string content, string userName)
        {
            await _messageApplication.CreateMessage(chatId, content, userName);
        }

    }
}
