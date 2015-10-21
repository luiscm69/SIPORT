
using QueryContracts.Common;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Linq;
using Data.Common;
using QueryContracts.Common.Seguridad.Parameters;
using QueryContracts.Common.Seguridad.Results;

namespace QueryHandlers.Common.Seguridad
{
    public class ValidarCredencialesUsuarioQuery : IQueryHandler<ValidarCredencialesUsuarioParameter>
    {

        public QueryResult Handle(ValidarCredencialesUsuarioParameter parameters)
        {
            using (var connection = ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("CODIGOUSUARIO", dbType: DbType.String, value: parameters.CodigoUsuario);
                parametros.Add("PASSWORD", dbType: DbType.String, value: parameters.PasswordUsuario);

                var result = connection.Query<ValidarCredencialesUsuarioResult>(
                       "SEGURIDAD.SP_VALIDARCREDENCIAL", parametros, commandType: CommandType.StoredProcedure
                    ).LastOrDefault();
                return result;
            }
        }
    }
}
