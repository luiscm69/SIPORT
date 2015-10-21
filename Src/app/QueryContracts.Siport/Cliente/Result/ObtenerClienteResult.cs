using QueryContracts.Common;
namespace QueryContracts.Siport.Cliente.Result
{
    public class ObtenerClienteResult : QueryResult
    {
        public double? IdClientes { get; set; }
        public string CodigoCliente { get; set; }
        public string NombreRazonSocial { get; set; }
    }
}
