using EndpointManager.Application.Tests.Fixture;
using EndpointManager.Interfece.Repository;
using Moq;

namespace EndpointManager.Application.Tests.Mock
{
    public class ModelApplicationMock : ApplicationMock<ModelApplication, ModelApplicationFixture>
    {

        public Mock<IModelRepository> GetModelRepository() => AutoMocker.GetMock<IModelRepository>();

        public ModelApplicationMock(ModelApplicationFixture fixture) : base(fixture)
        {
            SetupModelRepository();
        }
        private void SetupModelRepository()
        {
            var modelRepository = GetModelRepository();
            modelRepository.Setup(x => x.GetAllModels()).Returns(Fixture.MeterModels);
            modelRepository.Setup(x => x.GetModelByModelId(Fixture.MeterModel.Id)).Returns(Fixture.MeterModel);
        }
        public void SetupModelRepositoryEmptyList()
        {
            var modelRepository = GetModelRepository();
            modelRepository.Setup(x => x.GetAllModels()).Returns(Fixture.EmptyMeterModels);
        }
    }
}
