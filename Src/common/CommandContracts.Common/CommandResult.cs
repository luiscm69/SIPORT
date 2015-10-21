namespace CommandContracts.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using Infraestructure.Common.Types;

    [KnownType("GetKnowTypes")]
    public class CommandResult
    {
        private static IEnumerable<Type> GetKnowTypes()
        {
            var types = KnownTypesHelper.GetKnownTypes<CommandResult>();
            return types;
        }
    }
}
