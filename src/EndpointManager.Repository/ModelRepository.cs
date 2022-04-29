using EndpointManager.Domain.Model;
using EndpointManager.Interfece.Repository;
using EndpointManager.Repository.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndpointManager.Repository
{
    public class ModelRepository : IModelRepository
    {
        private readonly IDbContext _context;

        public ModelRepository(IDbContext context)
        {
            _context = context;
        }

        public async Task SetInitialModelsAsync(IEnumerable<MeterModel> models)
        {
            await _context.Models.AddRangeAsync(models);
            _context.SaveChanges();
        }
        public IEnumerable<MeterModel> GetAllModels()
        {
            return _context.Models.ToList();
        }
        public MeterModel GetModelByModelId(int modelId)
        {
            return _context.Models.FirstOrDefault(x => x.Id.Equals(modelId));
        }
    }
}
