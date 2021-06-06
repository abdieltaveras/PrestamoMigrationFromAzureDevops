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
        [HttpGet]
        public IEnumerable<Garantia> GetWithPrestamo(string JsonGet = "")
        {
            //search = "26";
            //string search = "26";
            IEnumerable<Garantia> garantias;
            //if (search.Length >= BUSCAR_A_PARTIR_DE)
            //{
            var paramss = JsonConvert.DeserializeObject<BuscarGarantiaParams>(JsonGet);
            paramss.IdNegocio = 1;
            garantias = BLLPrestamo.Instance.SearchGarantiaConDetallesDePrestamos(paramss);
            //}
            //********** enviamos la base64 de la imagen
            return garantias;
        }

        public IEnumerable<Garantia> GetGarantiasByPrestamo(int idPrestamo)
        {
            var result = BLLPrestamo.Instance.GetGarantiasByPrestamo(idPrestamo);
            return result;
        }

        [HttpGet]
        public IEnumerable<GarantiaConMarcaYModelo> SearchGarantias(string searchText)
        {
            IEnumerable<GarantiaConMarcaYModelo> garantias = null;
            if (searchText.Length >= BUSCAR_A_PARTIR_DE)
            {
                garantias = BLLPrestamo.Instance.SearchGarantias(searchText);
                garantias.ToList().ForEach(item => item.DetallesJSON = item.Detalles.ToType<DetalleGarantia>());
            }
            return garantias;
        }

        public IEnumerable<GarantiaConMarcaYModelo> GetGarantias(string searchObject)
        {
            var search = searchObject.ToType<GarantiaGetParams>();
            search.Usuario = LoginName;
            var result = BLLPrestamo.Instance.GetGarantias(search);
            result.ToList().ForEach(item => item.DetallesJSON = item.Detalles.ToType<DetalleGarantia>());
            return result;
        }

        public IEnumerable<Garantia> Get(string JsonGet = "")
        {
            dynamic listResult = null;
            var searchGarantia = JsonConvert.DeserializeObject<GarantiaGetParams>(JsonGet);
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

        
        
        [HttpPost]
        public IHttpActionResult Post(Garantia garantia)
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
