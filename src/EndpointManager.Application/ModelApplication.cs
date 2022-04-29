using EndpointManager.Domain.Model;
using EndpointManager.Interfece.Application;
using EndpointManager.Interfece.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EndpointManager.Application
{
    public class ModelApplication : IModelApplication
    {
        private readonly IModelRepository _modelRepository;

        public ModelApplication(IModelRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public IEnumerable<MeterModel> GetAllModels()
        {
            return _modelRepository.GetAllModels();
        }
        public MeterModel GetModelById(int modelId)
        {
            return _modelRepository.GetModelByModelId(modelId);
        }
        public async Task SetInitialModels()
        {
            var models = new List<MeterModel>
            {
                new MeterModel
                {
                    Code = "NSX1P2W",
                    Id = 16
                },
                new MeterModel
                {
                    Code = "NSX1P3W",
                    Id = 17
                },
                new MeterModel
                {
                    Code = "NSX2P3W",
                    Id = 18
                },
                new MeterModel
                {
                    Code = "NSX3P4W",
                    Id = 19
                }
            };

            await _modelRepository.SetInitialModelsAsync(models);
        }
    }
}
