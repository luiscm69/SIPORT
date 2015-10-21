using CommandContracts.Siport.OrdenServicio;
using System;
using System.Collections.Generic;
using System.Web;
using Trirand.Web.Mvc;
using System.Linq;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace Web.Siport.Models.OrdenServicio
{
    public class OrdenServicioCabeceraModel 
    {
        public string IdGuid { get; set; }
        public DateTime FechaUltimoUso { get; set; }
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
        public IList<OrdenServicioDestinoModel> ListaDestinos { get; set; }

    
    }

    public static class OrdenServicioCabeceraConfig
    {
        internal const string _MODELOSESSION = "SessionDetalleInsertarEditarOrdSrvModel";
  
        public static OrdenServicioCabeceraModel GetModeloDetalleInsertarEditarOrdSrv(string pGuid)
        {
            var vSession = HttpContext.Current.Session;

            if (string.IsNullOrEmpty(pGuid)) throw new ArgumentException("Tiene que ingresar el identificador del modelo.");
            IList<OrdenServicioCabeceraModel> vListaModelo = (List<OrdenServicioCabeceraModel>)vSession[OrdenServicioCabeceraConfig._MODELOSESSION];
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

        public static string SetModeloDetalleInsertarEditarOrdSrv(OrdenServicioCabeceraModel pModelo)
        {
            var vSession = HttpContext.Current.Session;
            if (pModelo == null) throw new ArgumentException("Se tiene que ingresar un modelo");

            List<OrdenServicioCabeceraModel> vListaModelo = null;
            string vIdGuid = Guid.NewGuid().ToString();
            int idx = -1;

            //Obteniendo la collection del Modelo de PlanTipoCobro.
            vListaModelo = (List<OrdenServicioCabeceraModel>)vSession[OrdenServicioCabeceraConfig._MODELOSESSION];
            if (vListaModelo == null)
                vListaModelo = new List<OrdenServicioCabeceraModel>();

            OrdenServicioCabeceraModel vModeloEncontrado = null;

            //verificar que el modelo no exista.
            if (!string.IsNullOrEmpty(pModelo.IdGuid))
            {
                idx = vListaModelo.FindIndex(x => x.IdGuid == pModelo.IdGuid && string.IsNullOrEmpty(x.IdGuid) == false);
            }
            else if (pModelo.IdOrdenServicio.HasValue)
            {
                idx = vListaModelo.FindIndex(x => x.IdOrdenServicio == pModelo.IdOrdenServicio);
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
            CleanModeloDetalleInsertarEditarOrdSrv();
            vSession[OrdenServicioCabeceraConfig._MODELOSESSION] = vListaModelo;
            return vIdGuid;

        }

        public static void RemoveModeloDetalleInsertarEditarOrdSrv(string pGuid)
        {
            var vSession = HttpContext.Current.Session;
            IList<OrdenServicioCabeceraModel> vListaModelo = (List<OrdenServicioCabeceraModel>)vSession[OrdenServicioCabeceraConfig._MODELOSESSION];
            if (vListaModelo != null && vListaModelo.Any())
            {
                var modelo = vListaModelo.LastOrDefault(x => x.IdGuid == pGuid);
                if (modelo != null)
                    vListaModelo.Remove(modelo);
                vSession[OrdenServicioCabeceraConfig._MODELOSESSION] = vListaModelo;
            }
            CleanModeloDetalleInsertarEditarOrdSrv();
        }

        public static void CleanModeloDetalleInsertarEditarOrdSrv()
        {
            var vSession = HttpContext.Current.Session;
            TimeSpan ts = new TimeSpan(0, 10, 0);
            IList<OrdenServicioCabeceraModel> vListaModelo = (List<OrdenServicioCabeceraModel>)vSession[OrdenServicioCabeceraConfig._MODELOSESSION];
            IList<OrdenServicioCabeceraModel> vListaModeloCopy;

            if (vListaModelo != null && vListaModelo.Any())
            {
                vListaModeloCopy = new List<OrdenServicioCabeceraModel>(vListaModelo);
                foreach (var modelo in vListaModeloCopy.Where(x => DateTime.Now.Subtract(x.FechaUltimoUso) >= ts))
                    vListaModelo.Remove(modelo);

                vSession[OrdenServicioCabeceraConfig._MODELOSESSION] = vListaModelo;
            }
        }


    }

}