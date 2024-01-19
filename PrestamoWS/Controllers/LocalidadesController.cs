using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PrestamoBLL;
using PrestamoWS.Models;


using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace PrestamoWS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LocalidadesController : ControllerBasePrestamoWS
    {
        [HttpGet]
        // GET: Localidades
        public IEnumerable<Localidad> Get([FromQuery] LocalidadGetParams getParams)
        {
            
            var result = BLLPrestamo.Instance.GetLocalidades(getParams);
            
            return result;
        }
        [HttpGet]
        public IEnumerable<Localidad> GetLocalidadConSusPadres([FromQuery] LocalidadGetParams getParams)
        {
            
            var a = BLLPrestamo.Instance.GetLocalidadesConSusPadres(getParams);
            //var a = BLLPrestamo.BllAcciones.GetData<Localidad, LocalidadGetParams>(new LocalidadGetParams(), "spGetLocalidades", BLLPrestamo.GetValidation);
            return a;
        }
        [HttpGet]
        public IEnumerable<BuscarLocalidad>  BuscarLocalidad(string search="", bool soloLosQuePermitenCalle=false, int  minLength=BuscarLocalidadParams.minLengthDefault)
       {
            IEnumerable<BuscarLocalidad> localidades = new List<BuscarLocalidad>();
            if (minLength==0 || (search!=null && search.Length >= BuscarLocalidadParams.minLengthDefault))
            {
                localidades = BLLPrestamo.Instance.SearchLocalidad(new BuscarLocalidadParams { Search = search, SoloLosQuePermitenCalle= soloLosQuePermitenCalle});
            }
            return localidades;
        }
        [HttpGet]

        /// <summary>
        /// Busca las localidades padres y devuelve el nombre de la localida mas los padres de ellas, permitiendo
        /// identificar si el nombre de una localidad existe para mas de una localidad padre, identificar que claramente
        /// se selecciono o es la correct ejemplo villa Duarte en Santo Domingo o villa Duarte en Santiago
        /// </summary>
        /// <param name="idLocalidad"></param>
        /// <returns></returns>
        public string GetFullNameLocalidad(int idLocalidad)
        {
            var result = BLLPrestamo.Instance.GetLocalidadesConSusPadres(new LocalidadGetParams { IdLocalidad = idLocalidad });
            var localidades = from localidad in result select new { localidad.Nombre };
            string localidadFullName = string.Empty;
            result.ToList().ForEach(loc => localidadFullName += loc.Nombre + " ");
            return localidadFullName;
        }
        [HttpGet]
        public IEnumerable<BuscarLocalidad> Search(string Search)
        {
            return BLLPrestamo.Instance.SearchLocalidad(new BuscarLocalidadParams { Search = Search });
        }
        [HttpGet]
        public IEnumerable<string> GetSearchLocalidadByName(int idLocalidad, int idNegocio)
        {
            return BLLPrestamo.Instance.SearchLocalidadByName(new BuscarNombreLocalidadParams { IdLocalidad = idLocalidad, IdNegocio = idNegocio });
        }
        [HttpGet]
        public IEnumerable<Localidad> GetPaises()
        {
            return BLLPrestamo.Instance.GetPaises(new LocalidadPaisesGetParams { });
        }
        [HttpGet]
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<LocalidadesHijas> GetHijasLocalidades(int idLocalidad = -1)
        {
            var paramlocalidad = new LocalidadGetParams { IdLocalidad = idLocalidad };
            var datos = BLLPrestamo.Instance.GetHijasLocalidades(new LocalidadGetParams { IdLocalidad = idLocalidad,IdNegocio= 1 });
            return datos;
        }
        [HttpPost]
        public IActionResult Post([FromBody] Localidad localidad)
        {
            localidad.Usuario = this.LoginName;
           localidad.IdLocalidadNegocio = this.IdLocalidadNegocio;
           //var localidadparams = new Localidad { IdLocalidad = IdLocalidad, IdLocalidadPadre = IdLocalidadPadre, IdDivisionTerritorialPadre = IdDivisionTerritorialPadre,  };
            BLLPrestamo.Instance.InsUpdLocalidad(localidad);
            return Ok();
        }
        
        
        [HttpDelete]
        public IActionResult Anular(int idRegistro)
        {
            throw new NotImplementedException();
            //// llenar el parametro de borrado si lo requier el metodo
            //var elimParam = new AnularCatalogo
            //{
            //    NombreTabla = "tblLocalidades",
            //    IdRegistro = idRegistro.ToString()
            //};
            try
            {
                //BLLPrestamo.Instance.AnularCatalogo(elimParam);
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