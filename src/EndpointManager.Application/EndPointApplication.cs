using EndpointManager.Application.Helper;
using EndpointManager.Domain.Exceptions;
using EndpointManager.Domain.Model;
using EndpointManager.Interfece.Application;
using EndpointManager.Interfece.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EndpointManager.Application
{
    public class EndPointApplication : IEndPointApplication
    {

        private readonly IEndPointRepository _endPointRepository;
        private readonly IModelApplication _modelApplication;

        public EndPointApplication(IEndPointRepository endPointRepository, IModelApplication modelApplication)
        {
            _endPointRepository = endPointRepository;
            _modelApplication = modelApplication;
        }

        public async Task CreateEndpoint(string[] args)
        {
            if (!args.IsValidEndpoint())
                throw new InvalidEndpointParametersException();

            int modelId = int.Parse(args[1]);

            var model = _modelApplication.GetModelById(modelId);

            if (model is null)
                throw new ModelNotFindException(modelId);

            var endpoint = GetBySerialNumber(args[0]);

            if (endpoint != null)
                throw new EndpointAlreadExistException(args[0]);

            await _endPointRepository.AddEndpointAsync(new Endpoint(args[0], model, int.Parse(args[2]), args[3], int.Parse(args[4])));
        }
        public async Task EditEndPointState(string serialNumber, string newState)
        {
            if (!newState.IsValidState())
                throw new SwitchStateIsNotValidException();

            var endpoint = GetBySerialNumber(serialNumber);

            if (endpoint is null)
                throw new EndpointNotFindException(serialNumber);

            endpoint.ChangeState(int.Parse(newState));

            await _endPointRepository.UpdateEndpointAsyn(endpoint);
        }
        public async Task DeleteEndpointBySerialNumberAsync(string serialNumber)
        {
            var endpoint = GetBySerialNumber(serialNumber);

            if (endpoint is null)
                throw new EndpointNotFindException(serialNumber);

            await _endPointRepository.DeleteEndpointAsync(endpoint);
        }
        public IEnumerable<Endpoint> GetAllEndpoints()
        {
            return _endPointRepository.GetAllEndpoints();
        }
        public Endpoint GetBySerialNumber(string serialNumber)
        {
            var endpoint = _endPointRepository.GetBySerialNumber(serialNumber);

            return endpoint;
        }


        
    }
}
