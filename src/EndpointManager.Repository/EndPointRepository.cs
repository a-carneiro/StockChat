using EndpointManager.Domain.Model;
using EndpointManager.Interfece.Repository;
using EndpointManager.Repository.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndpointManager.Repository
{
    public class EndPointRepository : IEndPointRepository
    {
        private readonly IDbContext _context;

        public EndPointRepository(IDbContext context)
        {
            _context = context;
        }

        public async Task AddEndpointAsync(Endpoint endpoint)
        {
            await _context.Endpoints.AddAsync(endpoint);
            _context.SaveChanges();
        }
        public async Task UpdateEndpointAsyn(Endpoint endpoint)
        {
            _context.Endpoints.Update(endpoint);
            _context.SaveChanges();
        }
        public async Task DeleteEndpointAsync(Endpoint endpoint)
        {
            _context.Endpoints.Remove(endpoint);
            _context.SaveChanges();
        }
        public IEnumerable<Endpoint> GetAllEndpoints()
        {
            return _context.Endpoints.ToList();
        }
        public Endpoint GetBySerialNumber(string serialNumber)
        {
            return _context.Endpoints.FirstOrDefault(x => x.SeriaNumber.Equals(serialNumber));
        }
    }
}