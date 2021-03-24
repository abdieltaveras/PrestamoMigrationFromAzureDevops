using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Http;
using WSPrestamo.Models;
using HESRAM.Utils;
using System.Web.Hosting;
using System.Web;
using System.Text.RegularExpressions;

namespace WSPrestamo.Controllers
{
    public class GarantiasController : BaseApiController
    {
        //private readonly HostingEnvironment _hostingEnvironment;

        //public GarantiasController(HostingEnvironment hostingEnvironment)
        //{
        //    _hostingEnvironment = hostingEnvironment;
        //}
        int BUSCAR_A_PARTIR_DE = 2;
        public IEnumerable<Garantia> GetWithPrestamo(string search)
        {
            #region Imagen
            //Obtenemos la ruta de la imagen
            string path = HttpContext.Current.Server.MapPath("~/Content/ImagesFor/Garantias/" + "imagenprueba" + ".jpg");
            // Utilizamos la libreria HESRAM.Utils y obtenemos el imagebase64 de la ruta de la imagen
            var imagepath = HConvert.GetImageBase64FromPath(path);
            // creamos una lista para agregar nuestras bases
            List<string> list = new List<string>();
            list.Add("data:image/jpg;base64," + imagepath);
            IEnumerable<string> sendList = list;
            //******************************************************//
            #endregion

            search = "26";
            //string search = "26";
            IEnumerable<Garantia> garantias = null;
            if (search.Length >= BUSCAR_A_PARTIR_DE)
            {
                garantias = BLLPrestamo.Instance.SearchGarantiaConDetallesDePrestamos(new BuscarGarantiaParams { Search = search, IdNegocio = 1 });
            }
            //********** enviamos la base64 de la imagen
            garantias.FirstOrDefault().ImagesForGaratia = sendList;
            return garantias;
        }

        //public Garantia Get(int idGarantia)
        //{
        //    var searchGarantia = new GarantiaGetParams { IdGarantia = idGarantia };
        //    //pcpSetUsuarioAndIdNegocioTo(searchGarantia);
        //    var garantias = BLLPrestamo.Instance.GetGarantias(searchGarantia);
        //    var result = new SeachResult<Garantia>(garantias);
        //    return result.DataList.FirstOrDefault();
        //    //var datos = GetGarantiaByIdGarantia(idGarantia).DataList.FirstOrDefault();
        //    //return datos;

        //}
        //public IEnumerable<GarantiaConMarcaYModelo> GetWithMarca()
        //{
        //    var getGarantiasParams = new GarantiaGetParams();

        //    var garantias = BLLPrestamo.Instance.GetGarantias(getGarantiasParams);
        //    foreach (var item in garantias)
        //    {
        //        item.DetallesJSON = item.Detalles.ToType<DetalleGarantia>();
        //    }
        //    return garantias;
        //    //var garantias = Get();
        //    //return garantias;
        //}

        //public string Get(int IdLocalidad, int IdNegocio)
        //{
        //    List<string> localidad = null;
        //    localidad = BLLPrestamo.Instance.SearchLocalidadByName(new BuscarNombreLocalidadParams { IdLocalidad = IdLocalidad, IdNegocio = IdNegocio }).ToList();
        //    return localidad[0];
        //}

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

        //[HttpPost]
        //public IHttpActionResult Post(Garantia garantia)
        //{
        //    BLLPrestamo.Instance.InsUpdGarantia(garantia);
        //    return Ok();
        //}
        [HttpPost]
        public IHttpActionResult Post( Garantia garantia)
        {
            if (garantia.ImagesForGaratia != null)
            {
                foreach (var item in garantia.ImagesForGaratia)
                {
                    string resultBase = Regex.Replace(item, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
                    string path = HttpContext.Current.Server.MapPath("~/Content/ImagesFor/Garantias/");
                    // He utilizado la libreria HESRAM.Utils para guardar y copiar la imagen
                    HConvert.SaveBase64AsImage(resultBase, path, "imagenprueba");
                    
                }
            }

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
