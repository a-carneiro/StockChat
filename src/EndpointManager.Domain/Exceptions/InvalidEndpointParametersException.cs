using System;

namespace EndpointManager.Domain.Exceptions
{
    public class InvalidEndpointParametersException : Exception
    {
        public InvalidEndpointParametersException() : base("Some parameters are missing or it is invalid, check the paramters and ty agan")
        { }
    }
}
