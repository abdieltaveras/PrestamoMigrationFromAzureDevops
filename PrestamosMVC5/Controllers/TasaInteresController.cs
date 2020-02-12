using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrestamoBLL;
using PrestamoEntidades;
using PrestamosMVC5.SiteUtils;
using PrestamosMVC5.Models;

namespace PrestamosMVC5.Controllers
{
    [AuthorizeUser]
    public class TasaInteresController : ControllerBasePcp
    {    
        // GET: TasaInteres
        public ActionResult Index()
        {
            TasaInteresVM modelo = new TasaInteresVM();

            modelo.ListaTasaInteres = BLLPrestamo.Instance.TasasInteresGet(new TasaInteresGetParams { IdNegocio = pcpUserIdNegocio });
            //ViewBag.listaInteres = intereses;

            return View(modelo);
        }

        [HttpPost]
        public RedirectToRouteResult Index(TasaInteresVM interes)
        {
            pcpSetUsuarioAndIdNegocioTo(interes.TasaInteres);
            
            try
            {
                BLLPrestamo.Instance.TasaInteresInsUpd(interes.TasaInteres);
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
                BLLPrestamo.Instance.TasaInteresDelete(new TasaInteresDelParams { id = id, Usuario = usuario });
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