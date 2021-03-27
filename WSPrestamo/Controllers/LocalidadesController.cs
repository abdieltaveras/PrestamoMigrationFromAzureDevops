﻿using PrestamoBLL.Entidades;
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
        public IEnumerable<Localidad> Get(int idLocalidad=1, int IdLocalidadNegocio=-1, int idNegocio=-1)
        {
            var searchParam = new LocalidadGetParams { IdLocalidad = idLocalidad, Usuario = this.LoginName, IdNegocio = idNegocio, IdLocalidadNegocio = IdLocalidadNegocio  };
            var a = BLLPrestamo.Instance.GetLocalidades(searchParam);
            //var a = BLLPrestamo.BllAcciones.GetData<Localidad, LocalidadGetParams>(new LocalidadGetParams(), "spGetLocalidades", BLLPrestamo.GetValidation);
            return a;
        }
        public IEnumerable<Localidad> GetLocalidadConSusPadres(int idLocalidad = 1, int IdLocalidadNegocio = -1, int idNegocio = -1)
        {
            var searchParam = new LocalidadGetParams { IdLocalidad = idLocalidad, Usuario = this.LoginName, IdNegocio = idNegocio, IdLocalidadNegocio = IdLocalidadNegocio };

            var a = BLLPrestamo.Instance.GetLocalidadesConSusPadres(searchParam);
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
        public IEnumerable<BuscarLocalidad> Get(string Search)
        {
            return BLLPrestamo.Instance.SearchLocalidad(new BuscarLocalidadParams { Search = Search });

        }
        public IEnumerable<string> GetSearchLocalidadByName(int idLocalidad, int idNegocio)
        {
            return BLLPrestamo.Instance.SearchLocalidadByName(new BuscarNombreLocalidadParams { IdLocalidad = idLocalidad, IdNegocio = idNegocio });

        }

        public IEnumerable<Localidad> GetPaises()
        {
            return BLLPrestamo.Instance.GetPaises(new LocalidadPaisesGetParams { });
        }

        public IEnumerable<LocalidadesHijas> GetHijasLocalidades(int idLocalidad = -1)
        {
            var paramlocalidad = new LocalidadGetParams { IdLocalidad = idLocalidad };
            return BLLPrestamo.Instance.GetHijasLocalidades(new LocalidadGetParams { IdLocalidad = idLocalidad });
        }
        [HttpPost]
        public IHttpActionResult Post(Localidad localidad)
        {
            //var localidadparams = new Localidad { IdLocalidad = IdLocalidad, IdLocalidadPadre = IdLocalidadPadre, IdTipoLocalidad = IdTipoLocalidad,  };
            BLLPrestamo.Instance.InsUpdLocalidad(localidad);
            return Ok();
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