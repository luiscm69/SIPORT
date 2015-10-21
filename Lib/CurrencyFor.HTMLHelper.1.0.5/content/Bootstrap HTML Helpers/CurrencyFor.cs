using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace System.Web.Mvc.Html
{
    public static partial class HtmlHelpers
    {
        /// <summary>
        /// Creates an input with a currency mask using JQuery, MoneyMask and Bootstrap. Remember to set the jquery.maskmoney.js reference to your page in order to use the currency mask.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="expression"></param>
        /// <param name="culture">The country culture for currency pattern. If informed, the helper will retrieve the culture data from the framework, if not, it'll retrieve from the Web.Config specified under the system.web/globalization property. If both are not informed, will get 'en-US' pattern as default. </param>
        /// <param name="events">Custom properties to be added to the tag. Separate them using the equals (=) sign.</param>
        /// <returns></returns>
        public static MvcHtmlString CurrencyFor<TModel, TValue>(this HtmlHelper<TModel> self, Expression<Func<TModel, TValue>> expression, [Optional] string culture, [Optional] params string[] events)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, self.ViewData);

            // Creates the main Input tag and set general values.
            var input = new TagBuilder("input");
            input.Attributes.Add("id", metadata.PropertyName);
            input.Attributes.Add("name", metadata.PropertyName);


            // Bootstrap CSS class for inputs.
            input.AddCssClass("form-control");


            // Starts the JQuery.MoneyMask on element load.
            //input.Attributes.Add("onfocus", "$('#' + this.id).maskMoney();");

            // Gets the Culture.
            CultureInfo currentCulture;
            try
            {
                if (!string.IsNullOrEmpty(culture))
                {
                    currentCulture = new CultureInfo(culture);
                }
                else
                {
                    currentCulture = CultureInfo.CurrentCulture;
                }
            }
            catch (Exception)
            {
                // If culture is not found, returns the default below.
                currentCulture = new CultureInfo("en-US");
            }

            // The region below will concatenate all the required string parts of the mask in order for it to work properly, according to the maskMoney API.
            #region Scripts

            var loadScript = new TagBuilder("script");

            // Starts the script.
            loadScript.InnerHtml += "$(window).bind('load', function () {";

            // Inserts the culture formating properties.
            loadScript.InnerHtml += "$('#" + metadata.PropertyName + "').maskMoney({thousands:'" + currentCulture.NumberFormat.CurrencyGroupSeparator + "', decimal:'" + currentCulture.NumberFormat.CurrencyDecimalSeparator + "'});";


            // Starts the maskMoney on the element with the Model value, if the Model is not null.
            if (metadata.Model != null)
            {
                // Compares the given value with all the know currency types in order to know if that is 0. If it is, remove the "value" attribute for validation purposes.
                if (metadata.Model.ToString() == "0" || metadata.Model.ToString() == "0,00" || metadata.Model.ToString() == "0,000" || metadata.Model.ToString() == "0,0000")
                {
                    input.Attributes.Remove("value");
                    // Creates the mask script and then sets the .val() of the element as '' so it's invalid by the [Required] DataAnnotation.
                    loadScript.InnerHtml += "$('#" + metadata.PropertyName + "').maskMoney('mask');$('#" + metadata.PropertyName + "').val('')";
                }
                else
                {
                    loadScript.InnerHtml += "$('#" + metadata.PropertyName + "').maskMoney('mask', " + metadata.Model.ToString().Replace(",", ".") + ");";
                    // The replacing of the comma is needed for compatibility with the mask plugin.
                }
            }

            // Closes the script.
            loadScript.InnerHtml += "});";

            #endregion

            // Sets the placeholder format according to the culture.
            input.Attributes.Add("placeholder", "0" + currentCulture.NumberFormat.CurrencyDecimalSeparator + "00");

            // Adds the custom attributes from the HelperMethods.HTMLHelper package.
            Helpers.AddAtrributes(input, events);


            // Prepend styling.
            // Addon (the icon container)
            var addon = new TagBuilder("span");
            addon.AddCssClass("input-group-addon");
            addon.InnerHtml = currentCulture.NumberFormat.CurrencySymbol;

            // Input group that will hold all the tags and define the prepend boundaries.
            var inputgroup = new TagBuilder("div");
            inputgroup.AddCssClass("input-group");

            // Adds the other elements inside the input group.
            inputgroup.InnerHtml = addon.ToString() + input.ToString(); // +valueInput.ToString();

            // Returns the input group.
            return new MvcHtmlString(inputgroup.ToString() + loadScript.ToString());
        }
    }
}