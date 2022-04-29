using EndpointManager.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EndpointManager.Interfece.Repository
{
    public interface IEndPointRepository
    {
        public Endpoint GetBySerialNumber(string serialNumber);
        public IEnumerable<Endpoint> GetAllEndpoints();
        public Task AddEndpointAsync(Endpoint endpoint);
        public Task DeleteEndpointAsync(Endpoint endpoint);
        public Task UpdateEndpointAsyn(Endpoint endpoint);
    }
}
