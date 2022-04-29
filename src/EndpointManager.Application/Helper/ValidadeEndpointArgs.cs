using EndpointManager.Domain.Enum;
using System;
using System.Linq;

namespace EndpointManager.Application.Helper
{
    public static class ValidadeEndpointArgs
    {
        public static bool IsValidEndpoint(this string[] args)
        {
            args = args.Where(x => x != null).ToArray();
            if (args.Count().Equals(5) &&
                Int32.TryParse(args[1].ToString(), out var model) &&
                Int32.TryParse(args[2].ToString(), out var checktwo) &&
                IsValidState(args[4].ToString()))
                return true;

            return false;
        }
        public static bool IsValidState(this string state)
        {
            if ((Int32.TryParse(state, out var stateEnum) && Enum.IsDefined(typeof(EndpointStateEnum), stateEnum)))
                return true;

            return false;
        }
    }
}
