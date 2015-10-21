
using CommandContracts.Common;
using CommandContracts.Siport.OrdenServicio;
using CommandContracts.Siport.OrdenServicio.Outputs;
using CommandHandlers.Common;
using Componentes.Common.Utilidades;
using Domain.Common.Contracts;
using Domain.Siport.OrdenServicio;
using System;
using System.Collections.Generic;
using Dominio = Domain.Siport.OrdenServicio;
using System.Linq;

namespace CommandHandlers.Siport.OrdenServicio
{
    public class DetalleInsertarEditarOrdSrvHandler : ICommandHandler<DetalleInsertarEditarOrdSrvCommand>
    {
        private readonly IRepository<Dominio.OrdenServicio> _ordenservicio;
        public DetalleInsertarEditarOrdSrvHandler(IRepository<Dominio.OrdenServicio> pordenservicio)
        {
            this._ordenservicio = pordenservicio;
        }

        public CommandResult Handle(DetalleInsertarEditarOrdSrvCommand command)
        {
            var dominio = new Dominio.OrdenServicio();
            if (command.IdOrdenServicio.HasValue)
                dominio = _ordenservicio.Get(Utilidades.Cast<long>(command.IdOrdenServicio));

            dominio.IdClientes = command.IdClientes;
            dominio.CodigoOrdServicio = command.CodigoOrdServicio;
            dominio.Comentario = command.Comentario;
            dominio.Descripcion = command.Descripcion;
            dominio.LogFecha = DateTime.Now;
            dominio.LogUsuario = command.LogUsuario;

            if (command.ListaDestinosCommand != null)
            {
                if(dominio.ListaDestinos == null) dominio.ListaDestinos = new List<OrdenServicioDestino>();
                foreach (var destino in command.ListaDestinosCommand)
                {
                    var dominiodestino = destino.IdOrdenServicioDestino.HasValue ? dominio.ListaDestinos.Where(x => x.ID == destino.IdOrdenServicioDestino.Value).Last() : new OrdenServicioDestino();
                    
                    dominiodestino.Estado = destino.Estado;
                    dominiodestino.LogFecha = DateTime.Now;
                    dominiodestino.LogUsuario = command.LogUsuario;
                    
                    if (destino.IdOrdenServicioDestino.HasValue == false){
                        dominiodestino.Direccion = destino.Direccion;
                        dominiodestino.FechaCreacion = DateTime.Now;
                        dominiodestino.FechaEstEntrega = destino.FechaEstEntrega;
                        dominiodestino.IdHorarioEntrega = destino.IdHorarioEntrega;
                        dominiodestino.CodigoUbigeo = destino.CodigoUbigeo;
                        dominiodestino.Latitude = destino.Latitude;
                        dominiodestino.Longitude = destino.Longitude;
                        dominiodestino.Referencia = destino.Referencia;
                        dominiodestino.UsuarioCreacion = command.LogUsuario;
                        dominio.ListaDestinos.Add(dominiodestino);
                    }
                }
            }

            if (command.IdOrdenServicio.HasValue == false)
            {
                dominio.FechaCreacion = command.FechaCreacion;
                dominio.UsuarioCreacion = command.UsuarioCreacion;
                _ordenservicio.Add(dominio);
            }
            _ordenservicio.Commit();
            return new DetalleInsertarEditarOrdSrvOutput() { IdOrdenServicio = dominio.ID };
        }
    }
}
