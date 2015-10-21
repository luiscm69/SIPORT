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
    public class ListarOrdenServicioDisponibleQuery : IQueryHandler<ListarOrdenServicioDisponibleParameter>
    {
        public QueryResult Handle(ListarOrdenServicioDisponibleParameter parameters)
        {
            if (parameters == null) { throw new ArgumentNullException("Parámetro parameters es nulo."); }
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("pFechaCreacion", dbType: DbType.Date, direction: ParameterDirection.Input, value: parameters.FechaCreacion);

                var resultado = new ListarOrdenServicioDisponibleResult
                {
                    Hits = connection.Query<ListarOrdenServicioDisponibleDto>
                        (
                            "OPERACIONES.SP_LISTARORDSRVDESTINOXFECHA",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;

            }
        }
    }
}
