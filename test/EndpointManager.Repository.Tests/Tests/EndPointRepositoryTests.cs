using EndpointManager.Repository.Tests.Fixtures;
using EndpointManager.Repository.Tests.Mock;
using FluentAssertions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EndpointManager.Repository.Tests.Tests
{
    public class EndPointRepositoryTests : IClassFixture<EndPointRepositoryFixture>
    {
        private readonly EndPointRepositoryFixture _fixture;
        private readonly EndPointRepositoryMock _mock;

        public EndPointRepositoryTests(EndPointRepositoryFixture fixture)
        {
            _fixture = fixture;
            _mock = new EndPointRepositoryMock(_fixture);
        }

        [Fact]
        public async Task AddEndpointAsync_ShouldAddNewEndpoint()
        {
            //Arrange
            var context = _mock.GetContext();

            var modelRepository = _mock.GetRepository();
            //Act
            await modelRepository.AddEndpointAsync(_fixture.Endpoint);
            //Assert
            context.Verify(x => x.Endpoints.AddAsync(_fixture.Endpoint, CancellationToken.None), Times.Once());
            context.Verify(x => x.SaveChanges(), Times.Once());
        }

        [Fact]
        public async Task UpdateEndpointAsyn_ShouldUpdate()
        {
            //Arrange
            var context = _mock.GetContext();

            var modelRepository = _mock.GetRepository();
            //Act
            await modelRepository.UpdateEndpointAsyn(_fixture.Endpoint);
            //Assert
            context.Verify(x => x.Endpoints.Update(_fixture.Endpoint), Times.Once());
            context.Verify(x => x.SaveChanges(), Times.Once());
        }

        [Fact]
        public async Task DeleteEndpointAsync_ShouldDelete()
        {
            //Arrange
            var context = _mock.GetContext();

            var modelRepository = _mock.GetRepository();
            //Act
            await modelRepository.DeleteEndpointAsync(_fixture.Endpoint);
            //Assert
            context.Verify(x => x.Endpoints.Remove(_fixture.Endpoint), Times.Once());
            context.Verify(x => x.SaveChanges(), Times.Once());
        }

        [Fact]
        public void GetAllEndpoints_EndpointsExist_ShouldReturnEndpoints()
        {
            //Arrange
            var modelRepository = _mock.GetRepository();
            //Act
            var result = modelRepository.GetAllEndpoints();
            //Assert
            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GetAllEndpoints_EndpointsDontExist_ShouldReturnEmpty()
        {
            //Arrange
            _mock.SetupModelRepositoryEmptyList();

            var modelRepository = _mock.GetRepository();
            //Act
            var result = modelRepository.GetAllEndpoints();
            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void GetBySerialNumber_EndpointsExist_ShouldReturnEndpoint()
        {
            //Arrange
            var modelRepository = _mock.GetRepository();
            //Act
            var result = modelRepository.GetBySerialNumber(_fixture.Endpoint.SeriaNumber);
            //Assert
            result.Should().NotBeNull();
            result.Number.Should().Be(_fixture.Endpoint.Number);
        }

        [Fact]
        public void GetBySerialNumber_EndpointDontExist_ShouldReturnNull()
        {
            //Arrange
            _mock.SetupModelRepositoryEmptyList();

            var modelRepository = _mock.GetRepository();
            //Act
            var result = modelRepository.GetBySerialNumber(_fixture.Endpoint.SeriaNumber);
            //Assert
            result.Should().BeNull();
        }
    }
}
