
using System;
namespace QueryContracts.Common
{
    public class ObtenerDatosConexionParameter : QueryParameter
    {
        public double? IdPerfil { get; set; }
        public string CodigoUsuario { get; set; }
    }
}
