using CommandContracts.Siport.OrdenServicio;
using System;
using System.Collections.Generic;
using System.Web;
using Trirand.Web.Mvc;
using System.Linq;
using System.Web.UI.WebControls;

namespace Web.Siport.Models.OrdenServicio
{
    public class DetalleInsertarEditarOrdSrvModel : DetalleInsertarEditarOrdSrvCommand
    {
        public DetalleInsertarEditarOrdSrvModel()
        {
            GridDestinosEntrega = DetalleInsertarEditarOrdSrvConfig.JQGridConfig();
        }

        public JQGrid GridDestinosEntrega { get; set; }
        public string IdGuid { get; set; }
        public DateTime FechaUltimoUso { get; set; }
    }

    public static class DetalleInsertarEditarOrdSrvConfig
    {
        internal const string _MODELOSESSION = "SessionDetalleInsertarEditarOrdSrvModel";
        public static JQGrid JQGridConfig()
        {
            var jqgrid = new JQGrid()
            {
                Columns = new List<JQGridColumn>() 
                {
                    new JQGridColumn{ DataField = "IdOrdenServicioDestino", PrimaryKey = true, Visible = false},
                    new JQGridColumn{ DataField = "Direccion", HeaderText = "Dirección", Width = 200 },
                    new JQGridColumn{ DataField = "Referencia", HeaderText = "Referencia", Width = 200 },
                    new JQGridColumn{ DataField = "DescripcionUbigeo", HeaderText = "Ubigeo", Width = 200 },
                    new JQGridColumn{ DataField = "FechaEstEntrega", HeaderText = "Fecha de Entrega", Width=100 },
                    new JQGridColumn{ DataField = "DesHorarioEntrega", HeaderText = "Horario de Entrega", Width = 120 },
                    new JQGridColumn{ DataField = "Estado", HeaderText = "Estado", Width = 100 },
                },
                Width = Unit.Percentage(100),
                ShrinkToFit = true,
                PagerSettings = JQGridConstantes.GetPagerSettingTotal(),
                ToolBarSettings = JQGridConstantes.GetToolBarSettingsTotal(),
                

            };
            return jqgrid;
        }

        public static DetalleInsertarEditarOrdSrvModel GetModeloDetalleInsertarEditarOrdSrv(string pGuid)
        {
            var vSession = HttpContext.Current.Session;

            if (string.IsNullOrEmpty(pGuid)) throw new ArgumentException("Tiene que ingresar el identificador del modelo.");
            IList<DetalleInsertarEditarOrdSrvModel> vListaModelo = (List<DetalleInsertarEditarOrdSrvModel>)vSession[DetalleInsertarEditarOrdSrvConfig._MODELOSESSION];
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

        public static string SetModeloDetalleInsertarEditarOrdSrv(DetalleInsertarEditarOrdSrvModel pModelo)
        {
            var vSession = HttpContext.Current.Session;
            if (pModelo == null) throw new ArgumentException("Se tiene que ingresar un modelo");

            List<DetalleInsertarEditarOrdSrvModel> vListaModelo = null;
            string vIdGuid = Guid.NewGuid().ToString();
            int idx = -1;

            //Obteniendo la collection del Modelo de PlanTipoCobro.
            vListaModelo = (List<DetalleInsertarEditarOrdSrvModel>)vSession[DetalleInsertarEditarOrdSrvConfig._MODELOSESSION];
            if (vListaModelo == null)
                vListaModelo = new List<DetalleInsertarEditarOrdSrvModel>();

            DetalleInsertarEditarOrdSrvModel vModeloEncontrado = null;

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
            vSession[DetalleInsertarEditarOrdSrvConfig._MODELOSESSION] = vListaModelo;
            return vIdGuid;

        }

        public static void RemoveModeloDetalleInsertarEditarOrdSrv(string pGuid)
        {
            var vSession = HttpContext.Current.Session;
            IList<DetalleInsertarEditarOrdSrvModel> vListaModelo = (List<DetalleInsertarEditarOrdSrvModel>)vSession[DetalleInsertarEditarOrdSrvConfig._MODELOSESSION];
            if (vListaModelo != null && vListaModelo.Any())
            {
                var modelo = vListaModelo.LastOrDefault(x => x.IdGuid == pGuid);
                if (modelo != null)
                    vListaModelo.Remove(modelo);
                vSession[DetalleInsertarEditarOrdSrvConfig._MODELOSESSION] = vListaModelo;
            }
            CleanModeloDetalleInsertarEditarOrdSrv();
        }

        public static void CleanModeloDetalleInsertarEditarOrdSrv()
        {
            var vSession = HttpContext.Current.Session;
            TimeSpan ts = new TimeSpan(0, 10, 0);
            IList<DetalleInsertarEditarOrdSrvModel> vListaModelo = (List<DetalleInsertarEditarOrdSrvModel>)vSession[DetalleInsertarEditarOrdSrvConfig._MODELOSESSION];
            IList<DetalleInsertarEditarOrdSrvModel> vListaModeloCopy;

            if (vListaModelo != null && vListaModelo.Any())
            {
                vListaModeloCopy = new List<DetalleInsertarEditarOrdSrvModel>(vListaModelo);
                foreach (var modelo in vListaModeloCopy.Where(x => DateTime.Now.Subtract(x.FechaUltimoUso) >= ts))
                    vListaModelo.Remove(modelo);

                vSession[DetalleInsertarEditarOrdSrvConfig._MODELOSESSION] = vListaModelo;
            }
        }


    }

}