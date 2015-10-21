
using Componentes.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Web.Routing;
using System.Security.Cryptography;
using System.IO;
namespace Web.Common.HtmlHelpers
{
    public static partial class Helpers
    {
        public static MvcHtmlString EncodedActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            string vQueryString = string.Empty;
            string vHtmlAttributesString = string.Empty;
            string vAreaName = string.Empty;
            string vControllerName = string.IsNullOrEmpty(controllerName) ? htmlHelper.ViewContext.RouteData.Values["controller"].ToString() : controllerName;
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            if (routeValues != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(routeValues);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    
                    string elementName = d.Keys.ElementAt(i).ToLower();
                    if (elementName == "area")
                    {
                        vAreaName = Convert.ToString(d.Values.ElementAt(i));
                        continue;
                    }
                    if (i > 0)
                    {
                        vQueryString += "?";
                    }
                    vQueryString += d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }

            if (htmlAttributes != null){
                RouteValueDictionary d = new RouteValueDictionary(htmlAttributes);
                for (int i = 0; i < d.Keys.Count; i++){
                    vHtmlAttributesString += " " + d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }

            StringBuilder ancor = new StringBuilder();
            ancor.Append("<a ");
            if (!string.IsNullOrEmpty(vHtmlAttributesString))
            {
                ancor.Append(vHtmlAttributesString);
            }

            ancor.Append(" href='");
            if (!string.IsNullOrEmpty(vAreaName))
                ancor.Append("/" + vAreaName);

            if (!string.IsNullOrEmpty(vControllerName))
                ancor.Append("/" + vControllerName);

            if (actionName != "Index")
                ancor.Append("/" + actionName);


            if (!string.IsNullOrEmpty(vQueryString))
                ancor.Append("?q=" + EncriptadorExtensions.EncryptText(vQueryString));

            ancor.Append("'");
            ancor.Append(">");
            ancor.Append(linkText);
            ancor.Append("</a>");
            return new MvcHtmlString(ancor.ToString());
        }
    }
}
