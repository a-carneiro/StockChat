using Moq.AutoMock;

namespace EndpointManager.Repository.Tests.Mock
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
