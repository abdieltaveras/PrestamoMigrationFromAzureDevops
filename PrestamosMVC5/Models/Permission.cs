using PrestamosMVC5.SiteUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.Models
{
    public class Permission
    {
        public static bool Has(string OperacionesSet)
        {
            var operacionesUsuario = AuthInSession.GetOperacionesToUserSession();
            string[] operacionesSolicitadas = OperacionesSet.Split(',');

            foreach (var operacion in operacionesSolicitadas)
            {
                //var result = operacionesUsuario == null ? false : operacionesUsuario.Contains(operacion);
                if (!operacionesUsuario.Contains(operacion))
                {
                    return false;
                }
            }
            return true;
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