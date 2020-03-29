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
            // TODO : this line was removed must be a configuration process in debug mode the initDatabase must create admin account
            
            //BLLPrestamo.Instance.CheckAndCreateAdminUserFoNegocios(key);
            return View();
        }
    }
}