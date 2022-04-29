using EndpointManager.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EndpointManager.Interfece.Repository
{
    public interface IModelRepository
    {
        Task SetInitialModelsAsync(IEnumerable<MeterModel> models);
        IEnumerable<MeterModel> GetAllModels();
        MeterModel GetModelByModelId(int modelId);
    }
}
