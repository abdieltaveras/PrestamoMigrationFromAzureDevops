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
            this.UpdViewBag_ShowSideBar(ShowSideBar());
            return View();
        }

        private bool ShowSideBar()
        {
            bool result = false;
            #if DEBUG
            {
                result = true;
            }
            #else
            {
                result = this.pcpIsUserAuthenticated;
            }
            #endif
            return result;
        }
    }
}