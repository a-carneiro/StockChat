using Moq;
using StockChat.Application.tests.Fixture;
using StockChat.Interface.Repository;

namespace StockChat.Application.tests.Mock
{
    public class MessageApplicationMock : ApplicationMock<MessageApplication, MessageApplicationFixture>
    {
        public Mock<IMessageRepository> GetMessageRepository() => AutoMocker.GetMock<IMessageRepository>();

        public MessageApplicationMock(MessageApplicationFixture fixture) : base(fixture)
        {
            SetupRepository();
        }
        private void SetupRepository()
        {
        }
    }
}
