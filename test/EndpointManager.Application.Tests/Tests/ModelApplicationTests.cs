using EndpointManager.Application.Tests.Fixture;
using EndpointManager.Application.Tests.Mock;
using EndpointManager.Domain.Model;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EndpointManager.Application.Tests.Tests
{
    [Trait("Model", "Application")]
    public class ModelApplicationTests : IClassFixture<ModelApplicationFixture>
    {
        private readonly ModelApplicationFixture _fixture;
        private readonly ModelApplicationMock _mock;

        public ModelApplicationTests(ModelApplicationFixture fixture)
        {
            _fixture = fixture;
            _mock = new ModelApplicationMock(_fixture);
        }

        [Fact]
        public void GetModelById_ModelExist_ShouldReturnModel()
        {
            //Arrange
            var modelApplication = _mock.GetRepository();
            //Act
            var result = modelApplication.GetModelById(_fixture.MeterModel.Id);
            //Assert
            result.Should().NotBeNull();
            result.Code.Should().Be(_fixture.MeterModel.Code);
        }

        [Fact]
        public void GetModelById_ModelDontExist_ShouldReturnNull()
        {
            //Arrange
            var modelApplication = _mock.GetRepository();
            //Act
            var result = modelApplication.GetModelById(0);
            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public void GetAllModels_ModelExist_ShouldReturnAllModels()
        {
            //Arrange
            var modelApplication = _mock.GetRepository();
            //Act
            var result = modelApplication.GetAllModels();
            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void GetAllModels_ModelDontExist_ShouldReturnEmpty()
        {
            //Arrange
            _mock.SetupModelRepositoryEmptyList();

            var modelRepository = _mock.GetRepository();
            //Act
            var result = modelRepository.GetAllModels();
            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task SeedMethod_ShouldAddModels()
        {
            //Arrange
            var repository = _mock.GetModelRepository();

            var modelRepository = _mock.GetRepository();
            //Act
            await modelRepository.SetInitialModels();
            //Assert

            repository.Verify(x => x.SetInitialModelsAsync(It.IsAny<IEnumerable<MeterModel>>()), Times.Once());
        }
    }
}