using AutoMapper;
using CommandContracts.Siport.OrdenServicio;
using CommandContracts.Siport.OrdenServicio.Outputs;
using QueryContracts.Siport.OrderServicio.Result;
using QueryContracts.Siport.Ubigeo.Result;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Web.Common.Controllers;
using Web.Siport.DataAccess;
using Web.Siport.Models;
using Web.Siport.Models.OrdenServicio;

namespace Web.Siport.Controllers
{
    [AllowAnonymous]
    public class OrdenServicioController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Insertar()
        {
            return View(GetDetalleInsertarEditarModel());
        }

        [HttpPost]
        public ActionResult Insertar(OrdenServicioCabeceraModel modelo, FormCollection formcollection)
        {
            var modelosession = OrdenServicioCabeceraConfig.GetModeloDetalleInsertarEditarOrdSrv(modelo.IdGuid);
            if (modelosession == null)
                throw new NullReferenceException("La session del modelo se ha perdido.");

            modelo.LogFecha = DateTime.Now;
            modelo.LogUsuario = "gcuentas";
            modelo.ListaDestinos = modelosession.ListaDestinos;

            Mapper.CreateMap<OrdenServicioDestinoModel, OrdenServicioDestinoCommand>();
            Mapper.CreateMap<OrdenServicioCabeceraModel, DetalleInsertarEditarOrdSrvCommand>()
                .ForMember(s => s.ListaDestinosCommand, m => m.MapFrom(
                    q => Mapper.Map<IEnumerable<OrdenServicioDestinoModel>, IEnumerable<OrdenServicioDestinoCommand>>(q.ListaDestinos)
                ));

            if (!this.ModelState.IsValid)
                return this.View(modelo);

            var comando = Mapper.Map<OrdenServicioCabeceraModel, DetalleInsertarEditarOrdSrvCommand>(modelo);
            var resultadocomando = this.ExecuteCommand(this.ControllerContext, comando);
            var output = resultadocomando.CommandResult as DetalleInsertarEditarOrdSrvOutput;

            if (output == null) return this.View(modelo);

            modelo.IdOrdenServicio = output.IdOrdenServicio;
            OrdenServicioCabeceraConfig.SetModeloDetalleInsertarEditarOrdSrv(modelo);

            this.TempData["MensajeHttpPost"] = "Los cambios han sido guardados satisfactoriamente";
            return this.RedirectToAction("Editar", null, new { codigo = modelo.CodigoOrdServicio });

        }

        public ActionResult Editar(string codigo)
        {
            var resultado = DAOrdenServicio.GetOrdenServicio(codigo);
            return View(GetDetalleInsertarEditarModel(resultado));
        }

        [HttpPost]
        public ActionResult Editar(OrdenServicioCabeceraModel modelo, FormCollection formcollection)
        {
            var modelosession = OrdenServicioCabeceraConfig.GetModeloDetalleInsertarEditarOrdSrv(modelo.IdGuid);
            if (modelosession == null)
                throw new NullReferenceException("La session del modelo se ha perdido.");

            modelo.LogFecha = DateTime.Now;
            modelo.LogUsuario = "gcuentas";

            Mapper.CreateMap<OrdenServicioDestinoModel, OrdenServicioDestinoCommand>();
            Mapper.CreateMap<OrdenServicioCabeceraModel, DetalleInsertarEditarOrdSrvCommand>()
                .ForMember(s => s.ListaDestinosCommand, m => m.MapFrom(
                    q => Mapper.Map<IEnumerable<OrdenServicioDestinoModel>, IEnumerable<OrdenServicioDestinoCommand>>(q.ListaDestinos)
                ));

            if (!this.ModelState.IsValid)
                return this.View(modelo);

            var comando = Mapper.Map<OrdenServicioCabeceraModel, DetalleInsertarEditarOrdSrvCommand>(modelo);
            var resultadocomando = this.ExecuteCommand(this.ControllerContext, comando);
            var output = resultadocomando.CommandResult as DetalleInsertarEditarOrdSrvOutput;

            if (output == null) return this.View(modelo);

            modelo.IdOrdenServicio = output.IdOrdenServicio;
            OrdenServicioCabeceraConfig.SetModeloDetalleInsertarEditarOrdSrv(modelo);

            this.TempData["MensajeHttpPost"] = "Los cambios han sido guardados satisfactoriamente";
            return this.RedirectToAction("Editar", null, new { codigo = modelo.CodigoOrdServicio });
        }

