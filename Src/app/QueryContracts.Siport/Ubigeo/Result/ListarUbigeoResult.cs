

using QueryContracts.Common;
using System.Collections.Generic;
namespace QueryContracts.Siport.Ubigeo.Result
{
    public class ListarUbigeoResult : QueryResult
    {
        public IEnumerable<ListarUbigeoDto> Hits { get; set;}
    }

    public class ListarUbigeoDto
    {
        public string CodigoUbigeo { get; set; }
        public string Descripcion { get; set; }
    }
}
