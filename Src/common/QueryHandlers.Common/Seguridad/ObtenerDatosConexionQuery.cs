
using QueryContracts.Common;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Linq;
using Data.Common;

namespace QueryHandlers.Common
{
    public class ObtenerDatosConexionQuery : IQueryHandler<ObtenerDatosConexionParameter>
    {
        public QueryResult Handle(ObtenerDatosConexionParameter parameters)
        {
            using (var connection = ConnectionFactory.CreateFromSecuritySession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("CODIGOUSUARIO", dbType: DbType.String, value: parameters.CodigoUsuario);
                parametros.Add("IDPERFIL", dbType: DbType.Int64, value: parameters.IdPerfil);
                    
                var result = connection.Query<ObtenerDatosConexionResult>(
                       "SEGURIDAD.SP_GETDATCNX", parametros, commandType: CommandType.StoredProcedure
                    ).LastOrDefault();

                return result;
            }

        }
    }
}
