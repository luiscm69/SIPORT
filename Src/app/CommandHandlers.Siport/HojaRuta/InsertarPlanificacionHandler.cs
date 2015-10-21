using CommandContracts.Common;
using CommandContracts.Siport.HojaRuta;
using CommandHandlers.Common;
using Componentes.Common.Utilidades;
using Domain.Common.Contracts;
using Domain.Siport.HojaRuta;
namespace CommandHandlers.Siport.HojaRuta
{
    public class InsertarPlanificacionHandler : ICommandHandler<InsertarPlanificacionCommand>
    {
        private readonly IRepository<PlanHojadeRutaCab> _hojaderuta;
        public InsertarPlanificacionHandler(IRepository<PlanHojadeRutaCab> phojaderuta)
        {
            this._hojaderuta = phojaderuta;
        }

        public CommandResult Handle(InsertarPlanificacionCommand command)
        {
            var dominio = new PlanHojadeRutaCab();
            if (command.IdPlanHojadeRutaCab.HasValue)
                dominio = _hojaderuta.Get(Utilidades.Cast<long>(command.IdPlanHojadeRutaCab));

            dominio.CodigoPlanificacion  = command.CodigoPlanificacion;
            dominio.ComentarioPlanificacion = command.ComentarioPlanificacion;

            //if (command.ListaPlanHojaRuta == null)
            //{ 
            
            //}

            if (command.IdPlanHojadeRutaCab.HasValue == false)
            {
                _hojaderuta.Add(dominio);
            }

            return null;

        }
    }
}
