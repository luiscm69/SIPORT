using Data.Common;
using QueryContracts.Common;
using QueryContracts.Siport.HojaRuta.Parameter;
using QueryContracts.Siport.HojaRuta.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
namespace QueryHandlers.Siport.HojaRuta
{
    public class ListarVehiculosDisponiblesQuery : IQueryHandler<ListarVehiculosDisponiblesParameter>
    {
        public QueryResult Handle(ListarVehiculosDisponiblesParameter parameters)
        {
            if (parameters == null) { throw new ArgumentNullException("Parámetro parameters es nulo."); }
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                //parametros.Add("pEstado", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.Estado);
                parametros.Add("pFechaEntrega", dbType: DbType.Date, direction: ParameterDirection.Input, value: parameters.FechaEntrega);

                var resultado = new ListarVehiculosDisponiblesResult
                {
                    Hits = connection.Query<ListarVehiculosDisponiblesDto>
                        (
                            "OPERACIONES.SP_LISTARVEHICULOS",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
