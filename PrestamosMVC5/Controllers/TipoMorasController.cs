using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Controllers
{
    public class TipoMorasController : Controller
    {
        // GET: TipoMoras
        public ActionResult Index()
        {
            TipoMora tipomora = new TipoMora();

            var intereses = BLLPrestamo.Instance.GetTiposMoras(new TipoMoraGetParams { IdNegocio = 1 });
            ViewBag.listaMoras = intereses;
            return View();
        }

        [HttpPost]
        public RedirectToRouteResult Index(TipoMora mora)
        {
            Console.WriteLine(mora.Codigo);
            Console.WriteLine(mora.TipoCargo);

            mora.IdNegocio = 1;
            mora.Usuario = "Usuario de prueba";

            try
            {
                BLLPrestamo.Instance.insUpdTipoMora(mora);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se daño algo " + ex.Message);
            }
            return RedirectToAction("Index");
        }
        public RedirectToRouteResult Delete(int id, string usuario)
        {
            try
            {
                BLLPrestamo.Instance.AnularTipoMora(new TipoMoraDelParams { id = id, Usuario = usuario });
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