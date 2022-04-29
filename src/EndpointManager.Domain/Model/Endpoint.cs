using EndpointManager.Domain.Enum;

namespace EndpointManager.Domain.Model
{
    public class Endpoint
    {
        public string SeriaNumber { get; private set; }
        public virtual MeterModel Model { get; private set; }
        public int Number { get; private set; }
        public string FirmwareVersion { get; private set; }
        public EndpointStateEnum State { get; private set; }

        public Endpoint() { }

        public Endpoint(string seriaNumber, MeterModel model, int number, string firmwareVersion, int state)
        {
            SeriaNumber = seriaNumber;
            Model = model;
            Number = number;
            FirmwareVersion = firmwareVersion;
            State = (EndpointStateEnum)state;
        }

        public void ChangeState(int newState)
        {
            State = (EndpointStateEnum)newState;
        } 
    }
}