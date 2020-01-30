using PrestamoBLL;
using PrestamoEntidades;
using PrestamosMVC5.Models;
using PrestamosMVC5.SiteUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Controllers
{
    [AuthorizeUser]
    public class ModelosController : ControllerBasePcp
    {
        // GET: Modelos
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateOrEdit()
        {
            ModeloVM datos = new ModeloVM();

            datos.ListaModelos = BLLPrestamo.Instance.GetModelos(new ModeloGetParams { IdNegocio = pcpUserIdNegocio });
            datos.ListaMarcas = BLLPrestamo.Instance.GetMarcas(new MarcaGetParams { IdNegocio = pcpUserIdNegocio });

            datos.ListaSeleccionMarcas =  new SelectList(datos.ListaMarcas, "IdMarca", "Nombre");

            return View("CreateOrEdit", datos);
        }

        [HttpPost]
        public RedirectToRouteResult CreateOrEdit(Modelo modelo)
        {
            //modelo.IdNegocio = 1;
            //modelo.InsertadoPor = "Bryan";
            this.pcpSetUsuarioAndIdNegocioTo(modelo);
            BLLPrestamo.Instance.InsUpdModelo(modelo);
            return RedirectToAction("CreateOrEdit");
        }
    }
}