using Moq.AutoMock;

namespace StockChat.Application.tests.Mock
{
    public abstract class BaseMock
    {
        protected readonly AutoMocker AutoMocker;

        protected BaseMock()
        {
            AutoMocker = new AutoMocker();

        }
    }
}
