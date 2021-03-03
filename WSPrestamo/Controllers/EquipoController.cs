using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrestamoBLL;
using WSPrestamo.Models;
using System.Web.Http;

namespace WSPrestamo.Controllers
{
    public class EquipoController : ApiController
    {
        /// <summary>
        /// Para registrar el equipo pero no actualiza el campo confirmado ni en la insersion
        /// ni en la actualicion esas son 2 operaciones apartes
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // Registrar Equipo

        //public EquipoIdYCodigo InsUpdEquipo(Equipo data)
        //{
        //    var _updParam = SearchRec.ToSqlParams(data);
        //    var resultSet = DBPrestamo.ExecReaderSelSP<EquipoIdYCodigo>("spInsUpdEquipo", _updParam);
        //    return resultSet.FirstOrDefault();
        //}
        [System.Web.Http.HttpPost]
        public IHttpActionResult Post ()
        {
            var equipoParam = new Equipo {  };
            BLLPrestamo.Instance.InsUpdEquipo(equipoParam);
            return Ok();
        }

        public IEnumerable<Equipo>get( int idEquipo,string codigo ,int idLocalidad )
        {
            return BLLPrestamo.Instance.GetEquipos(new EquiposGetParam { IdEquipo = idEquipo, Codigo = codigo, IdLocalidad = idLocalidad });
        }
    }
}