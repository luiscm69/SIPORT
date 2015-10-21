namespace CommandContracts.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using Infraestructure.Common.Types;

    [KnownType("GetKnowTypes")]
    public class Command
    {
        private static IEnumerable<Type> GetKnowTypes()
        {
            return KnownTypesHelper.GetKnownTypes<Command>();
        }
    }
}
