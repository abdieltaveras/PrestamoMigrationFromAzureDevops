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
using System.Security.Cryptography.X509Certificates;

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
        public IEnumerable<Cliente> Get([FromQuery] ClienteGetParams search)
        {
            var result = new ClienteBLL(this.IdLocalidadNegocio, this.LoginName).GetClientes(search, search.ConvertToObj, ImagePathForClientes);
            return result;
        }
        
        //[HttpGet]
        //public IEnumerable<Cliente> Get([FromQuery] ClienteGetParams getParams)
        //{
        //    var imgPath = ImagePathForClientes;    
        //    var data = ClienteBLL.GetClientes(getParams, getParams.ConvertToObj,  ImagePathForClientes);
        //    return data;
        //}


        [HttpGet]
        public IEnumerable<Cliente> SearchClientes(int option,string textoABuscar, bool cargarImagenesClientes=false)
        {
            IEnumerable<Cliente> clientes = null;
            clientes = new ClienteBLL(this.IdLocalidadNegocio, this.LoginName).SearchCliente(option, textoABuscar);
            if (cargarImagenesClientes)
            {
                foreach (var cliente in clientes)
                {
                    //cliente.Imagen1FileName = Url.Content(SiteDirectory.ImagesForClientes + "/" + cliente.Imagen1FileName);
                }
            }
            return clientes;
            
        }

        //Estp se puede poner como un servicio Generico, con un DataTable, asi nos evitamos estar creandolo
        [HttpGet]
        public IActionResult SearchClientesByColumn(string SearchText, string Colunm, string OrderBy = "")
        {
            IEnumerable<Cliente> clientes = new List<Cliente>();
            try
            {
                clientes = new ClienteBLL(this.IdLocalidadNegocio, this.LoginName).SearchClienteByColumn(SearchText, "tblClientes", Colunm, OrderBy);
                //return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(clientes);
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
                ManejoImagenes.MoverImagenes(cliente.ImagenesRemover, ImagePathForClientes, ImagePathDeleted);
                ManejoImagenes.ProcesarImagenes(cliente.ImagenesObj, ImagePathForClientes , string.Empty);
                var id = new ClienteBLL(this.IdLocalidadNegocio, this.LoginName).InsUpdCliente(cliente);
                return Ok(id);
            }
            catch (Exception e)
            {
              return BadRequest($"El cliente no pudo ser creado || {e.Message}");
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
                new ClienteBLL(this.IdLocalidadNegocio, this.LoginName).AnularClientes(new ClienteDelParams { Id = idCliente, Usuario = "pendiente" });
                return Ok("Registro fue eliminado exitosamente");
            }
            catch (Exception e)
            {
                throw new Exception("Lo siento el registro no pudo ser eliminado");
            }
        }

        
        [HttpGet]
        public IActionResult SearchClienteByProperties([FromQuery] ClienteGetParams param)
        {
            try
            {
                IEnumerable<Cliente> clientes = null;
                //var a = (eOpcionesSearchCliente)9;
                clientes = new ClienteBLL(this.IdLocalidadNegocio, this.LoginName).SearchClientesByProperties(param);
                return Ok(clientes);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
   
        }
        [HttpGet]
        public IActionResult GetImagenCliente(int IdCliente)
        {

            try
            {
                var cliente = new ClienteBLL(this.IdLocalidadNegocio, this.LoginName).GetClientes(new ClienteGetParams { IdCliente = IdCliente }, false);
                if (cliente.Count() > 0)
                {
                    var lst = ManejoImagenes.GetImagen(ImagePathForClientes, cliente.FirstOrDefault().Imagenes);
                    return Ok(lst);

                }
                return BadRequest("Cliente no encontrado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetImagenesClienteV2(int idCliente)
        {
            List<Imagen> imagenesDelCliente = new List<Imagen>();
            try
            {
                var clientes = new ClienteBLL(1, "BllTest").GetClientes(new ClienteGetParams { IdCliente = idCliente }, false);
                var cliente = clientes.FirstOrDefault();
                if (cliente != null)
                {
                    cliente.ConvertImagenJsonToObj(ImagePathForClientes);
                }
                return Ok(cliente.ImagenesObj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult GetImagenIdentificacion(string Identificacion)
        {
            try
            {
                var cliente = new ClienteBLL(this.IdLocalidadNegocio, this.LoginName).GetClientes(new ClienteGetParams { NoIdentificacion=Identificacion},false);
                if (cliente.Count() > 0)
                {
                    var lst = ManejoImagenes.GetImagen(ImagePathForClientesIdentificaciones,cliente.FirstOrDefault().Imagenes);
                    return Ok(lst);

                }
                return BadRequest("Cliente no encontrado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult ClienteReportInfo([FromQuery] int idcliente, int reportType)
        {
            string[] columnas = {"Sexo", "Direccion", "TipoIdentificacion", "TrabajaEn",
                "ContactoTrabajo", "DireccionTrabajo","NombreConyugue","ContactoConyugue",
                "ProfesionCliente","EstadoCivil" };
            Cliente cliente = new Cliente();
            IEnumerable<Cliente> clientes = new List<Cliente>();
            IEnumerable<Ocupacion> ocupaciones = new List<Ocupacion>();
            clientes = new ClienteBLL(this.IdLocalidadNegocio,this.LoginName).GetClientes(new ClienteGetParams { IdCliente = idcliente }, true, ImagePathForClientes);
            cliente = clientes.FirstOrDefault();
            ocupaciones = BLLPrestamo.Instance.GetCatalogos<Ocupacion>(CatalogoName.Ocupacion,new BaseCatalogoGetParams {IdRegistro= cliente.IdTipoProfesionUOcupacion });
            DataTable dtClientes = HConvert.ListToDataTable<Cliente>(clientes.ToList());

            foreach (var item in columnas)
            {
                dtClientes.Columns.Add(item);
            }
            dtClientes.Rows[0]["InfoDireccion"] = $"{cliente.InfoDireccionObj.Calle}  {cliente.InfoDireccionObj.Detalles}";
            dtClientes.Rows[0]["Sexo"] = cliente.idSexo == 1 ? "Hombre" : "Mujer";
            dtClientes.Rows[0]["TipoIdentificacion"] = Enum.GetName( typeof(TiposIdentificacionPersona), cliente.IdTipoIdentificacion);
            dtClientes.Rows[0]["TrabajaEn"] = $"{cliente.InfoLaboralObj.Nombre} || {cliente.InfoLaboralObj.Puesto}";
            dtClientes.Rows[0]["ContactoTrabajo"] = $"{cliente.InfoLaboralObj.NoTelefono1} || {cliente.InfoLaboralObj.NoTelefono2}";
            dtClientes.Rows[0]["DireccionTrabajo"] = $"{cliente.InfoLaboralObj.Direccion}";
            dtClientes.Rows[0]["NombreConyugue"] = $"{cliente.InfoConyugeObj.Nombres} {cliente.InfoConyugeObj.Apellidos}";
            dtClientes.Rows[0]["ContactoConyugue"] = $"{cliente.InfoConyugeObj.TelefonoPersonal} ";
            dtClientes.Rows[0]["ProfesionCliente"] = $"{ocupaciones.FirstOrDefault().Nombre}";
            dtClientes.Rows[0]["EstadoCivil"] = $"{Enum.GetName(typeof(EstadosCiviles), cliente.IdEstadoCivil)}";

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
            if (listimagen.Count>0)
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

            string path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\Clientes\\Ficha.rdlc";
            var resultado = _utils.ReportGenerator(dtClientes, path, reportType, baseReporte, parameter: parameters, DataInList:baseReporte);
            return resultado;
        }
    }
}
