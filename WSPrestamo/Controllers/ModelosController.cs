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
    public class ModelosController : BaseApiController
    {
        //[HttpGet]
        public IEnumerable<ModeloWithMarca> GetAll()
        {
            ModeloVM datos = new ModeloVM();

            datos.ListaModelos = BLLPrestamo.Instance.GetModelos(new ModeloGetParams { IdNegocio = 1 });
            return datos.ListaModelos;
        }
        
        public IEnumerable<Modelo> Get(int idMarca)
        {
            IEnumerable<Modelo> modelos = null;
            modelos = BLLPrestamo.Instance.GetModelosByMarca(new ModeloGetParams { IdMarca = idMarca, IdNegocio = 1 });
            return modelos;
        }

        [HttpPost]
        public IHttpActionResult Post(Modelo modelo)
        {
            modelo.Usuario = this.LoginName;
            modelo.IdLocalidadNegocio = this.IdLocalidadNegocio;
            BLLPrestamo.Instance.InsUpdModelo(modelo);
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Anular(int idRegistro)
        {
            // llenar el parametro de borrado si lo requier el metodo
            var elimParam = new AnularCatalogo
            {
                NombreTabla = "tblModelos",
                IdRegistro = idRegistro.ToString()
            };
            try
            {
                BLLPrestamo.Instance.AnularCatalogo(elimParam);
                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception("Registro no pudo ser anulado");
            }

            //return RedirectToAction("CreateOrEdit");
        }
    }
}
