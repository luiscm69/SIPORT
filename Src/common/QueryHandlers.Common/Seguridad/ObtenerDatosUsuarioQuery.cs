
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
    public class ObtenerDatosUsuarioQuery : IQueryHandler<ObtenerDatosUsuarioParameter>
    {
        public QueryResult Handle(ObtenerDatosUsuarioParameter parameters)
        {
            using (var connection = ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("codigousuario", dbType: DbType.String, value: parameters.CodigoUsuario);

                var resultado = new ObtenerDatosUsuarioResult();
                var multiquery = connection.QueryMultiple
                    (
                         "SEGURIDAD.SP_OBTENERDATAUSUARIO", 
                         parametros, 
                         commandType: CommandType.StoredProcedure
                    );
                resultado = multiquery.Read<ObtenerDatosUsuarioResult>().LastOrDefault();
                if (resultado != null)
                {
                    var collectionperfil = multiquery.Read<ObtenerDatosPerfil>().ToList<ObtenerDatosPerfil>();
                    resultado.Perfiles = collectionperfil;
                }
                return resultado;
            }
        }
    }
}
