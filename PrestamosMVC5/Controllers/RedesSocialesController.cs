using PrestamoBLL;
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
    public class RedesSocialesController : ControllerBasePcp
    {
        public RedesSocialesController()
        {
            UpdViewBag_LoadCssAndJsForDatatable(true);
        }

        // GET: RedesSociales
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateOrEdit()
        {
            RedSocialVM datos = new RedSocialVM();
            datos.ListaRedesSociales = BLLPrestamo.Instance.GetRedesSociales(new RedSocialGetParams { IdNegocio = this.pcpUserIdNegocio });

            return View("CreateOrEdit", datos);
        }

        [HttpPost]
        public RedirectToRouteResult CreateOrEdit(RedSocial redSocial)
        {
            //color.IdNegocio = 1;
            //marca.InsertadoPor = "Bryan";
            this.pcpSetUsuarioAndIdNegocioTo(redSocial);
            BLLPrestamo.Instance.InsUpdRedesSociales(redSocial);
            return RedirectToAction("CreateOrEdit");
        }
    }
}