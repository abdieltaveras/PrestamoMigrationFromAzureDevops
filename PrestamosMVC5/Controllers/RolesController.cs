using Newtonsoft.Json;
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

        public ActionResult Create()
        {
            RoleVM datos = new RoleVM();
            datos.Operaciones = BLLPrestamo.Instance.OperacionesGet(new OperacionGetParams { IdNegocio = this.pcpUserIdNegocio });

            return View("Create", datos);
        }
        public ActionResult Edit()
        {
            RoleVM datos = new RoleVM();
            datos.ListaRoles = BLLPrestamo.Instance.RolesGet(new RoleGetParams { IdNegocio = this.pcpUserIdNegocio });
            datos.Operaciones = BLLPrestamo.Instance.OperacionesGet(new OperacionGetParams { IdNegocio = this.pcpUserIdNegocio });

            return View("edit", datos);
        }
        [HttpPost]
        public ActionResult CreateOrEdit(RoleVM Role)
        {
            int idrole = Role.Role.IdRole;
            string operaciones = "";
            int cont = 0;

            pcpSetUsuarioAndIdNegocioTo(Role.Role);

            if (idrole == 0)
            {
                idrole = BLLPrestamo.Instance.RoleInsUpd(Role.Role);
            }


            foreach (var item in Role.ListaOperaciones)
            {
                cont++;
                if (Role.ListaOperaciones.Length != cont)
                {
                    operaciones += "(" + idrole + "," + item + "),";
                }
                else
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

            return RedirectToAction("ListRoles");
        }

        public void UpdateRoles()
        {

        }

        public string BuscarUserRoles(int idUsuario)
        {
            IEnumerable<UsuarioRole> roles = null;
            roles = BLLPrestamo.Instance.UserRolesSearch(new BuscarUserRolesParams { IdUsuario = idUsuario });
            return JsonConvert.SerializeObject(roles);
        }
        public string BuscarRoleOperaciones(int idRole)
        {
            IEnumerable<RoleOperacion> roles = null;
            roles = BLLPrestamo.Instance.RoleOperacionesSearch(new BuscarRoleOperacionesParams { IdRole = idRole });
            return JsonConvert.SerializeObject(roles);
        }

    }
}