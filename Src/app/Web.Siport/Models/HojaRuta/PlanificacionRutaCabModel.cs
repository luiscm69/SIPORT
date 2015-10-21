using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;


namespace Web.Siport.Models.HojaRuta
{
    public class PlanificacionRutaCabModel
    {
        public string IdGuid { get; set; }
        public DateTime FechaUltimoUso { get; set; }

        public SelectList SelectListVehiculosDisponibles { get; set; }
        public DateTime FechaOrdenServicioFiltro { get; set; }
        public double IdVehiculoDisponible { get; set; }
        
        public double? IdPlanHojaDeRutaCab { get; set; }
        public string CodigoPlanificacion { get; set; }
        public string ComentarioPlanificacion { get; set; }

        public IList<PlanificacionRutaDetModel> ListadoPlanificacionDet { get; set; }
    }


    public static class PlanificacionRutaCabConfig
    {
        internal const string _MODELOSESSION = "SessionPlanificacionRutaCabModel";

        public static PlanificacionRutaCabModel GetModelo(string pGuid)
        {
            var vSession = HttpContext.Current.Session;

            if (string.IsNullOrEmpty(pGuid)) throw new ArgumentException("Tiene que ingresar el identificador del modelo.");
            IList<PlanificacionRutaCabModel> vListaModelo = (List<PlanificacionRutaCabModel>)vSession[PlanificacionRutaCabConfig._MODELOSESSION];
            if (vListaModelo != null && vListaModelo.Any())
            {
                var modelo = vListaModelo.LastOrDefault(x => x.IdGuid == pGuid);
                if (modelo == null)
                    return null;

                modelo.FechaUltimoUso = DateTime.Now;
                return modelo;
            }
            return null;
        }

        public static string SetModelo(PlanificacionRutaCabModel pModelo)
        {
            var vSession = HttpContext.Current.Session;
            if (pModelo == null) throw new ArgumentException("Se tiene que ingresar un modelo");

            List<PlanificacionRutaCabModel> vListaModelo = null;
            string vIdGuid = Guid.NewGuid().ToString();
            int idx = -1;

            //Obteniendo la collection del Modelo de PlanTipoCobro.
            vListaModelo = (List<PlanificacionRutaCabModel>)vSession[PlanificacionRutaCabConfig._MODELOSESSION];
            if (vListaModelo == null)
                vListaModelo = new List<PlanificacionRutaCabModel>();

            PlanificacionRutaCabModel vModeloEncontrado = null;

            //verificar que el modelo no exista.
            if (!string.IsNullOrEmpty(pModelo.IdGuid))
            {
                idx = vListaModelo.FindIndex(x => x.IdGuid == pModelo.IdGuid && string.IsNullOrEmpty(x.IdGuid) == false);
            }
            else if (pModelo.IdPlanHojaDeRutaCab.HasValue)
            {
                idx = vListaModelo.FindIndex(x => x.IdPlanHojaDeRutaCab == pModelo.IdPlanHojaDeRutaCab);
            }


            if (idx >= 0)
            {
                vIdGuid = vListaModelo[idx].IdGuid;
                pModelo.IdGuid = vIdGuid;
                pModelo.FechaUltimoUso = DateTime.Now;
                vListaModelo[idx] = pModelo;
            }
            else
            {
                pModelo.IdGuid = vIdGuid;
                pModelo.FechaUltimoUso = DateTime.Now;
                vListaModelo.Add(pModelo);
            }
            CleanModelo();
            vSession[PlanificacionRutaCabConfig._MODELOSESSION] = vListaModelo;
            return vIdGuid;

        }

        public static void RemoveModelo(string pGuid)
        {
            var vSession = HttpContext.Current.Session;
            IList<PlanificacionRutaCabModel> vListaModelo = (List<PlanificacionRutaCabModel>)vSession[PlanificacionRutaCabConfig._MODELOSESSION];
            if (vListaModelo != null && vListaModelo.Any())
            {
                var modelo = vListaModelo.LastOrDefault(x => x.IdGuid == pGuid);
                if (modelo != null)
                    vListaModelo.Remove(modelo);
                vSession[PlanificacionRutaCabConfig._MODELOSESSION] = vListaModelo;
            }
            CleanModelo();
        }

        public static void CleanModelo()
        {
            var vSession = HttpContext.Current.Session;
            TimeSpan ts = new TimeSpan(0, 10, 0);
            IList<PlanificacionRutaCabModel> vListaModelo = (List<PlanificacionRutaCabModel>)vSession[PlanificacionRutaCabConfig._MODELOSESSION];
            IList<PlanificacionRutaCabModel> vListaModeloCopy;

            if (vListaModelo != null && vListaModelo.Any())
            {
                vListaModeloCopy = new List<PlanificacionRutaCabModel>(vListaModelo);
                foreach (var modelo in vListaModeloCopy.Where(x => DateTime.Now.Subtract(x.FechaUltimoUso) >= ts))
                    vListaModelo.Remove(modelo);

                vSession[PlanificacionRutaCabConfig._MODELOSESSION] = vListaModelo;
            }
        }


    }
}