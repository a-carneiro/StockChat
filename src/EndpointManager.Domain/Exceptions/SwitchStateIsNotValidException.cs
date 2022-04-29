using System;

namespace EndpointManager.Domain.Exceptions
{
    public class SwitchStateIsNotValidException : Exception
    {
        public SwitchStateIsNotValidException() : base("Switch state is not a valid State, check the paramter and ty again")
        { }
    }
}
