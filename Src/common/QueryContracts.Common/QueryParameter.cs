namespace QueryContracts.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using Infraestructure.Common.Types;

    [KnownType("GetKnowTypes")]
    public class QueryParameter
    {
        public string ServidorWeb { get; set; }

        public string CodigoUsuario { get; set; }

        private static IEnumerable<Type> GetKnowTypes()
        {
            var knownTypes = KnownTypesHelper.GetKnownTypes<QueryParameter>();
            return knownTypes;
        }
    }
}
