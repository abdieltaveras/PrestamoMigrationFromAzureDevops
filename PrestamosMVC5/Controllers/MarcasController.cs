using PrestamoBLL;
using PrestamoBLL.Entidades;
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
    public class MarcasController : ControllerBasePcp
    {
        public MarcasController()
        {
            UpdViewBag_LoadCssAndJsForDatatable(true);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateOrEdit()
        {
            MarcaVM datos = new MarcaVM();
            datos.ListaMarcas = BLLPrestamo.Instance.MarcasGet(new MarcaGetParams { IdNegocio = this.pcpUserIdNegocio});

            return View("CreateOrEdit", datos);
        }
        [HttpPost]
        public RedirectToRouteResult CreateOrEdit(Marca marca)
        {
            //marca.IdNegocio = 1;
            //marca.InsertadoPor = "Bryan";
            this.pcpSetUsuarioAndIdNegocioTo(marca);
            BLLPrestamo.Instance.MarcaInsUpd(marca);
            return RedirectToAction("CreateOrEdit");
        }
    }
}