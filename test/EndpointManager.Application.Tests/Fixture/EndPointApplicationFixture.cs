using EndpointManager.Domain.Model;
using System.Collections.Generic;

namespace EndpointManager.Application.Tests.Fixture
{
    public class EndPointApplicationFixture
    {
        public Endpoint Endpoint { get; set; }
        public IEnumerable<Endpoint> Endpoints { get; set; }
        public IEnumerable<Endpoint> EmptyEndpoints { get; set; }
        public MeterModel MeterModel { get; set; }

        public EndPointApplicationFixture()
        {
            SetUpData();
        }

        private void SetUpData()
        {
            Endpoint = new Endpoint("10", new MeterModel() { Code = "16", Id = 16 }, 1, "1.0v", 0);

            Endpoints = new List<Endpoint>()
            {
                Endpoint
            };

            EmptyEndpoints = new List<Endpoint>()
            { };

            MeterModel = new MeterModel()
            {
                Code = "000",
                Id = 10
            };
        }
    }
}
