using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PrestamoBLL;
using WSPrestamo.Models;
using System.Web.Http;
namespace WSPrestamo.Controllers
{
    public class LocalidadesController : BaseApiController
    {
        // GET: Localidades
        public IEnumerable<Localidad> GetAll()
        {
            BLLPrestamo bl = new BLLPrestamo();
            var a= BLLPrestamo.Instance.GetLocalidades(new LocalidadGetParams());
            //var a = BLLPrestamo.BllAcciones.GetData<Localidad, LocalidadGetParams>(new LocalidadGetParams(), "spGetLocalidades", BLLPrestamo.GetValidation);
            return a;
        }

        /// <summary>
        /// Busca las localidades padres y devuelve el nombre de la localida mas los padres de ellas, permitiendo
        /// identificar si el nombre de una localidad existe para mas de una localidad padre, identificar que claramente
        /// se selecciono o es la correct ejemplo villa Duarte en Santo Domingo o villa Duarte en Santiago
        /// </summary>
        /// <param name="idLocalidad"></param>
        /// <returns></returns>
        public string Get(int idLocalidad)
        {
            var result = BLLPrestamo.Instance.GetLocalidades(new LocalidadGetParams { IdLocalidad = idLocalidad });
            var localidades = from localidad in result select new { localidad.Nombre };
            string localidadFullName = string.Empty;
            result.ToList().ForEach(loc => localidadFullName += loc.Nombre + " ");
            return localidadFullName;
        }

        [HttpPost]
        public IHttpActionResult Post(Localidad localidad)
        {
            //var localidadparams = new Localidad { IdLocalidad = IdLocalidad, IdLocalidadPadre = IdLocalidadPadre, IdTipoLocalidad = IdTipoLocalidad,  };
            BLLPrestamo.Instance.InsUpdLocalidad(localidad);
            return Ok();
        }
        
        public IEnumerable<BuscarLocalidad> Get(string Search)
        {
            return BLLPrestamo.Instance.SearchLocalida(new BuscarLocalidadParams { Search = Search });
 
        }
        public IEnumerable<string> Get(int idLocalidad, int idNegocio)
        {
            return BLLPrestamo.Instance.SearchLocalidadByName(new BuscarNombreLocalidadParams { IdLocalidad = idLocalidad, IdNegocio = idNegocio });
    
        }

        public IEnumerable<Localidad> Get2()
        {
            return BLLPrestamo.Instance.GetPaises(new LocalidadPaisesGetParams { });
        }

        public IEnumerable<LocalidadesHijas> Get3(int idLocalidad = -1)
        {
            var paramlocalidad = new LocalidadGetParams { IdLocalidad = idLocalidad };
            return BLLPrestamo.Instance.GetHijasLocalidades(new LocalidadGetParams { IdLocalidad = idLocalidad });
        }
        [HttpDelete]
        public IHttpActionResult Anular(int idRegistro)
        {
            // llenar el parametro de borrado si lo requier el metodo
            var elimParam = new AnularCatalogo
            {
                NombreTabla = "tblLocalidades",
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