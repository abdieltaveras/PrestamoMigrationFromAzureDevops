using Microsoft.AspNetCore.Mvc;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using PrestamoWS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PrestamoWS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GarantiasController : Controller
    {
        int BUSCAR_A_PARTIR_DE = 2;
        [HttpGet]
        public IEnumerable<GarantiaConMarcaYModelo> Get()
        {
            var getGarantiasParams = new GarantiaGetParams();

            var garantias = BLLPrestamo.Instance.GetGarantias(getGarantiasParams);
            foreach (var item in garantias)
            {
                item.DetallesJSON = item.Detalles.ToType<DetalleGarantia>();
            }
            return garantias;
            //var garantias = Get();
            //return garantias;
        }
        [HttpGet("{idGarantia:int}")]
        public Garantia Get(int idGarantia)
        {
            var searchGarantia = new GarantiaGetParams { IdGarantia = idGarantia };
            //pcpSetUsuarioAndIdNegocioTo(searchGarantia);
            var garantias = BLLPrestamo.Instance.GetGarantias(searchGarantia);
            var result = new SeachResult<Garantia>(garantias);
            return result.DataList.FirstOrDefault();
            //var datos = GetGarantiaByIdGarantia(idGarantia).DataList.FirstOrDefault();
            //return datos;
            
        }
        [HttpGet("{searchToText}")]
        public IEnumerable<Garantia> Get(string searchToText)
        {
            IEnumerable<Garantia> garantias = null;
            if (searchToText.Length >= BUSCAR_A_PARTIR_DE)
            {
                garantias = BLLPrestamo.Instance.SearchGarantiaConDetallesDePrestamos(new BuscarGarantiaParams { Search = searchToText, IdNegocio = 1 });
            }
            return garantias;
        }

        [HttpGet("{IdLocalidad:int}/{IdNegocio:int}")]
        public string Get(int IdLocalidad, int IdNegocio)
        {
            List<string> localidad = null;
            localidad = BLLPrestamo.Instance.SearchLocalidadByName(new BuscarNombreLocalidadParams { IdLocalidad = IdLocalidad, IdNegocio = IdNegocio }).ToList();
            return localidad[0];
        }

        //[HttpGet("{searchToText}")]
        //public IEnumerable<Garantia> Get(string searchToText)
        //{
        //    IEnumerable<Garantia> garantias = null;
        //    if (searchToText.Length >= BUSCAR_A_PARTIR_DE)
        //    {
        //        garantias = BLLPrestamo.Instance.SearchGarantias(new BuscarGarantiaParams { Search = searchToText, IdNegocio = 1 });
        //    }
        //    return garantias;
        //}

        [HttpPost]
        public IActionResult Post(Garantia garantia)
        {
            BLLPrestamo.Instance.InsUpdGarantia(garantia);
            return Ok();
        }


        //[HttpGet("{MarcaModelo}")]
        //private IEnumerable<GarantiaConMarcaYModelo> Get3(string MarcaModelo)
        //{
        //    var getGarantiasParams = new GarantiaGetParams();

        //    var garantias = BLLPrestamo.Instance.GetGarantias(getGarantiasParams);
        //    foreach (var item in garantias)
        //    {
        //        item.DetallesJSON = item.Detalles.ToType<DetalleGarantia>();
        //    }
        //    return garantias;
        //}



        public class SeachResult<T>
        {
            public bool DatosEncontrados { get; private set; } = false;
            public IEnumerable<T> DataList
            {
                get;
                private set;
            }

            public SeachResult(IEnumerable<T> data)
            {
                this.DatosEncontrados = (data != null & data.Count() > 0);
                if (DatosEncontrados)
                    DataList = data;
                else
                    DataList = new List<T>();
            }
        }
    }
}
