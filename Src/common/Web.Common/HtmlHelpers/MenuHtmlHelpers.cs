
using QueryContracts.Common.Seguridad.Parameters;
using QueryContracts.Common.Seguridad.Results;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using Seguridad.Common;
using ServiceAgents.Common;
using System.Text;

namespace Web.Common.HtmlHelpers
{
    public static class MenuHtmlHelpers
    {
        public static MvcHtmlString ListarMenu(this HtmlHelper helper, double idperfil)
        {
            var listamenu = ObtenerMenus(idperfil);
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            return new MvcHtmlString(ConstruirMenu(listamenu)); 
        }

        private static List<Menu> ObtenerMenus(double pidperfil)
        {
            var listamenu = ListarMenu(pidperfil);
            var resmenu = ObtenerItemsMenu(null, listamenu);
            return resmenu;
        }

        private static string ConstruirMenu(IEnumerable<Menu> opciones)
        {
            var url = string.Empty;
            var navprincipal = new TagBuilder("ul");
            navprincipal.Attributes["class"] = "nav navbar-nav";
            navprincipal.InnerHtml = ContruirItems(opciones);



            return navprincipal.ToString();
        }

        private static string ContruirItems(IEnumerable<Menu> opciones)
        {
            StringBuilder items = new StringBuilder();
            foreach (var opc in opciones)
            {
                TagBuilder navli = new TagBuilder("li");
                navli = new TagBuilder("li");
                if (opc.Items.Any())
                {
                    navli.Attributes["class"] = "dropdown";
                    
                }
                navli.InnerHtml = ConstruirLink(opc);
                items.Append(navli.ToString());
            }
            return items.ToString();
        }
        private static string ConstruirLink(Menu opc)
        {
            TagBuilder navlink = new TagBuilder("a");
            navlink.SetInnerText(opc.Nombre);
            navlink.MergeAttribute("href", opc.Url);
            if (opc.Items.Any()){
                navlink.Attributes["class"] = "dropdown-toggle";
                navlink.MergeAttribute("href", "#");
                navlink.Attributes.Add("data-toggle", "dropdown");
                navlink.Attributes.Add("role", "button");
                navlink.Attributes.Add("aria-haspopup", "true");
                navlink.Attributes.Add("aria-expanded", "false");
                navlink.InnerHtml = opc.Nombre + "<span class='caret'></span>";

            }
            
            return navlink.ToString();
        }

        private static List<Menu> ObtenerItemsMenu(double? idpadre, List<ListarMenuDto> listamenu)
        {
            var listamenures = new List<Menu>();
            foreach (var menu in listamenu.Where(x => x.IdMenuPadre == idpadre).ToList())
            {
                Menu mnu = new Menu();
                mnu.Nombre = menu.Nombre;
                mnu.Url = menu.Url;
                mnu.IdMenu = menu.IdMenu;
                mnu.Nivel = menu.Nivel;
                mnu.Items = ObtenerItemsMenu(menu.IdMenu, listamenu);
                listamenures.Add(mnu);
            }
            return listamenures;
        }

        private static List<ListarMenuDto> ListarMenu(double pidperfil)
        {
            var parametro = new ListarMenuParameter();
            parametro.IdPerfil = pidperfil;
            var res = (ListarMenuResult)parametro.Execute();
            if (res == null)
                return new List<ListarMenuDto>();
            return res.Hits.ToList();
        }

    }
}
