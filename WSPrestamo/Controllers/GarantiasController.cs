using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Http;
using WSPrestamo.Models;
//hesram
using HESRAM.Utils;
using System.Web.Hosting;
using System.Web;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.IO;

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
        public IEnumerable<Garantia> GetWithPrestamo(string search="")
        {
            //search = "26";
            //string search = "26";
            IEnumerable<Garantia> garantias;
            //if (search.Length >= BUSCAR_A_PARTIR_DE)
            //{
                garantias = BLLPrestamo.Instance.SearchGarantiaConDetallesDePrestamos(new BuscarGarantiaParams { Search = search, IdNegocio = 1 });
            //}
            //********** enviamos la base64 de la imagen
            return garantias;
        }

        public IEnumerable<Garantia> Get(int IdGarantia)
        {
            dynamic listResult = null;
            var searchGarantia = new GarantiaGetParams { IdGarantia = IdGarantia };
            //pcpSetUsuarioAndIdNegocioTo(searchGarantia);
            var garantias = BLLPrestamo.Instance.GetGarantias(searchGarantia);
            var result = new SeachResult<Garantia>(garantias).DataList.ToList<Garantia>();
            result.FirstOrDefault().DetallesJSON = JsonConvert.DeserializeObject<DetalleGarantia>(result.FirstOrDefault().Detalles);
            #region Imagen
            List<string> list = new List<string>();
            if (result.FirstOrDefault().Imagen1FileName != null)
            {
                 listResult = JsonConvert.DeserializeObject<dynamic>(result.FirstOrDefault().Imagen1FileName);
                foreach (var item in listResult)
                {
                    string imagen = Convert.ToString(item.Value);
                    //Obtenemos la ruta de la imagen
                    string path = HttpContext.Current.Server.MapPath("~/Content/ImagesFor/Garantias/" + item.Value + ".jpg");
                    //Evaluamos si existe la imagen
                    var ExisteImagen = File.Exists(path);
                    if (ExisteImagen)
                    {
                        // Utilizamos la libreria HESRAM.Utils y obtenemos el imagebase64 de la ruta de la imagen
                        var imagepath = HConvert.GetImageBase64FromPath(path);
                        // creamos una lista para agregar nuestras bases
                        list.Add("data:image/jpg;base64," + imagepath);
                    }

                }
                IEnumerable<string> sendList = list;
                //garantias.FirstOrDefault().ImagesForGaratiaEntrantes = sendList;
                garantias.FirstOrDefault().ImagesForGaratia = sendList;
            }
            
            //******************************************************//
            #endregion
            

            return result;

        }
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
            #region Imagen
            List<string> ListaImagenes = new List<string>();
            if (garantia.ImagesForGaratia != null)
            {
                for (int i = 0; i < garantia.ImagesForGaratia.Count(); i++)
                {
                    string imagename = garantia.DetallesJSON.Placa + "-" + i.ToString();
                    string resultBase = Regex.Replace(garantia.ImagesForGaratia.ElementAt(i), @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
                    string path = HttpContext.Current.Server.MapPath("~/Content/ImagesFor/Garantias/");
                    // He utilizado la libreria HESRAM.Utils para guardar y copiar la imagen
                    HConvert.SaveBase64AsImage(resultBase, path, imagename);
                    ListaImagenes.Add(imagename);
                }
            }
            #endregion
            garantia.Usuario = this.LoginName;
            garantia.IdLocalidadNegocio = this.IdLocalidadNegocio;
            garantia.Detalles = JsonConvert.SerializeObject(garantia.DetallesJSON);
            garantia.Imagen1FileName = JsonConvert.SerializeObject(ListaImagenes);
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
