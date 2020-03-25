using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.SiteUtils
{
    public class HasPermission : ActionFilterAttribute
    {
        public string Operacion { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string name = Operacion;

            // Buscar permiso en arreglo
            //string[] arr = (string[])sessionState["operaciones"];
            var aaa = AuthInSession.GetOperacionesToUserSession();

            if (string.IsNullOrEmpty(name))
            {

                filterContext.Result = new HttpStatusCodeResult(403);
            }
            base.OnActionExecuting(filterContext);
        }
    }
}