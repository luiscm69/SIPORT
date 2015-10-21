using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Web.Common.Controllers;
using Web.Siport.DataAccess;
using Web.Siport.Models.HojaRuta;

namespace Web.Siport.Controllers
{
    [AllowAnonymous]
    public class HojaRutaController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IngresarPlanificacionRuta()
        { 
            return View(GetPlanificacionRutaModel());
        }

        private PlanificacionRutaCabModel GetPlanificacionRutaModel()
        {
            var modelo = new PlanificacionRutaCabModel();
            modelo.FechaOrdenServicioFiltro = DateTime.Now;
            var vehiculosdisponibles = DAHojaRuta.GetListarVehiculosDisponibles(DateTime.Now);
            SelectList selectlist = new SelectList(vehiculosdisponibles, "IdVehiculo", "NumeroPlaca");
            modelo.SelectListVehiculosDisponibles = selectlist;

            PlanificacionRutaCabConfig.SetModelo(modelo);
            return modelo;
        }
               

        public ActionResult ActualizarPlanificacionRuta()
        {
            return View();
        }

        public JsonResult JsonListarOrdenServicioDisponible(DateTime? fordsrv, string sidx, string sord, int page, int rows)
        {
            if (fordsrv.HasValue == false) fordsrv = DateTime.Now;
            var listadoTotal = DAHojaRuta.GetListarOrdenServicioDisponible(fordsrv);
           
            int pageindex = page - 1;
            int pagesize = rows;
            int totalrecord = listadoTotal.Count();
            var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

            if (sord.ToUpper() == "DESC")
            {
                listadoTotal = listadoTotal.OrderByDescending(s => s.IdOrdenServicioDestino).ToList();
                listadoTotal = listadoTotal.Skip(pageindex * pagesize).Take(pagesize).ToList();
            }
            else
            {
                listadoTotal = listadoTotal.OrderBy(s => s.IdOrdenServicioDestino).ToList();
                listadoTotal = listadoTotal.Skip(pageindex * pagesize).Take(pagesize).ToList();
            }

            var jsonData = new
            {
                total = totalpage,
                page,
                records = totalrecord,
                rows = listadoTotal
            };
            //return modelo.GridDestinosEntrega.DataBind(listadestino.AsQueryable(), listadestino.Count());
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonRutasPlanificadas(string pGuid, string sidx, string sord, int page, int rows)
        {

            if (string.IsNullOrEmpty(pGuid)) throw new ArgumentException("Se requiere el Guid del modelo");
            var modelo = PlanificacionRutaCabConfig.GetModelo(pGuid);

            if (modelo.ListadoPlanificacionDet == null)
                modelo.ListadoPlanificacionDet = new List<PlanificacionRutaDetModel>();
            var listadoTotal = modelo.ListadoPlanificacionDet.Where(x => x.Estado != "IC").ToList();

            int pageindex = page - 1;
            int pagesize = rows;
            int totalrecord = listadoTotal.Count();
            var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

            if (sord.ToUpper() == "DESC")
            {
                listadoTotal = listadoTotal.OrderByDescending(s => s.IdPlanHojaDeRutaDet).ToList();
                listadoTotal = listadoTotal.Skip(pageindex * pagesize).Take(pagesize).ToList();
            }
            else
            {
                listadoTotal = listadoTotal.OrderBy(s => s.IdPlanHojaDeRutaDet).ToList();
                listadoTotal = listadoTotal.Skip(pageindex * pagesize).Take(pagesize).ToList();
            }

            var jsonData = new
            {
                total = totalpage,
                page,
                records = totalrecord,
                rows = listadoTotal
            };
            //return modelo.GridDestinosEntrega.DataBind(listadestino.AsQueryable(), listadestino.Count());
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PlanificarRuta(Dictionary<string, string> pDatos, string pGuid)
        {
            if (string.IsNullOrEmpty(pGuid)) throw new ArgumentException("Se requiere el Guid del modelo");
            var modelo = PlanificacionRutaCabConfig.GetModelo(pGuid);

            var vIds = pDatos["pIds"];
            var vIdVehiculo = pDatos["pIdVehiculo"];
            var vPlacaVehiculo = pDatos["pPlaca"];
            var vIdsArray = vIds.Split(',');

            modelo.ListadoPlanificacionDet = new List<PlanificacionRutaDetModel>();

            for (var i = 0; i < vIdsArray.Count(); i++)
            {
                var idosd = Int64.Parse(vIdsArray[i]);
                var obj = new PlanificacionRutaDetModel();
                var res = DAOrdenServicio.GetOrdenServicioDestino(idosd);

                if (res != null)
                {
                    obj.Estado = "RE";
                    obj.FechaCreacion = DateTime.Now;
                    obj.IdOrdenServicio = res.IdOrdenServicio;
                    obj.IdOrdenServicioDestino = res.IdOrdenServicioDestino;
                    obj.IdVehiculo = Int64.Parse("0" + vIdVehiculo);
                    obj.OrdenAtencion = i + 1;
                    obj.UsuarioCreacion = "gcuentasj";
                    obj.CodigoOrdServicio = res.Codigoordservicio;
                    obj.PlacaVehiculo = vPlacaVehiculo;
                    obj.Direccion = res.Direccion;
                    obj.DesHorarioEntrega = res.DesHorarioEntrega;
                    modelo.ListadoPlanificacionDet.Add(obj);
                }
            }
            PlanificacionRutaCabConfig.SetModelo(modelo);
            
            return Json(new { res = true, msj = "se planifico las ordenes correctamente." });
        }

    }
}
