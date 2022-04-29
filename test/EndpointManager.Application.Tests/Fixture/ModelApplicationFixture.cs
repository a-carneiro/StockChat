using EndpointManager.Domain.Model;
using System.Collections.Generic;

namespace EndpointManager.Application.Tests.Fixture
{
    public class ModelApplicationFixture
    {
        public MeterModel MeterModel { get; set; }
        public IEnumerable<MeterModel> MeterModels { get; set; }
        public IEnumerable<MeterModel> EmptyMeterModels { get; set; }

        public ModelApplicationFixture()
        {
            SetUpData();
        }

        private void SetUpData()
        {
            MeterModel = new MeterModel()
            {
                Code = "000",
                Id = 10
            };

            MeterModels = new List<MeterModel>()
            {
                MeterModel
            };

            EmptyMeterModels = new List<MeterModel>()
            { };

        }
    }
}
