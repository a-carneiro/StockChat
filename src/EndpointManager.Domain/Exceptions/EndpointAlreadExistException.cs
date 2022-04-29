using System;
using System.Collections.Generic;
using System.Text;

namespace EndpointManager.Domain.Exceptions
{
    public class EndpointAlreadExistException : Exception
    {
        public EndpointAlreadExistException(string serialNumber) : base($"Endpoint with the serial number {serialNumber} alread exists, check the model id and ty agan")
        { }
    }
}
