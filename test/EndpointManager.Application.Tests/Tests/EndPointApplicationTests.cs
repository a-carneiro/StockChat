using EndpointManager.Application.Tests.Fixture;
using EndpointManager.Application.Tests.Mock;
using EndpointManager.Domain.Exceptions;
using EndpointManager.Domain.Model;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace EndpointManager.Application.Tests.Tests
{
    [Trait("EndPoint", "Application")]
    public class EndPointApplicationTests : IClassFixture<EndPointApplicationFixture>
    {
        private readonly EndPointApplicationFixture _fixture;
        private readonly EndPointApplicationMock _mock;

        public EndPointApplicationTests(EndPointApplicationFixture fixture)
        {
            _fixture = fixture;
            _mock = new EndPointApplicationMock(_fixture);
        }

        [Fact]
        public async Task CreateEndpoint_EndpointDontExist_ShouldCreateEndpoint()
        {
            //Arrange
            var endpointRepository = _mock.GetEndPointRepository();
            var modelApplication = _mock.GetRepository();
            //Act
            await modelApplication.CreateEndpoint(new string[5] {"11", "10", "10", "10", "1" });
            //Assert
            endpointRepository.Verify(x => x.AddEndpointAsync(It.IsAny<Endpoint>()), Times.Once);
        }

        [Fact]
        public async Task CreateEndpoint_ModelDontExist_ShouldthrowAnException()
        {
            //Arrange
            var modelApplication = _mock.GetRepository();
            //Act and Assert
            await Assert.ThrowsAsync<ModelNotFindException>(() => modelApplication.CreateEndpoint(new string[5] { "11", "15", "10", "10", "1" }));
        }


        [Fact]
        public async Task CreateEndpoint_EndpointAlreadExist_ShouldthrowAnException()
        {
            //Arrange
            var modelApplication = _mock.GetRepository();
            //Act and Assert
            await Assert.ThrowsAsync<EndpointAlreadExistException>(() => modelApplication.CreateEndpoint(new string[5] { "10", "10", "10", "10", "1" }));
        }

        [Fact]
        public async Task CreateEndpoint_ArgsIsNotValid_EnumNotFound_ShouldthrowAnException()
        {
            //Arrange
            var modelApplication = _mock.GetRepository();
            //Act and Assert
            await Assert.ThrowsAsync<InvalidEndpointParametersException>(() => modelApplication.CreateEndpoint(new string[5] { "11", "10", "10", "10", "5" }));
        }

        [Fact]
        public async Task CreateEndpoint_ArgsIsNotValid_SizeNot5_ShouldthrowAnException()
        {
            //Arrange
            var modelApplication = _mock.GetRepository();
            //Act and Assert
            await Assert.ThrowsAsync<InvalidEndpointParametersException>(() => modelApplication.CreateEndpoint(new string[4] { "11", "10", "10", "10"}));
        }

        [Fact]
        public async Task EditEndPointState_StateNotFound_ShouldthrowAnException()
        {
            //Arrange
            var modelApplication = _mock.GetRepository();
            //Act and Assert
            await Assert.ThrowsAsync<SwitchStateIsNotValidException>(() => modelApplication.EditEndPointState(It.IsAny<string>(), "5"));
        }

        [Fact]
        public async Task EditEndPointState_EndpointNotFound_ShouldthrowAnException()
        {
            //Arrange
            var modelApplication = _mock.GetRepository();
            //Act and Assert
            await Assert.ThrowsAsync<EndpointNotFindException>(() => modelApplication.EditEndPointState("11", "0"));
        }

        [Fact]
        public async Task EditEndPointState_InputsAreValid_ShouldChangeState()
        {
            //Arrange
            var endpointRepository = _mock.GetEndPointRepository();
            var modelApplication = _mock.GetRepository();
            //Act
            await modelApplication.EditEndPointState("10", "2");
            //Assert
            endpointRepository.Verify(x => x.UpdateEndpointAsyn(It.IsAny<Endpoint>()), Times.Once);
        }

        [Fact]
        public async Task DeleteEndpointBySerialNumberAsync_EndpointNotFound_ShouldthrowAnException()
        {
            //Arrange
            var modelApplication = _mock.GetRepository();
            //Act and Assert
            await Assert.ThrowsAsync<EndpointNotFindException>(() => modelApplication.DeleteEndpointBySerialNumberAsync("11"));
        }

        [Fact]
        public async Task DeleteEndpointBySerialNumberAsync_EndpointIsValid_ShouldChangeState()
        {
            //Arrange
            var endpointRepository = _mock.GetEndPointRepository();
            var modelApplication = _mock.GetRepository();
            //Act
            await modelApplication.DeleteEndpointBySerialNumberAsync("10");
            //Assert
            endpointRepository.Verify(x => x.DeleteEndpointAsync(It.IsAny<Endpoint>()), Times.Once);
        }

        [Fact]
        public void GetAllEndpoints_EndpointsAreAvailable_ShouldReturnEdpoints()
        {
            //Arrange
            var endpointRepository = _mock.GetEndPointRepository();
            var modelApplication = _mock.GetRepository();
            //Act
            var endpoints = modelApplication.GetAllEndpoints();
            //Assert
            endpoints.Should().NotBeNullOrEmpty();
            endpointRepository.Verify(x => x.GetAllEndpoints(), Times.Once);
        }

        [Fact]
        public void GetAllEndpoints_EndpointsAreNotAvailable_ShouldReturnEmpty()
        {
            //Arrange
            var endpointRepository = _mock.GetEndPointRepository();
            var modelApplication = _mock.GetRepository();

            _mock.SetupNullEndpointsRepository();
            //Act
            var endpoints = modelApplication.GetAllEndpoints();
            //Assert
            endpoints.Should().BeNullOrEmpty();
            endpointRepository.Verify(x => x.GetAllEndpoints(), Times.Once);
        }

        [Fact]
        public void GetBySerialNumber_EndpointFoud_ShouldReturnEndpoint()
        {
            //Arrange
            var endpointRepository = _mock.GetEndPointRepository();
            var modelApplication = _mock.GetRepository();

            //Act
            var endpoints = modelApplication.GetBySerialNumber("10");
            //Assert
            endpoints.Should().NotBeNull();
            endpoints.Number.Should().Be(_fixture.Endpoint.Number);
            endpointRepository.Verify(x => x.GetBySerialNumber(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void GetBySerialNumber_EndpointNotFoud_ShouldReturnEndpoint()
        {
            //Arrange
            var endpointRepository = _mock.GetEndPointRepository();
            var modelApplication = _mock.GetRepository();

            //Act
            var endpoints = modelApplication.GetBySerialNumber("11");
            //Assert
            endpoints.Should().BeNull();
            endpointRepository.Verify(x => x.GetBySerialNumber(It.IsAny<string>()), Times.Once);
        }
    }
}