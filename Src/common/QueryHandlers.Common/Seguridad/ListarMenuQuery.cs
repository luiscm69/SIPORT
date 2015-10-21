
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Common.Seguridad.Parameters;
using QueryContracts.Common.Seguridad.Results;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
namespace QueryHandlers.Common.Seguridad
{
    public class ListarMenuQuery : IQueryHandler<ListarMenuParameter>
    {
        public QueryResult Handle(ListarMenuParameter parameters)
        {
            if (parameters == null) { throw new ArgumentNullException("Parámetro parameters es nulo."); }
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("IDPERFIL", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.IdPerfil);

                var resultado = new ListarMenuResult
                {
                    Hits = connection.Query<ListarMenuDto>
                        (
                            "SEGURIDAD.SP_LISTARMENU",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
