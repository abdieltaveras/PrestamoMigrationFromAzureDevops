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
    public class EquipoController : BaseApiController
    {
        public IEnumerable<Equipo> GetAll()
        {
            return BLLPrestamo.Instance.GetEquipos(new EquiposGetParam {  });
        }
        public IEnumerable<Equipo> Get(int idEquipo, string codigo, int idLocalidad)
        {
            return BLLPrestamo.Instance.GetEquipos(new EquiposGetParam { IdEquipo = idEquipo, Codigo = codigo, IdLocalidadNegocio = idLocalidad });
        }
        /// <summary>
        /// Para registrar el equipo pero no actualiza el campo confirmado ni en la insersion
        /// ni en la actualicion esas son 2 operaciones apartes
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // Registrar Equipo

        [HttpPost]
        public IHttpActionResult Post(Equipo equipo)
        {
            equipo.Usuario = this.LoginName;
            equipo.IdNegocio = 1;
            equipo.IdLocalidadNegocio = this.IdLocalidadNegocio;
            BLLPrestamo.Instance.InsUpdEquipo(equipo);
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Anular(int idRegistro)
        {
            // llenar el parametro de borrado si lo requier el metodo
            var elimParam = new AnularCatalogo
            {
                NombreTabla = "tblEquipo",
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