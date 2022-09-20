using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PrestamoBlazorApp.Controllers
{
    [Route("[controller]/[action]")]
    public class TestController : Controller
    {
        public  IActionResult Test()
        {
            return View();
        }

        public IActionResult Test2()
        {
            return View();
        }

        public IActionResult GetLocalidades()
        {
            return View();
        }
    }
}
