using System;
using System.Collections.Generic;
namespace QueryContracts.Common.Seguridad.Results
{
    public class ObtenerDatosUsuarioResult : QueryResult
    {
        public double Idusuario { get; set; }
        public string CodigoUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public Int16? IsApproved { get; set; }
        public Int16? IsLockedout { get; set; }
        public Int16? IsOnline { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public DateTime? LastLockedOutDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public Int64? IdUsuarioPwd { get; set; }
        public string RecordatorioPwd { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaVctoPwd { get; set; }
        public IEnumerable<ObtenerDatosPerfil> Perfiles { get; set; }
    }

    public class ObtenerDatosPerfil
    {
        public double IdPerfil { get; set; }
        public string NombrePerfil { get; set; }
        public string BdUsrPerfil { get; set; }
    }

}
