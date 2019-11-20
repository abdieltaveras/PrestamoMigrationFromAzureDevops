using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrestamoBLL;
using PrestamoEntidades;

namespace PrestamosMVC5.Controllers
{
    public class TasaInteresController : Controller
    {
    
        // GET: TasaInteres
        public ActionResult Index()
        {
            var intereses = BLLPrestamo.Instance.GetTasasInteres(new TasaInteresGetParams { IdNegocio = 1 });
            ViewBag.listaInteres = intereses;
            return View();
        }

        [HttpPost]
        public RedirectToRouteResult Index(TasaInteres interes)
        {
            Console.WriteLine(interes.Codigo);

            interes.IdNegocio = 1;

            try
            {
                BLLPrestamo.Instance.insUpdTasaInteres(interes);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se daño algo " + ex.Message);
            }

            return  RedirectToAction("Index");
        }

        public RedirectToRouteResult Delete(int id, string usuario)
        {
            try
            {
                BLLPrestamo.Instance.DeleteTasaInteres(new TasaInteresDelParams { id = id, Usuario = usuario });
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                throw;
            }

            return RedirectToAction("Index");
        }
    }
}