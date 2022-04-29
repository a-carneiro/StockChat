using EndpointManager.Repository.Tests.Fixtures;
using EndpointManager.Repository.Tests.Mock;
using FluentAssertions;
using Xunit;
using Moq;
using System.Collections.Generic;
using EndpointManager.Domain.Model;
using System.Threading.Tasks;
using System.Threading;

namespace EndpointManager.Repository.Tests.Tests
{
    [Trait("Model", "Repository")]
    public class ModelRepositoryTests : IClassFixture<ModelRepositoryFixture>
    {
        private readonly ModelRepositoryFixture _fixture;
        private readonly ModelRepositoryMock _mock;

        public ModelRepositoryTests(ModelRepositoryFixture fixture)
        {
            _fixture = fixture;
            _mock = new ModelRepositoryMock(_fixture);
        }

        [Fact]
        public void GetModelByModelId_ModelExist_ShouldReturnModel()
        {
            //Arrange
            var modelRepository = _mock.GetRepository();
            //Act
            var result = modelRepository.GetModelByModelId(_fixture.MeterModel.Id);
            //Assert
            result.Should().NotBeNull();
            result.Code.Should().Be(_fixture.MeterModel.Code);
        }

        [Fact]
        public void GetModelByModelId_ModelDontExist_ShouldReturnNull()
        {
            //Arrange
            var modelRepository = _mock.GetRepository();
            //Act
            var result = modelRepository.GetModelByModelId(0);
            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public void GetAllModels_ModelExist_ShouldReturnAllModels()
        {
            //Arrange
            var modelRepository = _mock.GetRepository();
            //Act
            var result = modelRepository.GetAllModels();
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
            var context = _mock.GetContext();

            var modelRepository = _mock.GetRepository();
            //Act
            await modelRepository.SetInitialModelsAsync(It.IsAny<IEnumerable<MeterModel>>());
            //Assert

            context.Verify(x => x.SaveChanges(), Times.Once());
        }
    }
}
