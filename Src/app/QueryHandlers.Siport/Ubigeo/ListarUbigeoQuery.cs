
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Siport.Ubigeo.Parameter;
using QueryContracts.Siport.Ubigeo.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
namespace QueryHandlers.Siport.Ubigeo
{
    public class ListarUbigeoQuery : IQueryHandler<ListarUbigeoParameter>
    {
        public QueryResult Handle(ListarUbigeoParameter parameters)
        {
            if (parameters == null) { throw new ArgumentNullException("Parámetro parameters es nulo."); }
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("pTipoUbigeo", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.TipoUbigeo);
                parametros.Add("pCodigoUbigeo", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.CodigoUbigeo);

                var resultado = new ListarUbigeoResult
                {
                    Hits = connection.Query<ListarUbigeoDto>
                        (
                            "OPERACIONES.SP_LISTARUBIGEO",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
