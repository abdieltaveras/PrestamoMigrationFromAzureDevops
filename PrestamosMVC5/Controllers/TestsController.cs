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
    //Controller to make Test

    public class TestsController : Controller
    {
        // GET: Tests
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CheckBoxes()
        {
            var model = new TestCheckBox();
            model.Bloqueado = false;
            return View(model);
        }
        [HttpPost]
        public ActionResult CheckBoxes(TestCheckBox model)
        {
            return Content(model.ToJson());
        }

        public ActionResult Imagen()
        {
            var infoImg = new InfoConImagen();
            infoImg.ImagesForCliente = new List<HttpPostedFileBase>();
            return View(new InfoConImagen());
        }
        [HttpPost]
        public ActionResult Imagen(InfoConImagen infoConImagen)
        {
            ActionResult result = null;
            var files = Utils.SaveFiles(Server.MapPath(ImagePath.ForCliente), infoConImagen.ImagesForCliente).ToList();
            var mensaje = string.Empty;
            files.ToList().ForEach(f =>
            {
                mensaje += f;
                //if (f.IndexOf("error") > 0) 
            });
            return Content(mensaje);
        }
    }

    public class InfoConImagen
    {
        public System.Web.HttpPostedFileBase Imagen { get; set; }
        public IEnumerable<System.Web.HttpPostedFileBase> ImagesForCliente { get; set; }

        public ImagesFor imgsForCliente => new ImagesFor("ImagesForCliente", "Clientes") {Qty=3 };
        
        public IEnumerable<System.Web.HttpPostedFileBase> ImagesForGarantia { get; set; }

        public ImagesFor imgsForGarantia => new ImagesFor("ImagesForGarantia","Garantia") {Qty=2 };
    }

    public class ImagesFor
    {
        public string PropName { get; } = string.Empty;

        public string FriendlyName { get; } = string.Empty;

        public int Qty { get; set; } = 5;

        public ImagesFor(string propName, string friendlyName)
        {
            this.PropName = propName;
            this.FriendlyName = friendlyName;
        }
    }
}