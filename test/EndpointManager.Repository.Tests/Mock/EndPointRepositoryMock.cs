using EndpointManager.Domain.Model;
using EndpointManager.Repository.Tests.Fixtures;
using System.Linq;

namespace EndpointManager.Repository.Tests.Mock
{
    public class EndPointRepositoryMock : RepositoryMock<EndPointRepository, EndPointRepositoryFixture>
    {
        public EndPointRepositoryMock(EndPointRepositoryFixture fixture) : base(fixture)
        {
            SetupEndpointRepository();
        }
        private void SetupEndpointRepository()
        {
            var mockSet = MockDbSet(Fixture.Endpoints.AsQueryable());

            var mockContext = GetContext();
            mockContext.Setup(c => c.Endpoints).Returns(mockSet.Object);
        }

        public void SetupModelRepositoryEmptyList()
        {
            var mockSet = MockDbSet(Fixture.EmptyEndpoints.AsQueryable());

            var mockContext = GetContext();
            mockContext.Setup(c => c.Endpoints).Returns(mockSet.Object);
        }
    }
}
