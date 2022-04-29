using Moq;
using StockChat.Application.tests.Fixture;
using StockChat.Application.tests.Mock;
using StockChat.Domain.Entity;
using System.Threading.Tasks;
using Xunit;

namespace StockChat.Application.tests.Tests
{
    [Trait("Message", "Application")]
    public class MessageApplicationTests : IClassFixture<MessageApplicationFixture>
    {
        private readonly MessageApplicationFixture _fixture;
        private readonly MessageApplicationMock _mock;

        public MessageApplicationTests(MessageApplicationFixture fixture)
        {
            _fixture = fixture;
            _mock = new MessageApplicationMock(_fixture);
        }

        [Fact]
        public async Task CreateEndpoint_EndpointDontExist_ShouldCreateEndpoint()
        {
            //Arrange
            var endpointRepository = _mock.GetMessageRepository();
            var modelApplication = _mock.GetRepository();
            //Act
            await modelApplication.CreateMessage(_fixture.Message.ChatId, _fixture.Message.Content, _fixture.Message.Name);
            //Assert
            endpointRepository.Verify(x => x.CreateMessage(It.IsAny<Message>()), Times.Once);
        }

    }
}
