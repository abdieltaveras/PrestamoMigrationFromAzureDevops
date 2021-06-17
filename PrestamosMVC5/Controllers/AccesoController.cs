using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {

            Role rolesDeUsuario = new Role() {
                IdRole = 1,
                Codigo = "A01",
                Nombre = "Cajera",
                Descripcion = "Este role es para los usuarios de caja",
                //Permisos = new List<Operacion>()
                //{
                //    new Operacion()
                //    {
                //        IdOperacion = 1,
                //        Codigo = "P01",
                //        Nombre = "Cobrar",
                //        Descripcion = "Permiso para realizar cobros a usuarios",
                //    },
                //    new Operacion()
                //    {
                //        IdOperacion = 2,
                //        Codigo = "P02",
                //        Nombre = "ReporteCobro",
                //        Descripcion = "Permiso para reporte de cobros a usuarios",
                //    }
                //}
            };

            return View(rolesDeUsuario);
        }

        public ActionResult AccessDenied()
        {
            return View("AccessDenied");
        }
    }
}