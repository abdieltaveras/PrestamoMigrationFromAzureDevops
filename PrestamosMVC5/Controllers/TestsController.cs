using PrestamoEntidades;
using PrestamosMVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace PrestamosMVC5.Controllers
{
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
    }

    
}