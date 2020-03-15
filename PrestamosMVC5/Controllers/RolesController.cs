using PrestamoBLL;
using PrestamoEntidades;
using PrestamosMVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Controllers
{
    public class RolesController : ControllerBasePcp
    {
        // GET: Roles
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListRoles()
        {
            RoleVM datos = new RoleVM();
            datos.ListaRoles = BLLPrestamo.Instance.RolesGet(new RoleGetParams { IdNegocio = this.pcpUserIdNegocio });

            return View("ListRoles", datos);
        }

        public ActionResult CreateOrEdit()
        {
            RoleVM datos = new RoleVM();
            datos.Operaciones = BLLPrestamo.Instance.OperacionesGet(new OperacionGetParams { IdNegocio = this.pcpUserIdNegocio });

            return View("CreateOrEdit", datos);
        }
        [HttpPost]
        public ActionResult CreateOrEdit(RoleVM Role)
        {
            pcpSetUsuarioAndIdNegocioTo(Role.Role);
            int idrole = BLLPrestamo.Instance.RoleInsUpd(Role.Role);

            string operaciones = "";

            int cont = 0;

            foreach (var item in Role.ListaOperaciones)
            {
                cont++;
                if(Role.ListaOperaciones.Length != cont)
                {
                    operaciones += "(" + idrole + "," + item + "),";
                } else
                {
                    operaciones += "(" + idrole + "," + item + ")";
                }
            }

            RoleOperacionInsUpdParams parametros = new RoleOperacionInsUpdParams()
            {
                IdRole = idrole,
                Values = operaciones
            };

            BLLPrestamo.Instance.RoleOperacionInsUpd(parametros);

            return View("CreateOrEdit");
        }
    }
}