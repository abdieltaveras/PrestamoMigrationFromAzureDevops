﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Controllers
{
    public class IngresosController : ControllerBasePcp
    {
        
        public ActionResult Index()
        {
            return View();
        }
    }
}