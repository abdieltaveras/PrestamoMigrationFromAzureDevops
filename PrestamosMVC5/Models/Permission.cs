using PrestamosMVC5.SiteUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.Models
{
    public class Permission
    {
        public static bool Has(string Operacion)
        {
            var operaciones = AuthInSession.GetOperacionesToUserSession();
            var result = operaciones == null ? false : operaciones.Contains(Operacion);
            return  result;
            //if (operaciones == null) return false;

            //if (Array.Exists(operaciones, element => element == Operacion))
            //{
            //    return true;
            //    //filterContext.Result = new HttpStatusCodeResult(403);
            //}

            //return false;
        }
    }
}