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
            return View(new InfoConImagen());
        }
        [HttpPost]
        public ActionResult Imagen(InfoConImagen infoConImagen)
        {
            ActionResult result = null;
            try
            {
                Utils.SaveFile(Server.MapPath(ImagePath.ForCliente), infoConImagen.Imagen);
                result = Content("imagen procesada");
            }
            catch (Exception)
            {
                result = Content("imagen no procesada");
            }
            return result;
        }
    }

    public class InfoConImagen
    {
        public System.Web.HttpPostedFileBase Imagen { get; set; }
    }
    
}