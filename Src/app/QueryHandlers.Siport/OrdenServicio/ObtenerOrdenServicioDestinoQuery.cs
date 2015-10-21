
using QueryContracts.Common;
using QueryContracts.Siport.OrderServicio.Parameter;
using QueryHandlers.Common;
using Data.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using QueryContracts.Siport.OrderServicio.Result;
using System.Linq;

namespace QueryHandlers.Siport.OrdenServicio
{
    public class ObtenerOrdenServicioDestinoQuery : IQueryHandler<ObtenerOrdenServicioDestinoParameter>
    {
        public QueryResult Handle(ObtenerOrdenServicioDestinoParameter parameters)
        {
            using (var connection = ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("pIdOrdenServicioDestino", dbType: DbType.Int64, value: parameters.IdOrdenServicioDestino);

                var resultado = connection.Query<ObtenerOrdenServicioDestinoResult>
                        (
                            "OPERACIONES.SP_OBTENERORDENSERVICIODEST",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();
                return resultado;
            }
        }
    }
}
