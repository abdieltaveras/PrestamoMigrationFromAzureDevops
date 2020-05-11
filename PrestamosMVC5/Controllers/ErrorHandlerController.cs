using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Controllers
{
    public class ErrorHandlerController : ControllerBasePcp
    {
        // GET: ErroHandler

        //  
        // GET: /Error/  
        public ActionResult Default()
        {
            return View();
        }
        public ActionResult NotFound()
        {
            return View();
        }

    }
}