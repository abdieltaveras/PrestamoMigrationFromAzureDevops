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

            var operaciones = AuthInSession.GetOperacionesToUserSession();

            if (!Array.Exists(operaciones, element => element == name))
            {
                filterContext.Result = new HttpStatusCodeResult(403);
            }           

            base.OnActionExecuting(filterContext);
        }
    }
}