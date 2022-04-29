using EndpointManager.Domain.Model;
using System.Collections.Generic;

namespace EndpointManager.Repository.Tests.Fixtures
{
    public class ModelRepositoryFixture
    {
        public MeterModel MeterModel { get; set; }
        public IEnumerable<MeterModel> MeterModels { get; set; }
        public IEnumerable<MeterModel> EmptyMeterModels { get; set; }

        public ModelRepositoryFixture()
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
            {};

        }
    }
}
