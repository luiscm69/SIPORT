

using CommandContracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace CommandContracts.Siport.OrdenServicio
{
    public class DetalleInsertarEditarOrdSrvCommand : Command
    {
        public double? IdOrdenServicio { get; set; }

        [DisplayName("Código")]
        public string CodigoOrdServicio { get; set; }

        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime LogFecha { get; set; }
        public string LogUsuario { get; set; }
        public double IdClientes { get; set; }

        [DisplayName("Cliente")]
        public string CodigoCliente { get; set; }
        public string NombreRazonSocial { get; set; }

        public IEnumerable<OrdenServicioDestinoCommand> ListaDestinosCommand { get; set; }

    }

    public class OrdenServicioDestinoCommand
    {
        public double? IdOrdenServicioDestino { get; set; }
        public string CodigoUbigeo { get; set; }

        public string DescripcionUbigeo { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? LogFecha { get; set; }
        public string LogUsuario { get; set; }
        public DateTime? FechaEstEntrega { get; set; }
        public DateTime? FechaRealDespacho { get; set; }
        public double IdHorarioEntrega { get; set; }
        
        public string Direccion { get; set; }
        
        public string Referencia { get; set; }
        public string Estado { get; set; }
        public string DesHorarioEntrega { get; set; }
                       


        public double? Latitude { get; set; }
        public double? Longitude { get; set; }





    }
}
