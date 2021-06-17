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
    //[AuthorizeUser]
    public class TipoMorasController : ControllerBasePcp
    {
        public TipoMorasController()
        {
            UpdViewBag_LoadCssAndJsForDatatable(true);
        }
        // GET: TipoMoras
        public ActionResult Index()
        {
            TipoMoraVM modelo = new TipoMoraVM();

            modelo.ListaTipoMoras = BLLPrestamo.Instance.GetTiposMoras(new TipoMoraGetParams { IdNegocio = pcpUserIdNegocio });
            return View(modelo);
        }

        [HttpPost]
        public RedirectToRouteResult Index(TipoMoraVM mora)
        {            
            pcpSetUsuarioAndIdNegocioTo(mora.TipoMora);

            try
            {
                BLLPrestamo.Instance.InsUpdTipoMora(mora.TipoMora);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se daño algo " + ex.Message);
            }
            return RedirectToAction("Index");
        }
        public RedirectToRouteResult Delete(int id)
        {
            try
            {
                var tipoMora = new TipoMoraDelParams { Id = id};
                pcpSetUsuarioTo(tipoMora);
                BLLPrestamo.Instance.CancelTipoMora(tipoMora);
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