
using Domain.Common;
using System;
using System.Collections.Generic;
namespace Domain.Siport.OrdenServicio
{
    public class OrdenServicio : Entity
    {
        public string CodigoOrdServicio { get; set; }
        public string Descripcion { get; set; }
        public string Comentario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? LogFecha { get; set; }
        public string LogUsuario { get; set; }
        public double IdClientes { get; set; }
        public virtual IList<OrdenServicioDestino> ListaDestinos { get; set; }
    }
}
