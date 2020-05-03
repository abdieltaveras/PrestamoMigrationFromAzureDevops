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
    public class TipoController : ControllerBasePcp
    {
        // GET: Tipo
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateOrEdit()
        {
            TipoVM datos = new TipoVM();
            datos.Tipo = new TipoGarantia();
            datos.ListaTipos = BLLPrestamo.Instance.TiposGarantiaGet(new TipoGetParams { IdNegocio = this.pcpUserIdNegocio });

            return View("CreateOrEdit", datos);
        }
        
        // TODO: Cambiar este modelo por la estructura que lleva
        [HttpPost]
        public RedirectToRouteResult CreateOrEdit(TipoGarantia tipo)
        {
            //TipoInsUpdParams TipoAInsertar = new TipoInsUpdParams()
            //{
            //    Nombre = tipo.Nombre,
            //    IdNegocio = pcpUserIdNegocio,
            //    IdClasificacion = tipo.IdClasificacion,
            //    InsertadoPor = "Bryan"
            //};

            this.pcpSetUsuarioAndIdNegocioTo(tipo);

            BLLPrestamo.Instance.TipoGarantiaInsUpd(tipo);
            return RedirectToAction("CreateOrEdit");
        }
    }
}