        public ActionResult Eliminar(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Eliminar(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private OrdenServicioCabeceraModel GetDetalleInsertarEditarModel()
        {
            return GetDetalleInsertarEditarModel(null);
        }

        private OrdenServicioCabeceraModel GetDetalleInsertarEditarModel(ObtenerOrdenServicioResult pdetalle)
        {
            var modelo = new OrdenServicioCabeceraModel();
            if (pdetalle != null)
            {
                Mapper.CreateMap<OrdenServicioDestinoDto, OrdenServicioDestinoModel>();
                Mapper.CreateMap<ObtenerOrdenServicioResult, OrdenServicioCabeceraModel>()
                    .ForMember(s => s.ListaDestinos, m => m.MapFrom(
                        q => Mapper.Map<IEnumerable<OrdenServicioDestinoDto>, IEnumerable<OrdenServicioDestinoModel>>(q.ListadoOrdenServicioDestino)
                     ));
                modelo = Mapper.Map<ObtenerOrdenServicioResult, OrdenServicioCabeceraModel>(pdetalle);
            }

            var vGuid = OrdenServicioCabeceraConfig.SetModeloDetalleInsertarEditarOrdSrv(modelo);
            //modelo.GridDestinosEntrega.DataUrl = Url.Action(BaseModel.JsonDataUrl.OrdenServicioDestino_JsonListarOrdenServicioDestino, null, new { pGuid = vGuid });

            return modelo;
        }

        public JsonResult JsonListarOrdenServicioDestino(string pGuid, string sidx, string sord, int page, int rows)
        {
            if (string.IsNullOrEmpty(pGuid)) throw new ArgumentException("Se requiere el Guid del modelo");
            var modelo = OrdenServicioCabeceraConfig.GetModeloDetalleInsertarEditarOrdSrv(pGuid);

            if (modelo.ListaDestinos == null)
                modelo.ListaDestinos = new List<OrdenServicioDestinoModel>();
            var listadoTotal = modelo.ListaDestinos.Where(x=>x.Estado != "IC").ToList();

            int pageindex = page - 1;
            int pagesize = rows;
            int totalrecord = listadoTotal.Count();
            var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

            if (sord.ToUpper() == "DESC")
            {
                listadoTotal = listadoTotal.OrderByDescending(s => s.Direccion).ToList();
                listadoTotal = listadoTotal.Skip(pageindex * pagesize).Take(pagesize).ToList();
            }
            else
            {
                listadoTotal = listadoTotal.OrderBy(s => s.Direccion).ToList();
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

        public JsonResult JsonListarUbigeo(string pCodigoUbigeo)
        {
            try
            {
                var codigo = string.IsNullOrEmpty(pCodigoUbigeo) ? string.Empty : pCodigoUbigeo;
                var tipoubigeo = "D";

                if (codigo.EndsWith("0000", false, CultureInfo.InvariantCulture))
                {
                    tipoubigeo = "P";
                }else if (codigo.EndsWith("00", false, CultureInfo.InvariantCulture))
                {
                    tipoubigeo = "V";
                }

                var resultado = DAUbigeo.ListarUbigeo(tipoubigeo, codigo);
                if (resultado == null || resultado.Hits.Any() == false)
                    return Json(new { res = false, lista = new List<ListarUbigeoDto>(), mensaje = "No se encontro ningun ubigeo" });
                return Json(new { res = true, lista = resultado.Hits, mensaje = string.Empty });

            }
            catch (Exception ex)
            {
                return Json(new { res = false, lista = new List<ListarUbigeoDto>(), mensaje = ex.Message});
            }
        }

        private static SelectList ListarDepartamentos(bool listadpt)
        {
            if (listadpt == false) return new SelectList(new List<ListarUbigeoDto>(), "CodigoUbigeo", "Descripcion");

            var resultado = DAUbigeo.ListarUbigeo();
            var listado = resultado == null ? new List<ListarUbigeoDto>() : resultado.Hits.ToList();
            return new SelectList(listado, "CodigoUbigeo", "Descripcion");
        }

        private static SelectList ListarHorarioEntrega()
        {
            var resultado = DAOrdenServicio.GetListarHorarioEntrega();
            var listado = resultado == null ? new List<ListarHorarioEntregaDto>() : resultado.Hits.ToList();
            return new SelectList(listado, "IdHorarioEntrega", "DesHorarioEntrega");
        }

        public PartialViewResult GetPartialDestinoModal()
        {
            ViewData["listaubigeodep"] = ListarDepartamentos(true);
            ViewData["listaubigeoprv"] = ListarDepartamentos(false);
            ViewData["listaubigeodis"] = ListarDepartamentos(false);
            ViewData["listahorarioentrega"] = ListarHorarioEntrega();


            return PartialView("_OrdenServicioDestino");
        }

        public JsonResult GrabarOrdenServicioDestino(string pdata, string pguid)
        {
            if (pdata == null) throw new ArgumentException("Tiene que ingresar el modelo");
            if (string.IsNullOrEmpty(pguid)) throw new ArgumentException("Tiene que ingresar el guid");

            var serializer = new JavaScriptSerializer();
            var modelo = serializer.Deserialize<OrdenServicioDestinoModel>(pdata);
            
            var modelopadresession = OrdenServicioCabeceraConfig.GetModeloDetalleInsertarEditarOrdSrv(pguid);
            if (modelopadresession == null) throw new ArgumentNullException("No se ha encontrado ningun modelo padre para el modelo OrdenServicioDestinoModel");

            modelo.Estado = "RE";
            modelo.LogFecha = DateTime.Now;
            modelo.LogUsuario = "gcuentasj";
            modelo.Longitude = 0;
            modelo.Latitude = 0;

            try { 
                modelopadresession.ListaDestinos.Add(modelo);
                return Json(new { res = true, mensaje = string.Empty });
            }
            catch (Exception ex) { return Json(new { res = false, mensaje = ex.Message }); }
        }

        public JsonResult ObtenerCliente(string pCodigoCliente)
        {
            if (string.IsNullOrEmpty(pCodigoCliente)) throw new ArgumentException("Se requiere el Codigo del Cliente");
            var resultado = DACliente.ObtenerCliente(pCodigoCliente);
            if (resultado == null) throw new ArgumentNullException("No se encontro el codigo del cliente ingresado");

            try{
                return Json(new { res = true, obj = resultado, mensaje = string.Empty });
            }
            catch (Exception ex) { return Json(new { res = false, mensaje = ex.Message }); }

            /*
                     public double? IdClientes { get; set; }
        public string CodigoCliente { get; set; }
        public string NombreRazonSocial { get; set; }
             */

        }

        public JsonResult JsonListarOrdenesServicio(string sidx, string sord, int page, int rows)
        {
            var resultado = DAOrdenServicio.GetListarOrdenServicio();
            if (resultado == null) return null;
            var listadoTotal = resultado.Hits.ToList();

            int pageindex = page - 1;
            int pagesize = rows;
            int totalrecord = listadoTotal.Count();
            var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);

            if (sord.ToUpper() == "DESC")
            {
                listadoTotal = listadoTotal.OrderByDescending(s => s.IdOrdenServicio).ToList();
                listadoTotal = listadoTotal.Skip(pageindex * pagesize).Take(pagesize).ToList();
            }
            else
            {
                listadoTotal = listadoTotal.OrderBy(s => s.IdOrdenServicio).ToList();
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

        //public static Dictionary<string, decimal> GoogleGeoCode(string address)
        //{
        //    var latLong = new Dictionary<string, decimal>();

        //    const string url = "http://maps.googleapis.com/maps/api/geocode/json?sensor=true&address=";

        //    dynamic googleResults = new Uri(url + address).GetDynamicJsonObject();

        //    foreach (var result in googleResults.results)
        //    {
        //        //Have to do a specific cast or we'll get a C# runtime binding exception
        //        var lat = (decimal)result.geometry.location.lat;
        //        var lng = (decimal)result.geometry.location.lng;

        //        latLong.Add("Lat", lat);
        //        latLong.Add("Lng", lng);
        //    }

        //    return latLong;
        //}


    }
}
