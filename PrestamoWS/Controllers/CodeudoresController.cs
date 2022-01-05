using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using PcpUtilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;


namespace PrestamoWS.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]

    /// <summary>
    /// Para registrar los pagos realizados por los clientes a los prestamos
    /// </summary>

    public class CodeudoresController : ControllerBasePrestamoWS
    {
        
        [HttpGet]
        public IEnumerable<Codeudor> Get([FromQuery] CodeudorGetParams getParams, bool convertToObj)
        {
            //var imgPath = ImagePathForCodeudores;    
            var data = BLLPrestamo.Instance.GetCodeudores(getParams, convertToObj, ImagePathForCodeudores);
            return data;
        }


        [HttpGet]
        public IEnumerable<Codeudor> SearchCodeudores(string textoABuscar, bool cargarImagenes=false)
        {
            var codeudores = searchCodeudor(textoABuscar, cargarImagenes);
            return codeudores;
        }
        /// <summary>
        /// esto es para insertar o actualizar un cliente
        /// </summary>
        /// <param name="cliente"></param>
        [HttpPost]
        public IActionResult Post([FromBody] Codeudor param)
        {
            var img = param.ImagenesObj;
            param.Usuario = this.LoginName;
            param.IdLocalidadNegocio = this.IdLocalidadNegocio;
            param.IdNegocio = 1;
            var state = ModelState.IsValid;
            try
            {
                ManejoImagenes.ProcesarImagenes(param.ImagenesObj, ImagePathForCodeudores , string.Empty);
                var id = BLLPrestamo.Instance.InsUpdCodeudor(param);
                return Ok(id);
            }
            catch (Exception e)
            {
                throw new Exception("El codeudor no pudo ser creado");

            }
        }
        /// <summary>
        /// Esto es para Borrar, anular un cliente
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public IActionResult Delete(int idCodeudor)
        {
            try
            {
                BLLPrestamo.Instance.AnularCodeudor(new CodeudorDelParams { Id = idCodeudor, Usuario = "pendiente" });
                return Ok("Registro fue eliminado exitosamente");
            }
            catch (Exception e)
            {
                throw new Exception("Lo siento el registro no pudo ser eliminado");
            }
        }

        private IEnumerable<Codeudor> searchCodeudor(string searchText, bool CargarImagenes)
        {
            IEnumerable<Codeudor> codeudores = null;
            codeudores = BLLPrestamo.Instance.SearchCodeudor(new BuscarCodeudorParams { TextToSearch = searchText, IdNegocio = 1 });
            //if (CargarImagenesClientes)
            //{
            //    foreach (var cliente in codeudores)
            //    {
            //        //cliente.Imagen1FileName = Url.Content(SiteDirectory.ImagesForClientes + "/" + cliente.Imagen1FileName);
            //    }
            //}
            return codeudores;
        }
    }
}
