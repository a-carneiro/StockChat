using EndpointManager.Domain.Model;
using EndpointManager.Repository.Tests.Fixtures;
using System.Linq;

namespace EndpointManager.Repository.Tests.Mock
{
    public class ModelRepositoryMock : RepositoryMock<ModelRepository, ModelRepositoryFixture>
    {
        public ModelRepositoryMock(ModelRepositoryFixture fixture) : base(fixture)
        {
            SetupModelRepository();
        }
        private void SetupModelRepository()
        {
            var mockSet = MockDbSet(Fixture.MeterModels.AsQueryable());

            var mockContext = GetContext();
            mockContext.Setup(c => c.Models).Returns(mockSet.Object);
        }

        public void SetupModelRepositoryEmptyList()
        {
            var mockSet = MockDbSet(Fixture.EmptyMeterModels.AsQueryable());

            var mockContext = GetContext();
            mockContext.Setup(c => c.Models).Returns(mockSet.Object);
        }
    }
}
