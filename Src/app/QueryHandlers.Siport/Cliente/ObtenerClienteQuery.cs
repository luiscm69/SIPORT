using Data.Common;
using QueryContracts.Common;
using QueryContracts.Siport.Cliente.Parameter;
using QueryContracts.Siport.Cliente.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.Siport.Cliente
{
    public class ObtenerClienteQuery : IQueryHandler<ObtenerClienteParameter>
    {
        public QueryResult Handle(ObtenerClienteParameter parameters)
        {
            if (parameters == null) { throw new ArgumentNullException("Parámetro parameters es nulo."); }
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("pCodigoCliente", dbType: DbType.String, value: parameters.CodigoCliente);

                var resultado = connection.Query<ObtenerClienteResult>
                (
                    "OPERACIONES.SP_OBTENERCLIENTE",
                    parametros,
                    commandType: CommandType.StoredProcedure
                ).LastOrDefault();

                return resultado;
            }
        }
    }
}
