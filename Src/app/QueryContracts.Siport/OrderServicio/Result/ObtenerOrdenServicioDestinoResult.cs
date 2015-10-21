using QueryContracts.Common;
namespace QueryContracts.Siport.OrderServicio.Result
{
    public class ObtenerOrdenServicioDestinoResult : QueryResult
    {
        public double? IdOrdenServicio { get; set; }
        public double? IdOrdenServicioDestino { get; set; }
        public string Codigoordservicio { get; set; }
        public string Direccion { get; set; }
        public double? IdHorarioEntrega { get; set; }
        public string DesHorarioEntrega { get; set; }
    }
}
