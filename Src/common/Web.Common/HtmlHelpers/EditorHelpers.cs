using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Common.HtmlHelpers
{
    /// <summary>
    /// Clase derivada del Helper, donde estan las funciones del TextBox.
    /// </summary>
    public static partial class Helpers
    {
        /// <summary>
        /// Enumerador que indica el tipo de dato que se ingresara en el HTML.Helper.
        /// </summary>
        public enum TipoValidacion
        {
            Numerico,
            NumeroEntero,
            NumeroDecimal,
            AlfaNumerico,
            Alfabetico,
            Fecha,
            Email,
            Horario,
            //Temporal
            NumeroEnteroSinFormato,
            NumeroEnteroGuionSlash,
            Ip,
            Porcentaje,
            AlfaNumericoNiTildeNiEnie,
            AlfaNumericoNiTildeNiEnieNiEspeciales,
            NumeroDecimalSinFormato,
            PorcentajeDecimal
        }

        /// <summary>
        /// Obtiene el control TextBox tipificado con el modelo.
        /// </summary>
        /// <typeparam name="TModel">Modelo de la Vista</typeparam>
        /// <typeparam name="TValue">valor que toma el modelo</typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression">Modelo</param>
        /// <param name="htmlAttributes">Atributos que toma el control</param>
        /// <param name="tipoValidacion">Enumerador que indica el tipo de dato del cual seteara.</param>
        /// <returns></returns>
        public static MvcHtmlString TextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes, TipoValidacion tipoValidacion)
        {
            return TextBoxFor(htmlHelper, expression, new RouteValueDictionary(htmlAttributes), tipoValidacion);
        }

        /// <summary>
        /// Obtiene el control TextBox tipificado con el modelo.
        /// </summary>
        /// <typeparam name="TModel">Modelo de la Vista</typeparam>
        /// <typeparam name="TValue">valor que toma el modelo</typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression">Modelo</param>
        /// <param name="htmlAttributes">Atributos que toma el control</param>
        /// <param name="tipoValidacion">Enumerador que indica el tipo de dato del cual seteara.</param>
        /// <returns></returns>
        public static MvcHtmlString TextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes, TipoValidacion tipoValidacion)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var attibutes = new RouteValueDictionary(htmlAttributes);

            string nameControl = ExpressionHelper.GetExpressionText(expression);
            string valueControl = metadata.Model == null ? string.Empty : metadata.Model.ToString();
            string control = InputHelper(htmlHelper, nameControl, valueControl, (valueControl == string.Empty), true, true, htmlAttributes, tipoValidacion);

            return MvcHtmlString.Create(control);
        }

        /// <summary>
        /// Obtiene el control TextBox notipificado con el modelo.
        /// </summary>
        /// <param name="htmlHelper">Helper</param>
        /// <param name="name">Nombre del control</param>
        /// <param name="value">Valor que toma el control (width, class, etc)</param>
        /// <param name="tipoValidacion">Enumerador que indica el tipo de dato del cual seteara.</param>
        /// <returns></returns>
        public static MvcHtmlString TextBox(this HtmlHelper htmlHelper, string name, object value, TipoValidacion tipoValidacion)
        {
            string control = InputHelper(htmlHelper, name, value, (value == null), true, true, null, tipoValidacion);
            return MvcHtmlString.Create(control);
        }

        /// <summary>
        /// Obtiene el control TextBox notipificado con el modelo.
        /// </summary>
        /// <param name="htmlHelper">Helper</param>
        /// <param name="name">Nombre del control</param>
        /// <param name="value">Valor que toma el control</param>
        /// <param name="htmlAttributes">atributos que toma el control (width, class, etc)</param>
        /// <param name="tipoValidacion">Enumerador que indica el tipo de dato del cual seteara.</param>
        /// <returns></returns>
        public static MvcHtmlString TextBox(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes, TipoValidacion tipoValidacion)
        {
            string control = InputHelper(htmlHelper, name, value, (value == null), true, true, new RouteValueDictionary(htmlAttributes), tipoValidacion);
            return MvcHtmlString.Create(control);
        }

        private static string InputHelper(this HtmlHelper htmlHelper, string name, object value, bool useViewData, bool setId, bool isExplicitValue, IDictionary<string, object> htmlAttributes, TipoValidacion tipoValidacion)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("El Nombre del control no puede ser nulo o vacio.", "name");
            }

            var tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttribute("type", "text");
            tagBuilder.MergeAttribute("name", name, true);
            if (htmlAttributes != null)
                tagBuilder.MergeAttributes(htmlAttributes);

            string valueParameter = Convert.ToString(value, CultureInfo.CurrentCulture);

            switch (tipoValidacion)
            {
                case TipoValidacion.Numerico:
                    tagBuilder.Attributes.Add("onkeypress", "FP_ValidaNumerico(event);");
                    tagBuilder.Attributes.Add("onblur", "return FP_ValidaNumericoCP(event, this);");
                    break;
                case TipoValidacion.AlfaNumerico:
                    tagBuilder.Attributes.Add("onkeypress", "return FP_ValidaAlfaNumerico(event);");
                    tagBuilder.Attributes.Add("onblur", "return FP_ValidaAlfaNumericoCP(event, this);");
                    break;
                case TipoValidacion.Alfabetico:
                    tagBuilder.Attributes.Add("onkeypress", "return FP_ValidaAlfabetico(event);");
                    tagBuilder.Attributes.Add("onblur", "return FP_ValidaAlfabeticoCP(event, this);");
                    break;
                case TipoValidacion.Email:
                    tagBuilder.Attributes.Add("onkeypress", "return FP_ValidaEmail(event);");
                    tagBuilder.Attributes.Add("onblur", "return FP_ValidaEmailCP(event, this);");
                    break;
                case TipoValidacion.Fecha:
                    tagBuilder.Attributes.Add("onkeypress", "return FP_ValidaFecha(event);");
                    tagBuilder.Attributes.Add("onblur", "return FP_ValidaFechaCP(event, this);");
                    valueParameter = string.IsNullOrEmpty(valueParameter) ? string.Empty : Convert.ToDateTime(valueParameter).ToShortDateString();
                    break;
                case TipoValidacion.Horario:
                    tagBuilder.Attributes.Add("onkeypress", "return FP_ValidaHorario(event);");
                    tagBuilder.Attributes.Add("onblur", "return FP_ValidaHorarioCP(event, this);");
                    break;
                case TipoValidacion.NumeroEntero:
                    tagBuilder.Attributes.Add("onkeypress", "return FP_ValidaNumeroEntero(event);");
                    tagBuilder.Attributes.Add("onblur", "return FP_AgregaComasEntero(event, this);");
                    valueParameter = string.IsNullOrEmpty(valueParameter) ? string.Empty : double.Parse(valueParameter).ToString("####################", CultureInfo.InvariantCulture);
                    break;
                case TipoValidacion.NumeroDecimal:
                    tagBuilder.Attributes.Add("onkeypress", "return FP_ValidaNumeroDecimal(event);");
                    tagBuilder.Attributes.Add("onblur", "return FP_AgregaComasDecimal(event, this);");
                    valueParameter = string.IsNullOrEmpty(valueParameter) ? string.Empty : double.Parse(valueParameter).ToString("#############.##", CultureInfo.InvariantCulture);
                    break;
                //Formatos Temporales
                case TipoValidacion.NumeroEnteroGuionSlash:
                    tagBuilder.Attributes.Add("onkeypress", "return FP_ValidaEnteroGuionSlash(event);");
                    tagBuilder.Attributes.Add("onblur", "return FP_ValidaEnteroGuionSlashCP(event, this);");
                    break;
                case TipoValidacion.NumeroEnteroSinFormato:
                    tagBuilder.Attributes.Add("onkeypress", "return FP_ValidaNumeroEntero(event);");
                    tagBuilder.Attributes.Add("onblur", "return FP_ValidaNumeroEnteroCP(event, this);");
                    break;
                case TipoValidacion.Ip:
                    tagBuilder.Attributes.Add("onkeypress", "return FP_ValidaIP(event);");
                    tagBuilder.Attributes.Add("onblur", "return FP_ValidaIPCP(event, this);");
                    break;
                case TipoValidacion.Porcentaje:
                    tagBuilder.Attributes.Add("onkeypress", "return FP_ValidaPorcentaje(event);");
                    tagBuilder.Attributes.Add("onblur", "return FP_ValidaPorcentajeCP(event, this);");
                    break;
                case TipoValidacion.AlfaNumericoNiTildeNiEnie:
                    tagBuilder.Attributes.Add("onkeypress", "return FP_ValidaAlfaNumericoNiTildeNiEnie(event);");
                    tagBuilder.Attributes.Add("onblur", "return FP_ValidaAlfaNumericoNiTildeNiEnieCP(event, this);");
                    break;
                case TipoValidacion.AlfaNumericoNiTildeNiEnieNiEspeciales:
                    tagBuilder.Attributes.Add("onkeypress", "return FP_ValidaAlfaNumericoNiTildeNiEnieNiEspeciales(event);");
                    tagBuilder.Attributes.Add("onblur", "return FP_ValidaAlfaNumericoNiTildeNiEnieNiEspecialesCP(event, this);");
                    break;
                case TipoValidacion.NumeroDecimalSinFormato:
                    tagBuilder.Attributes.Add("onkeypress", "return FP_ValidaNumericoSinFormato(event);");
                    tagBuilder.Attributes.Add("onblur", "return FP_ValidaNumericoSinFormatoCP(event, this);");
                    break;
                case TipoValidacion.PorcentajeDecimal:
                    tagBuilder.Attributes.Add("onkeypress", "return FP_ValidaPorcentajeDecimal(event);");
                    tagBuilder.Attributes.Add("onblur", "return FP_ValidaPorcentajeDecimalCP(event, this);");
                    break;
            }
            string attemptedValue = null;
            ModelState modelState = null;
            if (htmlHelper.ViewData.ModelState.TryGetValue(name, out modelState))
            {
                attemptedValue = (string)modelState.Value.ConvertTo(typeof(string), null /* culture */);
            }

            tagBuilder.MergeAttribute("value", attemptedValue ?? ((useViewData) ? Convert.ToString(htmlHelper.ViewData.Eval(name), CultureInfo.CurrentCulture) : valueParameter), isExplicitValue);

            if (setId)
            {
                tagBuilder.GenerateId(name);
            }

            if (modelState != null)
            {
                if (modelState.Errors.Count > 0)
                {
                    tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
                }
            }

            return tagBuilder.ToString(TagRenderMode.SelfClosing);
        }
    }
}
