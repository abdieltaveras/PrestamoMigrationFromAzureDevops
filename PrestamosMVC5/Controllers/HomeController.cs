using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Controllers
{
    public class HomeController : ControllerBasePcp
    {
        public ActionResult Index()
        {
            this.UpdViewBag_ShowSideBar(this.pcpIsUserAuthenticated);
            return View();
        }
    }
}