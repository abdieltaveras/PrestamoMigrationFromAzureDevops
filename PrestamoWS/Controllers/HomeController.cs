using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamoWS.Controllers
{
    
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class HomeController : ControllerBasePrestamoWS
    {
        [HttpGet]
        public IActionResult Index()
        { 
            return Ok("Index action at Home Controller");
        }


    }
}