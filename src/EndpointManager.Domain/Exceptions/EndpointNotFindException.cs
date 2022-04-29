using System;

namespace EndpointManager.Domain.Exceptions
{
    public class EndpointNotFindException : Exception
    {
        public EndpointNotFindException(string serialNumber) : base($"Endpoint with the serial number {serialNumber} was not found")
        { }
    }
}
