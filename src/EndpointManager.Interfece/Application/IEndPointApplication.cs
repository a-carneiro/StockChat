using EndpointManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EndpointManager.Interfece.Application
{
    public interface IEndPointApplication
    {
        public Task CreateEndpoint(string[] args);
        public Endpoint GetBySerialNumber(string serialNumber);
        public IEnumerable<Endpoint> GetAllEndpoints();
        public Task DeleteEndpointBySerialNumberAsync(string serialNumber);
        public Task EditEndPointState(string serialNumber, string newState);
    }
}
