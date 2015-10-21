using System;
namespace QueryContracts.Common
{
    public class ObtenerDatosConexionResult : QueryResult
    {
        public long IdPerfil { get; set; }
        public string NombrePerfil { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
       
    }
}
