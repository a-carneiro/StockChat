using EndpointManager.Domain.Model;
using System.Collections.Generic;

namespace EndpointManager.Repository.Tests.Fixtures
{
    public class EndPointRepositoryFixture
    {
        public Endpoint Endpoint { get; set; }
        public IEnumerable<Endpoint> Endpoints { get; set; }
        public IEnumerable<Endpoint> EmptyEndpoints { get; set; }

        public EndPointRepositoryFixture()
        {
            SetUpData();
        }

        private void SetUpData()
        {
            Endpoint = new Endpoint("10", new MeterModel() {Code = "16", Id = 16 }, 1, "1.0v", 0);

            Endpoints = new List<Endpoint>()
            {
                Endpoint
            };

            EmptyEndpoints = new List<Endpoint>()
            { };

        }
    }
}
