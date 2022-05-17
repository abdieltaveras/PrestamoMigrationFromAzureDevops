using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Configuration;
using PrestamoWS.Models;
//hesram
using HESRAM.Utils;
using System.Web;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

using PcpUtilidades;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Hosting;
using System.Text;

namespace PrestamoWS.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GarantiasController : ControllerBasePrestamoWS
    {
        //private readonly HostingEnvironment _hostingEnvironment;

        //public GarantiasController(HostingEnvironment hostingEnvironment)
        //{
        //    _hostingEnvironment = hostingEnvironment;
        //}
        private readonly IWebHostEnvironment _webHostEnvironment;
        private Utils _utils { get; set; } = new Utils();
        public GarantiasController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        int BUSCAR_A_PARTIR_DE = 2;
        [HttpGet]
        public Task<ActionResult<IEnumerable<Garantia>>> GetWithPrestamo([FromQuery] BuscarGarantiaParams getParams)
        {
            //search = "26";
            //string search = "26";
            IEnumerable<Garantia> garantias;
            //if (search.Length >= BUSCAR_A_PARTIR_DE)
            //{
            getParams.IdNegocio = this.IdNegocio;
            garantias = BLLPrestamo.Instance.SearchGarantiaConDetallesDePrestamos(getParams);
            //}
            //********** enviamos la base64 de la imagen
            var result =(Task<ActionResult<IEnumerable<Garantia>>>)garantias;
            return result;
        }
        [HttpGet]
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

        [HttpGet]
        public IEnumerable<GarantiaConMarcaYModelo> GetGarantias([FromQuery] GarantiaGetParams getParams)
        {
            var result = BLLPrestamo.Instance.GetGarantias(getParams);
            result.ToList().ForEach(item => item.DetallesJSON = item.Detalles.ToType<DetalleGarantia>());
            return result;
        }
        [HttpGet]
        public bool TienePrestamoVigentes(int idGarantia)
        {
            var result = BLLPrestamo.Instance.GarantiasTienenPrestamosVigentes(new int[] { idGarantia });
            return result;
        }

        [HttpGet]
        public IEnumerable<GarantiasConPrestamo> GetPrestamosVigentes(int idGarantia)
        {
            var result = BLLPrestamo.Instance.GarantiasConPrestamos(new int[] { idGarantia });
            return result;
        }

        [HttpGet]
        public IEnumerable<Garantia> Get([FromQuery] GarantiaGetParams getParams)

        {
            //pcpSetUsuarioAndIdNegocioTo(searchGarantia);
            var garantias = BLLPrestamo.Instance.GetGarantias(getParams);
            var result = new SeachResult<Garantia>(garantias).DataList.ToList<Garantia>();
            result.ToList().ForEach(item => item.DetallesJSON = item.Detalles.ToType<DetalleGarantia>());
            //result.FirstOrDefault().DetallesJSON = JsonConvert.DeserializeObject<DetalleGarantia>(result.FirstOrDefault().Detalles);
            #region Imagen
            //List<string> list = new List<string>();
            //if (result.FirstOrDefault().Imagen1FileName != null)
            //{
            //    var listResult = JsonConvert.DeserializeObject<dynamic>(result.FirstOrDefault().Imagen1FileName);
            //    foreach (var item in listResult)
            //    {
            //        string imagen = Convert.ToString(item.Value);
            //        //Obtenemos la ruta de la imagen
            //        string path = ImagePathForGarantia + item.Value + ".jpg";
            //        //Evaluamos si existe la imagen
            //        var ExisteImagen = System.IO.File.Exists(path);
            //        if (ExisteImagen)
            //        {
            //            // Utilizamos la libreria HESRAM.Utils y obtenemos el imagebase64 de la ruta de la imagen
            //            var imagepath = HConvert.GetImageBase64FromPath(path);
            //            // creamos una lista para agregar nuestras bases
            //            list.Add("data:image/jpg;base64," + imagepath);
            //        }

            //    }
            //    IEnumerable<string> sendList = list;
            //    //garantias.FirstOrDefault().ImagesForGaratiaEntrantes = sendList;
            //    garantias.FirstOrDefault().ImagesForGaratia = sendList;
            //}

            //******************************************************//
            #endregion


            return result;

        }



        [HttpPost]
        public async Task<IActionResult> Post(Garantia garantia)
        {
            #region Imagen
            ManejoImagenes.ProcesarImagenes(garantia.ImagesForGaratia, ImagePathForGarantia, string.Empty);

            //List<string> ListaImagenes = new List<string>();
            //if (garantia.ImagesForGaratia != null)
            //{
            //    for (int i = 0; i < garantia.ImagesForGaratia.Count(); i++)
            //    {
            //        string imagename = garantia.DetallesJSON.Placa + "-" + i.ToString();
            //        string resultBase = Regex.Replace(garantia.ImagesForGaratia.ElementAt(i), @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
            //        string path = ImagePathForGarantia;
            //        // He utilizado la libreria HESRAM.Utils para guardar y copiar la imagen
            //        HConvert.SaveBase64AsImage(resultBase, path, imagename);
            //        ListaImagenes.Add(imagename+".jpg");
            //    }
            //}
            #endregion
            garantia.Usuario = this.LoginName;
            garantia.IdLocalidadNegocio = this.IdLocalidadNegocio;
            garantia.Detalles = JsonConvert.SerializeObject(garantia.DetallesJSON);
            //garantia.Imagen1FileName = JsonConvert.SerializeObject(ListaImagenes);
            try
            {
                BLLPrestamo.Instance.InsUpdGarantia(garantia);
                return Ok();
            }
            catch (Exception ex)
            {
                return Forbid();
            }
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

        [HttpGet]
        public IActionResult GarantiaFichaReport([FromQuery] int idgarantia, int reportType)
        {
            string[] columnas = {"NombreMarca", "NombreModelo", "NoMaquina", "Placa",
                "Ano", "Color","Matricula","Descripcion",
                "TransPermitidas", "Valor", "Medida", "Tipo", "DetalleDireccion"};
            Garantia garantia = new Garantia();
            IEnumerable<Garantia> garantias = new List<Garantia>();
            IEnumerable<Modelo> modelos = new List<Modelo>();
            IEnumerable<Marca> marcas = new List<Marca>();
            garantias = BLLPrestamo.Instance.GetGarantias(new GarantiaGetParams { IdGarantia = idgarantia });
            garantia = garantias.FirstOrDefault();
            garantia.DetallesJSON = JsonConvert.DeserializeObject<DetalleGarantia>(garantia.Detalles);
            modelos = BLLPrestamo.Instance.GetModelos(new ModeloGetParams { IdModelo = garantia.IdModelo });
            marcas = BLLPrestamo.Instance.GetMarcas(new MarcaGetParams { IdMarca = garantia.IdMarca });
            DataTable dtDatos = HConvert.ListToDataTable<Garantia>(garantias.ToList());

            foreach (var item in columnas)
            {
                dtDatos.Columns.Add(item);
            }
            dtDatos.Rows[0]["NombreMarca"] = $"{marcas.FirstOrDefault().Nombre}";
            dtDatos.Rows[0]["NombreModelo"] = $"{modelos.FirstOrDefault().Nombre}";
            dtDatos.Rows[0]["NoMaquina"] = $"{garantia.DetallesJSON.NoMaquina}";
            dtDatos.Rows[0]["Placa"] = $"{garantia.DetallesJSON.Placa}";
            dtDatos.Rows[0]["Ano"] = $"{garantia.DetallesJSON.Ano}";
            dtDatos.Rows[0]["Color"] = $"{garantia.DetallesJSON.Color}";
            dtDatos.Rows[0]["Matricula"] = $"{garantia.DetallesJSON.Matricula}";
            dtDatos.Rows[0]["Descripcion"] = $"{garantia.DetallesJSON.Descripcion}";
            dtDatos.Rows[0]["TransPermitidas"] = $"{garantia.DetallesJSON.UsoExclusivo.ToString()}";
            dtDatos.Rows[0]["Valor"] = $"{garantia.DetallesJSON.Valor}";
            dtDatos.Rows[0]["Medida"] = $"{garantia.DetallesJSON.Medida}";
            dtDatos.Rows[0]["Tipo"] = $"{garantia.IdClasificacion}";
            dtDatos.Rows[0]["DetalleDireccion"] = $"{garantia.DetallesJSON.DetallesDireccion}";



            List<Reports.Bases.BaseReporteMulti> baseReporte = null;

            #region Imagen
            List<string> listimagen = new List<string>();
            if (garantias.FirstOrDefault().Imagen1FileName != null)
            {
                var listResult = JsonConvert.DeserializeObject<dynamic>(garantias.FirstOrDefault().Imagen1FileName);
                foreach (var item in listResult)
                {
                    //Obtenemos la ruta de la imagen
                    string pathimage = ImagePathForGarantia + item;
                    //Evaluamos si existe la imagen
                    var ExisteImagen = System.IO.File.Exists(pathimage);
                    if (ExisteImagen)
                    {
                        // Utilizamos la libreria HESRAM.Utils y obtenemos el imagebase64 de la ruta de la imagen
                        var imagebase = HConvert.GetImageBase64FromPath(pathimage);
                        // creamos una lista para agregar nuestras bases
                        listimagen.Add(imagebase);
                    }
                }
                if (listResult.Count<4)
                {
                    for (int i = 0; i < (4 - Convert.ToInt32(listResult.Count)); i++)
                    {
                        listimagen.Add(NoImageBase64);
                    }
                }
            }

            //******************************************************//
            #endregion
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            if (listimagen.Count > 0)
            {
                for (int i = 0; i < listimagen.Count; i++)
                {
                    parameters.Add($"Imagen{i + 1}", listimagen[i]);
                }
            }
            else
            {
                parameters.Add("Imagen1", NoImageBase64);
            }

            //******************************************************//
            _utils = new Utils();
            string path = "";
            if (garantia.IdTipoGarantia == 1)
            {
                 path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\Garantias\\FichaInMobiliaria.rdlc";
            }
            else
            {
                path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\Garantias\\FichaMobiliaria.rdlc";
            }
            
            var resultado = _utils.ReportGenerator(dtDatos, path, reportType, baseReporte, parameter: parameters, DataInList: baseReporte);
            return resultado;
        }
    }
}
