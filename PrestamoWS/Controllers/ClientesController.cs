using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using PcpUtilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using HESRAM.Utils;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Data;

namespace PrestamoWS.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]

    /// <summary>
    /// Para registrar los pagos realizados por los clientes a los prestamos
    /// </summary>

    public class ClientesController : ControllerBasePrestamoWS
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        private Utils _utils { get; set; } = new Utils();
        public ClientesController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        [HttpGet]
        public IEnumerable<Cliente> Get([FromQuery] ClienteGetParams getParams, bool convertToObj)
        {
            var imgPath = ImagePathForClientes;    
            var data = BLLPrestamo.Instance.GetClientes(getParams, convertToObj,  ImagePathForClientes);
            return data;
        }


        [HttpGet]
        public IEnumerable<Cliente> SearchClientes(string textoABuscar, bool cargarImagenesClientes=false)
        {
            var clientes = searchCliente(textoABuscar, cargarImagenesClientes);
            return clientes;
        }
        /// <summary>
        /// esto es para insertar o actualizar un cliente
        /// </summary>
        /// <param name="cliente"></param>
        [HttpPost]
        public IActionResult Post([FromBody] Cliente cliente)
        {
            var img = cliente.ImagenesObj;
            cliente.Usuario = this.LoginName;
            cliente.IdLocalidadNegocio = this.IdLocalidadNegocio;
            var state = ModelState.IsValid;
            try
            {
                ManejoImagenes.ProcesarImagenes(cliente.ImagenesObj, ImagePathForClientes , string.Empty);
                var id = BLLPrestamo.Instance.InsUpdCliente(cliente);
                return Ok(id);
            }
            catch (Exception e)
            {
                throw new Exception("El cliente no pudo ser creado");

            }
        }
        /// <summary>
        /// Esto es para Borrar, anular un cliente
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public IActionResult Delete(int idCliente)
        {
            try
            {
                BLLPrestamo.Instance.AnularClientes(new ClienteDelParams { Id = idCliente, Usuario = "pendiente" });
                return Ok("Registro fue eliminado exitosamente");
            }
            catch (Exception e)
            {
                throw new Exception("Lo siento el registro no pudo ser eliminado");
            }
        }

        private IEnumerable<Cliente> searchCliente(string searchText, bool CargarImagenesClientes)
        {
            IEnumerable<Cliente> clientes = null;
            clientes = BLLPrestamo.Instance.SearchCliente(new BuscarClienteParams { TextToSearch = searchText, IdNegocio = 1 });
            if (CargarImagenesClientes)
            {
                foreach (var cliente in clientes)
                {
                    //cliente.Imagen1FileName = Url.Content(SiteDirectory.ImagesForClientes + "/" + cliente.Imagen1FileName);
                }
            }
            return clientes;
        }

        [HttpGet]
        public IActionResult ClienteReportInfo([FromQuery] int idcliente)
        {
            string[] columnas = {"Sexo", "Direccion", "TipoIdentificacion" };
            Cliente cliente = new Cliente();
            IEnumerable<Cliente> clientes = new List<Cliente>();
            clientes = BLLPrestamo.Instance.GetClientes(new ClienteGetParams { IdCliente = idcliente }, true, ImagePathForClientes);
            cliente = clientes.FirstOrDefault();
            DataTable dtClientes = HConvert.ListToDataTable<Cliente>(clientes.ToList());

            foreach (var item in columnas)
            {
                dtClientes.Columns.Add(item);
            }
            dtClientes.Rows[0]["Direccion"] = cliente.InfoDireccionObj.Calle;
            dtClientes.Rows[0]["Sexo"] = cliente.idSexo == 1 ? "Hombre" : "Mujer";
            dtClientes.Rows[0]["TipoIdentificacion"] = Enum.GetName( typeof(TiposIdentificacionPersona), cliente.IdTipoIdentificacion);

            List<Reports.Bases.BaseReporteMulti> baseReporte = null;

            #region Imagen
            List<string> listimagen = new List<string>();
            if (clientes.FirstOrDefault().Imagenes != null)
            {
                var listResult = JsonConvert.DeserializeObject<dynamic>(clientes.FirstOrDefault().Imagenes);
                foreach (var item in listResult)
                {
                    //Obtenemos la ruta de la imagen
                    string pathimage = ImagePathForClientes + item.NombreArchivo ;
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
            }

            //******************************************************//
            #endregion
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Imagen1", listimagen.FirstOrDefault());
            //******************************************************//
            _utils = new Utils();

            string path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\Clientes\\Ficha.rdlc";
            var resultado = _utils.ReportGenerator(dtClientes, path, 1, baseReporte, parameter: parameters, DataInList:baseReporte);
            return resultado;
        }
    }
}
