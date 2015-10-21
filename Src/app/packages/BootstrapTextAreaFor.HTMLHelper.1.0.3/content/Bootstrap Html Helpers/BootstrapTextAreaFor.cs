using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace System.Web.Mvc.Html
{
    public static partial class HtmlHelpers
    {
        /// <summary>
        /// This HTML Helper will add a Bootstrap stylized textarea.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="expression"></param>
        /// <param name="rows">The number of rows of the control. Default: 5.</param>
        /// <returns></returns>
        public static MvcHtmlString BootstrapTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> self, Expression<Func<TModel, TValue>> expression, [Optional, DefaultParameterValue(5)] int rows)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, self.ViewData);
            var description = metadata.Description;

            // Creates the textarea tag.
            var textarea = new TagBuilder("textarea");

            // Replaces the value if the Model is not null.
            textarea.InnerHtml = metadata.Model == null ? "" : metadata.Model.ToString();

            // General properties.
            textarea.Attributes.Add("id", metadata.PropertyName);
            textarea.Attributes.Add("name", metadata.PropertyName);

            // Stylize with Bootstrap.
            textarea.AddCssClass("form-control");
            textarea.Attributes.Add("type", "text");

            // Add the number of rows.
            textarea.Attributes.Add("rows", rows.ToString());

            // Returns the textarea.
            return new MvcHtmlString(textarea.ToString());
        }
    }
}