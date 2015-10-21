using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.Siport.OrderServicio.Result
{
    public class ListarHorarioEntregaResult : QueryResult
    {
        public IEnumerable<ListarHorarioEntregaDto> Hits { get; set; }
    }

    public class ListarHorarioEntregaDto
    {
        public double? IdHorarioEntrega { get; set; }
        public DateTime? HoraInicio { get; set; }
        public DateTime? HoraFin { get; set; }
        public string DesHorarioEntrega { get; set; }
    }
}
