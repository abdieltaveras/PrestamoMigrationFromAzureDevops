using PrestamoBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Controllers
{
    public class ConfigurationController : Controller
    {
        string key => "pcp46232";
        // GET: Configuration
        public ActionResult Index()
        {
            BLLPrestamo.Instance.NegocioCreateIfNotExist(key);
            BLLPrestamo.Instance.CheckAndCreateAdminUserFoNegocios(key);
            return View();
        }
    }
}