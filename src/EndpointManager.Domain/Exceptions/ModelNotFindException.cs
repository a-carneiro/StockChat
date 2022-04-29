using System;
using System.Collections.Generic;
using System.Text;

namespace EndpointManager.Domain.Exceptions
{
    public class ModelNotFindException : Exception
    {
        public ModelNotFindException(int modelId) : base($"Model with id {modelId} not found, check the model id and ty agan")
        { }
    }
}
