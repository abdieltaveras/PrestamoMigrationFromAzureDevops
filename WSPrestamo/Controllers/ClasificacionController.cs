﻿using PrestamoBLL;
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
    public class ClasificacionController : BaseApiController
    {
        public IEnumerable<Clasificacion> Get(int idNegocio)
        {
            var result = PrestamoBLL.BLLPrestamo.Instance.ClasificacionQueRequierenGarantias(idNegocio);
            var result2 = result.Where(clas => clas.RequiereGarantia);
            return result2;
        }
        [HttpPost]
        public IHttpActionResult Post(Clasificacion clasificacion)
        {
            try
            {
                clasificacion.IdNegocio = 1;
                clasificacion.Usuario = this.LoginName;
                clasificacion.IdLocalidadNegocio = this.IdLocalidadNegocio;
                BLLPrestamo.Instance.InsUpdClasificacion(clasificacion);
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }

            //return RedirectToAction("CreateOrEdit");
        }
    }

}
