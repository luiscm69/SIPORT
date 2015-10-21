using System.Web.Mvc;

namespace Web.Siport.Controllers
{
    using AutoMapper;
    using CommandContracts.Siport.OrdenServicio;
    using CommandContracts.Siport.OrdenServicio.Outputs;
    using QueryContracts.Siport.OrderServicio.Result;
    using System;
    using System.Collections.Generic;
    using System.Web.Routing;
    using Web.Common.Controllers;
    using Web.Siport.DataAccess;
    using Web.Siport.Models;
    using Web.Siport.Models.OrdenServicio;
    using System.Linq;
    using JqGridComponent.Extensions;


    [AllowAnonymous]
    public class OrdenServicioController : BaseController
    {
        //
        // GET: /OrdenServicio/
                
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /OrdenServicio/Details/5

        public ActionResult Detalle(int id)
        {
            return View();
        }

        //
        // GET: /OrdenServicio/Create

        public ActionResult Insertar()
        {
            return View(GetDetalleInsertarEditarModel());
        }

        [HttpPost]
        public ActionResult Insertar(DetalleInsertarEditarOrdSrvModel modelo, FormCollection formcollection)
        {
            var modelosession = DetalleInsertarEditarOrdSrvConfig.GetModeloDetalleInsertarEditarOrdSrv(modelo.IdGuid);
            if (modelosession == null)
                throw new NullReferenceException("La session del modelo se ha perdido.");

            modelo.LogFecha = DateTime.Now;
            modelo.LogUsuario = "gcuentas";

            Mapper.CreateMap<OrdenServicioDestinoCommand, OrdenServicioDestinoCommand>();
            Mapper.CreateMap<DetalleInsertarEditarOrdSrvModel, DetalleInsertarEditarOrdSrvCommand>()
                .ForMember(s => s.ListaDestinos, m => m.MapFrom(
                    q => Mapper.Map<IEnumerable<OrdenServicioDestinoCommand>, IEnumerable<OrdenServicioDestinoCommand>>(q.ListaDestinos)
                ));

            if (!this.ModelState.IsValid)
                return this.View(modelo);

            var comando = Mapper.Map<DetalleInsertarEditarOrdSrvModel, DetalleInsertarEditarOrdSrvCommand>(modelo);
            var resultadocomando = this.ExecuteCommand(this.ControllerContext, comando);
            var output = resultadocomando.CommandResult as DetalleInsertarEditarOrdSrvOutput;

            if (output == null) return this.View(modelo);

            modelo.IdOrdenServicio = output.IdOrdenServicio;
            DetalleInsertarEditarOrdSrvConfig.SetModeloDetalleInsertarEditarOrdSrv(modelo);

            this.TempData["MensajeHttpPost"] = "Los cambios han sido guardados satisfactoriamente";
            return this.RedirectToAction("Editar", null, new { codigo = modelo.CodigoOrdServicio });

        }

        public ActionResult Editar(string codigo)
        {
            var resultado = DAOrdenServicio.GetOrdenServicio(codigo);
            return View(GetDetalleInsertarEditarModel(resultado));
        }

        [HttpPost]
        public ActionResult Editar(DetalleInsertarEditarOrdSrvModel modelo, FormCollection formcollection)
        {
            var modelosession = DetalleInsertarEditarOrdSrvConfig.GetModeloDetalleInsertarEditarOrdSrv(modelo.IdGuid);
            if (modelosession == null)
                throw new NullReferenceException("La session del modelo se ha perdido.");

            modelo.LogFecha = DateTime.Now;
            modelo.LogUsuario = "gcuentas";

            Mapper.CreateMap<OrdenServicioDestinoCommand, OrdenServicioDestinoCommand>();
            Mapper.CreateMap<DetalleInsertarEditarOrdSrvModel, DetalleInsertarEditarOrdSrvCommand>()
                .ForMember(s => s.ListaDestinos, m => m.MapFrom(
                    q => Mapper.Map<IEnumerable<OrdenServicioDestinoCommand>, IEnumerable<OrdenServicioDestinoCommand>>(q.ListaDestinos)
                ));

            if (!this.ModelState.IsValid)
                return this.View(modelo);

            var comando = Mapper.Map<DetalleInsertarEditarOrdSrvModel, DetalleInsertarEditarOrdSrvCommand>(modelo);
            var resultadocomando = this.ExecuteCommand(this.ControllerContext, comando);
            var output = resultadocomando.CommandResult as DetalleInsertarEditarOrdSrvOutput;

            if (output == null) return this.View(modelo);

            modelo.IdOrdenServicio = output.IdOrdenServicio;
            DetalleInsertarEditarOrdSrvConfig.SetModeloDetalleInsertarEditarOrdSrv(modelo);

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

        public ActionResult PlanificarRuta()
        {
            return null;
        }

        private DetalleInsertarEditarOrdSrvModel GetDetalleInsertarEditarModel()
        {
            return GetDetalleInsertarEditarModel(null);
        }

        private DetalleInsertarEditarOrdSrvModel GetDetalleInsertarEditarModel(ObtenerOrdenServicioResult pdetalle)
        {
            var modelo = new DetalleInsertarEditarOrdSrvModel();
            if (pdetalle != null)
            {
                Mapper.CreateMap<OrdenServicioDestinoDto, OrdenServicioDestinoCommand>();
                Mapper.CreateMap<ObtenerOrdenServicioResult, DetalleInsertarEditarOrdSrvModel>()
                    .ForMember(s => s.ListaDestinos, m => m.MapFrom(
                        q => Mapper.Map<IEnumerable<OrdenServicioDestinoDto>, IEnumerable<OrdenServicioDestinoCommand>>(q.ListadoOrdenServicioDestino)
                     ));
                modelo = Mapper.Map<ObtenerOrdenServicioResult, DetalleInsertarEditarOrdSrvModel>(pdetalle);
            }

            var vGuid = DetalleInsertarEditarOrdSrvConfig.SetModeloDetalleInsertarEditarOrdSrv(modelo);
            modelo.GridDestinosEntrega.DataUrl = Url.Action(BaseModel.JsonDataUrl.OrdenServicioDestino_JsonListarOrdenServicioDestino, null, new { pGuid = vGuid });

            return modelo;
        }

        public JsonResult JsonListarOrdenServicioDestino(string pGuid, string sidx, string sord, int page, int rows)
        {
            if (string.IsNullOrEmpty(pGuid)) throw new ArgumentException("Se requiere el Guid del modelo");
            var modelo = DetalleInsertarEditarOrdSrvConfig.GetModeloDetalleInsertarEditarOrdSrv(pGuid);

            if (modelo.ListaDestinos == null)
                modelo.ListaDestinos = new List<OrdenServicioDestinoCommand>();
            var listadoTotal = modelo.ListaDestinos.ToList();

            int pageindex = page - 1;
            int pagesize = rows;
            int totalrecord = listadoTotal.Count();
            var totalpage = (int)Math.Ceiling((float)totalrecord / (float)rows);
            
            if(sord.ToUpper() == "DESC")
            {
                listadoTotal = listadoTotal.OrderByDescending(s=>s.Direccion).ToList();
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

        public ActionResult AgregarInsertar(){
            return PartialView("_OrdenServicioDestino");
        }

    }
}
