using Microsoft.AspNetCore.Mvc;
using PrestamoBLL;
using PrestamoEntidades;
using PrestamoWS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PrestamoWS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MarcasController : Controller
    {
        //[HttpGet]
        //public Marca Get()
        //{
        //    return new Marca();
        //}
        [HttpGet]
        public IEnumerable<Marca> Get()
        {
            MarcaVM datos = new MarcaVM();
            //Hay que agregar el controller
            datos.ListaMarcas = BLLPrestamo.Instance.GetMarcas(new MarcaGetParams { IdNegocio = 1 });
            return datos.ListaMarcas;
            //return View("CreateOrEdit", datos);
        }
        [HttpPost]
        public IActionResult Post(Marca marca)
        {
            //marca.IdNegocio = 1;
            //marca.InsertadoPor = "Bryan";
            //this.pcpSetUsuarioAndIdNegocioTo(marca);
            BLLPrestamo.Instance.InsUpdMarca(marca);
            return Ok();
            //return RedirectToAction("CreateOrEdit");
        }
    }
}
