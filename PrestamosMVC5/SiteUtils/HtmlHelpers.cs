using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.SiteUtils
{
    public enum PropToGet { Name, Description};
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Get a description for a property.
        /// 
        /// Adapted from: https://stackoverflow.com/questions/26967329/using-a-htmlhelper-in-mvc-5
        /// With this as the model for extracting description: https://stackoverflow.com/questions/6578495/how-do-i-display-the-displayattribute-description-attribute-value
        /// Based on these annotations on the model: https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations.displayattribute.description%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="helper">The HtmlHelper (called with @Html on the views)</param>
        /// <param name="expression">Lambda for the property that we'll show the description for. For example model => model.Created</param>
        /// <returns>The description of the property, if available. Alternatively, will return the DisplayName or PropertyName (internal name).</returns>
        public static MvcHtmlString NameOrDescriptionFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, PropToGet propToGet )
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            var description = (propToGet == PropToGet.Description) ? metadata.Description : metadata.DisplayName;

            // fallback! We'll try to show something anyway.
            if (String.IsNullOrEmpty(description)) description = String.IsNullOrEmpty(metadata.DisplayName) ? metadata.PropertyName : metadata.DisplayName;

            return MvcHtmlString.Create(description);
        }
    }
    
}