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
            var result =new LocalidadesBLL(this.IdLocalidadNegocio,this.LoginName).GetLocalidades(getParams);
            return result;
        }
        [HttpGet]
        // GET: Localidades
        public IActionResult GetLocalidadesComponents([FromQuery] LocalidadesComponentGetParams getParams)
        {
            //Se debe crear un ResponseManager el cual cuando venga un estatus code desde la bll lo gestione automaticamente si retornará 200, 400,401...
            //o algun status code que predefinamos en constantes o base de datos
            try
            {
                var result = new LocalidadesBLL(this.IdLocalidadNegocio, this.LoginName).GetLocalidadesComponents(getParams);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest( new ResponseData(ex,false,ex.Message,400));
            }
          
        }
        [HttpGet]
        public IEnumerable<Localidad> GetLocalidadConSusPadres([FromQuery] LocalidadGetParams getParams)
        {
            
            var a = new LocalidadesBLL(this.IdLocalidadNegocio, this.LoginName).GetLocalidadesConSusPadres(getParams);
            //var a = BLLPrestamo.BllAcciones.GetData<Localidad, LocalidadGetParams>(new LocalidadGetParams(), "spGetLocalidades", BLLPrestamo.GetValidation);
            return a;
        }
        [HttpGet]
        public IEnumerable<BuscarLocalidad>  BuscarLocalidad(string search="", bool soloLosQuePermitenCalle=false, int  minLength=BuscarLocalidadParams.minLengthDefault)
       {
            IEnumerable<BuscarLocalidad> localidades = new List<BuscarLocalidad>();
            if (minLength==0 || (search!=null && search.Length >= BuscarLocalidadParams.minLengthDefault))
            {
                localidades = new LocalidadesBLL(this.IdLocalidadNegocio, this.LoginName).SearchLocalidad(new BuscarLocalidadParams { Search = search, SoloLosQuePermitenCalle= soloLosQuePermitenCalle});
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
            var result = new LocalidadesBLL(this.IdLocalidadNegocio, this.LoginName).GetLocalidadesConSusPadres(new LocalidadGetParams { IdLocalidad = idLocalidad });
            var localidades = from localidad in result select new { localidad.Nombre };
            string localidadFullName = string.Empty;
            result.ToList().ForEach(loc => localidadFullName += loc.Nombre + " ");
            return localidadFullName;
        }
        [HttpGet]
        public IEnumerable<BuscarLocalidad> Search(string Search)
        {
            return new LocalidadesBLL(this.IdLocalidadNegocio, this.LoginName).SearchLocalidad(new BuscarLocalidadParams { Search = Search });
        }
        [HttpGet]
        public IEnumerable<string> GetSearchLocalidadByName(int idLocalidad, int idNegocio)
        {
            return new LocalidadesBLL(this.IdLocalidadNegocio, this.LoginName).SearchLocalidadByName(new BuscarNombreLocalidadParams { IdLocalidad = idLocalidad, IdNegocio = idNegocio });
        }
        [HttpGet]
        public IEnumerable<Localidad> GetPaises()
        {
            return new LocalidadesBLL(this.IdLocalidadNegocio, this.LoginName).GetPaises(new LocalidadPaisesGetParams { });
        }
        [HttpGet]
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<LocalidadesHijas> GetHijasLocalidades(int idLocalidad = -1)
        {
            var paramlocalidad = new LocalidadGetParams { IdLocalidad = idLocalidad };
            var datos = new LocalidadesBLL(this.IdLocalidadNegocio, this.LoginName).GetHijasLocalidades(new LocalidadGetParams { IdLocalidad = idLocalidad,IdNegocio= 1 });
            return datos;
        }
        [HttpPost]
        public IActionResult Post([FromBody] Localidad localidad)
        {
            localidad.Usuario = this.LoginName;
           localidad.IdLocalidadNegocio = this.IdLocalidadNegocio;
           //var localidadparams = new Localidad { IdLocalidad = IdLocalidad, IdLocalidadPadre = IdLocalidadPadre, IdDivisionTerritorialPadre = IdDivisionTerritorialPadre,  };
            new LocalidadesBLL(this.IdLocalidadNegocio, this.LoginName).InsUpdLocalidad(localidad);
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