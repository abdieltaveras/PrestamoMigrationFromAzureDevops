using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Http;
using WSPrestamo.Models;

namespace WSPrestamo.Controllers
{
    public class MarcasController : ApiController
    {
        public IEnumerable<Marca> Get()
        {
            MarcaVM datos = new MarcaVM();
            //Hay que agregar el controller
            datos.ListaMarcas = BLLPrestamo.Instance.GetMarcas(new MarcaGetParams { IdNegocio = 1 });
            return datos.ListaMarcas;
            //return View("CreateOrEdit", datos);
        }
        [HttpPost]
        public IHttpActionResult Post(Marca marca)
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
