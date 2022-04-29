using EndpointManager.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EndpointManager.Interfece.Application
{
    public interface IModelApplication
    {
        Task SetInitialModels();
        IEnumerable<MeterModel> GetAllModels();
        MeterModel GetModelById(int modelId);
    }
}
