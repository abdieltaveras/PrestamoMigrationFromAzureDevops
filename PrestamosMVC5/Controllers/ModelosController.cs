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
    public class ModelosController : Controller
    {
        // GET: Modelos
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateOrEdit()
        {
            ModeloVM datos = new ModeloVM();

            datos.ListaModelos = BLLPrestamo.Instance.GetModelos(new ModeloGetParams { IdNegocio = 1 });
            datos.ListaMarcas = BLLPrestamo.Instance.GetMarcas(new MarcaGetParams { IdNegocio = 1 });

            datos.ListaSeleccionMarcas =  new SelectList(datos.ListaMarcas, "IdMarca", "Nombre");

            return View("CreateOrEdit", datos);
        }

        [HttpPost]
        public RedirectToRouteResult CreateOrEdit(ModeloInsUpdParams modelo)
        {
            modelo.IdNegocio = 1;
            modelo.InsertadoPor = "Bryan";
            BLLPrestamo.Instance.InsUpdModelo(modelo);
            return RedirectToAction("CreateOrEdit");
        }
    }
}