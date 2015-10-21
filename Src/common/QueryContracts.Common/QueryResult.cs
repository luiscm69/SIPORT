
namespace QueryContracts.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using Infraestructure.Common.Types;

    [KnownType("GetKnowTypes")]
    public class QueryResult
    {
        private static IEnumerable<Type> GetKnowTypes()
        {
            IEnumerable<Type> types = KnownTypesHelper.GetKnownTypes<QueryResult>();
            return types;
        }
    }
}
