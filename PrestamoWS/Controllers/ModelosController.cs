using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoWS.Models;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PrestamoWS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ModelosController : Controller
    {
        [HttpGet]
        public IEnumerable<Modelo> Get()
        {
            ModeloVM datos = new ModeloVM();

            datos.ListaModelos = BLLPrestamo.Instance.GetModelos(new ModeloGetParams { IdNegocio = 1 });
            return datos.ListaModelos;
            //datos.ListaMarcas = BLLPrestamo.Instance.GetMarcas(new MarcaGetParams { IdNegocio = 1 });

            //datos.ListaSeleccionMarcas = new SelectList(datos.ListaMarcas, "IdMarca", "Nombre");
           // return View("CreateOrEdit", datos);
        }
        [HttpGet("{idMarca:int}")]
        public IEnumerable<Modelo> Get(int idMarca)
        {
            IEnumerable<Modelo> modelos = null;

            modelos = BLLPrestamo.Instance.GetModelosByMarca(new ModeloGetParams { IdMarca = idMarca, IdNegocio = 1 });
            return modelos;
            //return JsonConvert.SerializeObject(modelos);
        }

        [HttpPost]
        public IActionResult Post(Modelo modelo)
        {
            //modelo.IdNegocio = 1;
            //modelo.InsertadoPor = "Bryan";
            //this.pcpSetUsuarioAndIdNegocioTo(modelo);
            BLLPrestamo.Instance.InsUpdModelo(modelo);
            return Ok();
            //return RedirectToAction("CreateOrEdit");
        }


    }
}
