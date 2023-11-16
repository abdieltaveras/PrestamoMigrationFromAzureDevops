using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;

using PrestamoWS.Models;
using static PrestamoBLL.BLLPrestamo;
using Microsoft.AspNetCore.Mvc;

namespace PrestamoWS.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    /// <summary>
    /// para tomarlo como modelo y copiarlo para hacer los demas
    /// </summary>
    public class AccountController : ControllerBasePrestamoWS
    {
        [HttpGet]
        public IEnumerable<Users> GetAll()
        {
            var usrGetParams = new UsuarioGetParams { IdLocalidadNegocio = this.IdLocalidadNegocio};
            var data = BLLPrestamo.Instance.GetUsuarios(usrGetParams);
            return data;
        }

        [HttpGet]
        public Users Get(string loginName, int idUsuario=-1)
        {
            var usrGetParams = new UsuarioGetParams { LoginName = loginName, IdUsuario=idUsuario, IdLocalidadNegocio = IdLocalidadNegocio};
            var data = BLLPrestamo.Instance.GetUsuarios(usrGetParams);
            
            return data.FirstOrDefault();
        }

        [HttpGet]
        public Tuple<LoginResponse,string>  Login(string loginName, string password, string returnUrl)
        {
            var result = BLLPrestamo.Instance.Login(loginName, password, this.IdLocalidadNegocio);
            var response = new Tuple<LoginResponse, string>(result, returnUrl);
            return response;
        }

        [HttpPost]
        public IActionResult Post(Users usuario)
        {
            try
            {
                BLLPrestamo.Instance.InsUpdUsuario(usuario);
                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception("Registro no pudo ser guardado");
            }
            //return RedirectToAction("CreateOrEdit");
        }

        [HttpPost]
        public IActionResult CambiarContraseña(string loginName, string nuevaContraseña, string returnUrl)
        {
            try
            {
                // todo indagar con ernesto si en el api, debe tener todo ese proceso que se tiene en la vista
                // a veces de validar contrasena anterior, u otra opcion, analizarlo con el
                BLLPrestamo.Instance.CambiarContrasena(loginName, nuevaContraseña);
                return Ok(returnUrl);
            }
            catch (Exception e)
            {
                throw new Exception("Registro no pudo ser guardado");
            }
            //return RedirectToAction("CreateOrEdit");
        }


        [HttpPost]
        public IActionResult OlvideLaContraseña(string loginName, string urlCambioContrasena)
        {
            return null;
            // esto deve devolver un link a un correo para debloquear o 
            // o reenviarlo al url para cambio de contrasena
        }

        [HttpDelete]
        public IActionResult Delete(int idUsuario)
        {
            // llenar el parametro de borrado si lo requier el metodo
            var elimParam = new UsuarioDeleteParam {
                Id = idUsuario
            };
            try
            {
                BLLPrestamo.Instance.DeleteUsuario(idUsuario, this.LoginName);
                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception("Registro no pudo ser eliminado");
            }

            //return RedirectToAction("CreateOrEdit");
        }
    }
}
