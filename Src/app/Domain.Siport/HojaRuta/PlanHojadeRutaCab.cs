using Domain.Common;
using System.Collections.Generic;
namespace Domain.Siport.HojaRuta
{
    public class PlanHojadeRutaCab : Entity
    {
        public string CodigoPlanificacion { get; set; }
        public string ComentarioPlanificacion { get; set; }
        public virtual IList<PlanHojadeRutaDet> ListaPlanHojaRuta { get; set; }
    }
}
