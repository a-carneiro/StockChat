using EndpointManager.Application.Tests.Fixture;
using EndpointManager.Domain.Model;
using EndpointManager.Interfece.Application;
using EndpointManager.Interfece.Repository;
using Moq;
using System.Collections.Generic;

namespace EndpointManager.Application.Tests.Mock
{
    public class EndPointApplicationMock : ApplicationMock<EndPointApplication, EndPointApplicationFixture>
    {

        public Mock<IEndPointRepository> GetEndPointRepository() => AutoMocker.GetMock<IEndPointRepository>();
        public Mock<IModelApplication> GetModelApplication() => AutoMocker.GetMock<IModelApplication>();

        public EndPointApplicationMock(EndPointApplicationFixture fixture) : base(fixture)
        {
            SetupEndpointRepository();
            SetupModelApplication();
        }
        private void SetupEndpointRepository()
        {
            var endpointRepository = GetEndPointRepository();
            endpointRepository.Setup(x => x.GetBySerialNumber(Fixture.Endpoint.SeriaNumber)).Returns(Fixture.Endpoint);
            endpointRepository.Setup(x => x.GetAllEndpoints()).Returns(Fixture.Endpoints);
        }

        public void SetupNullEndpointsRepository()
        {
            var endpointRepository = GetEndPointRepository();
            endpointRepository.Setup(x => x.GetAllEndpoints()).Returns((IEnumerable<Endpoint>)null);
        }

        private void SetupModelApplication()
        {
            var modelRepository = GetModelApplication();
            modelRepository.Setup(x => x.GetModelById(Fixture.MeterModel.Id)).Returns(Fixture.MeterModel);
        }
        public void SetupModelRepositoryNullModel()
        {
            var modelRepository = GetModelApplication();
            modelRepository.Setup(x => x.GetModelById(It.IsAny<int>())).Returns((MeterModel)null);
        }
    }
}
