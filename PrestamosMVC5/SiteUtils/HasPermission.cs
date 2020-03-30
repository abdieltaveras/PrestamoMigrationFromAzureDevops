using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PrestamosMVC5.Models;
namespace PrestamosMVC5.SiteUtils
{
    public class HasPermission : ActionFilterAttribute
    {
        public string Operacion { get; set; }
        public override void   OnActionExecuting(ActionExecutingContext filterContext)
        {
            //string name = Operacion;
            //var operaciones = AuthInSession.GetOperacionesToUserSession();
            // if (!Array.Exists(operaciones, element => element == name))
            if (!Permission.Has(Operacion))
            {
                filterContext.Result = new RedirectResult("Acceso/AccessDenied");
            }           
            base.OnActionExecuting(filterContext);
        }
    }
}