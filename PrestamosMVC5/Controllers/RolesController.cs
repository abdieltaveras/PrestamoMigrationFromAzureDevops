using Newtonsoft.Json;
using PrestamoBLL;
using PrestamoBLL.Entidades;
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
        public RolesController()
        {
            UpdViewBag_LoadCssAndJsForDatatable(true);
        }
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
        public ActionResult Edit(int id = 0)
        {
            RoleVM datos = new RoleVM();

            datos.Role = new Role() { IdRole = id };

            datos.ListaRoles = BLLPrestamo.Instance.RolesGet(new RoleGetParams { IdNegocio = this.pcpUserIdNegocio });
            datos.Operaciones = BLLPrestamo.Instance.OperacionesGet(new OperacionGetParams { IdNegocio = this.pcpUserIdNegocio });

            return View("edit", datos);
        }
        [HttpPost]
        public ActionResult CreateOrEdit(RoleVM Role)
        {
            int idrole = Role.Role.IdRole;

            pcpSetUsuarioAndIdNegocioTo(Role.Role);

            if (idrole == 0)
            {
                idrole = BLLPrestamo.Instance.RoleInsUpd(Role.Role);
            }

            // Listado actual del usuario
            List<RoleOperacion> ListadoDB =  (List<RoleOperacion>)BLLPrestamo.Instance.RoleOperacionesGet(new RoleOperacionGetParams { IdRole = idrole});

            List<RoleOperacionIns> ListaAInsertar = new List<RoleOperacionIns>();
            List<RoleOperacionIns> ListaAAnular = new List<RoleOperacionIns>();
            List<RoleOperacionIns> ListaAModificar = new List<RoleOperacionIns>();

            List<RoleOperacionIns> lista = new List<RoleOperacionIns>();
 
            // Crear lista de seleccion actual
            if (Role.ListaOperaciones != null)
            {
                foreach (var operacion in Role.ListaOperaciones)
                {
                    lista.Add(new RoleOperacionIns() { IdRole = idrole, IdOperacion = int.Parse(operacion) });
                }
            }

            // Determinar cuales se insertan
            foreach (var item in lista)
            {
                if (!ListadoDB.Exists(element => element.IdOperacion == item.IdOperacion ))
                {
                    ListaAInsertar.Add(new RoleOperacionIns() { IdRole = idrole, IdOperacion = item.IdOperacion });
                }
            }

            // Determinar cuales se borran
            foreach (var item in ListadoDB)
            {
                if (!lista.Exists(element => element.IdOperacion == item.IdOperacion))
                {
                    ListaAAnular.Add(new RoleOperacionIns() { IdRole = idrole, IdOperacion = item.IdOperacion });
                }
            }

            // Determinar cuales se modifican (se quita el AnuladoPor y se asigna el ModificadoPor)
            foreach (var item in ListadoDB)
            {
                if (lista.Exists(element => element.IdOperacion == item.IdOperacion && !item.Anulado()))
                {
                    ListaAModificar.Add(new RoleOperacionIns() { IdRole = idrole, IdOperacion = item.IdOperacion });
                }
            }

            BLLPrestamo.Instance.RoleOperacionInsUpd(ListaAInsertar, ListaAModificar, ListaAAnular, this.pcpUserLoginName);

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