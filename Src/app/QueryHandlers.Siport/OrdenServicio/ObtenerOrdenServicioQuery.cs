
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
    public class ObtenerOrdenServicioQuery : IQueryHandler<ObtenerOrdenServicioParameter>
    {
        public QueryResult Handle(ObtenerOrdenServicioParameter parameters)
        {
            using (var connection = ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("pCodigoOrdServicio", dbType: DbType.String, value: parameters.CodigoOrdenServicio);

                var resultado = new ObtenerOrdenServicioResult();
                var multiquery = connection.QueryMultiple
                    (
                         "OPERACIONES.SP_OBTENERORDSRV",
                         parametros,
                         commandType: CommandType.StoredProcedure
                    );
                resultado = multiquery.Read<ObtenerOrdenServicioResult>().LastOrDefault();
                if (resultado != null)
                {
                    var collectiondestino = multiquery.Read<OrdenServicioDestinoDto>().ToList<OrdenServicioDestinoDto>();
                    resultado.ListadoOrdenServicioDestino = collectiondestino;
                }
                return resultado;
            }
        }
    }
}
