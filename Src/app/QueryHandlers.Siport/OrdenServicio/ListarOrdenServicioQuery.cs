﻿
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Siport.OrderServicio.Parameter;
using QueryContracts.Siport.OrderServicio.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;

namespace QueryHandlers.Siport.OrdenServicio
{
    public class ListarOrdenServicioQuery : IQueryHandler<ListarOrdenServicioParameter>
    {

        public QueryResult Handle(ListarOrdenServicioParameter parameters)
        {
            if (parameters == null) { throw new ArgumentNullException("Parámetro parameters es nulo."); }
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                var resultado = new ListarOrdenServicioResult
                {
                    Hits = connection.Query<ListarOrdenServicioDto>
                        (
                            "OPERACIONES.SP_LISTARORDENESSERVICIO",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
