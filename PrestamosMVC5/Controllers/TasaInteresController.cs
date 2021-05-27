using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using PrestamosMVC5.SiteUtils;
using PrestamosMVC5.Models;

namespace PrestamosMVC5.Controllers
{
    [AuthorizeUser]
    public class TasaInteresController : ControllerBasePcp
    {
        public TasaInteresController()
        {
            UpdViewBag_LoadCssAndJsForDatatable(true);
        }
        // GET: TasaInteres
        //[HasPermission(Operacion = "tasainteres-ver")]
        public ActionResult Index()
        {
            TasaInteresVM modelo = new TasaInteresVM();
            //var prueba = 2;
            modelo.ListaTasaInteres = BLLPrestamo.Instance.GetTasasDeInteres(new TasaInteresGetParams { IdNegocio = pcpUserIdNegocio });
            //ViewBag.listaInteres = intereses;

            return View(modelo);
        }

        [HttpPost]
        public RedirectToRouteResult Index(TasaInteresVM interes)
        {

            var id = Request["TasaInteres.RequiereAutorizacion"];
            pcpSetUsuarioAndIdNegocioTo(interes.TasaInteres);
            
            try
            {
               BLLPrestamo.Instance.InsUpdTasaInteres(interes.TasaInteres);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se daño algo " + ex.Message);
            }

            return  RedirectToAction("Index");
        }

        public RedirectToRouteResult Delete(int id)
        {
            var tasaInteres = new TasaInteresDelParams { Id = id };
            pcpSetUsuarioTo(tasaInteres);
            try
            {
                BLLPrestamo.Instance.DeleteTasaInteres(tasaInteres);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                throw;
            }

            return RedirectToAction("Index");
        }

        public JsonResult GetAllTasasDeInteres()
        {
            var data = BLLPrestamo.Instance.GetTasasDeInteres(new TasaInteresGetParams());
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}