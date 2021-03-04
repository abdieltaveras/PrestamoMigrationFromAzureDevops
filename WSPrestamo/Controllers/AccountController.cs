using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Http;
using WSPrestamo.Models;
using static PrestamoBLL.BLLPrestamo;

namespace WSPrestamo.Controllers
{
 
    /// <summary>
    /// para tomarlo como modelo y copiarlo para hacer los demas
    /// </summary>
    public class AccountController : BaseApiController
    {
        public IEnumerable<Usuario> GetAll()
        {
            
            var usrGetParams = new UsuarioGetParams { IdLocalidadNegocio = this.idLocalidadNegocio};
            var data = BLLPrestamo.Instance.GetUsuarios(usrGetParams);
            return data;
        }

        public Usuario Get(string loginName)
        {

            var usrGetParams = new UsuarioGetParams { LoginName = loginName };
            var data = BLLPrestamo.Instance.GetUsuarios(usrGetParams);
            return data.FirstOrDefault();
        }

        public LoginResponse  Login(string loginName, string password)
        {
            var result = BLLPrestamo.Instance.Login(loginName, password, this.idLocalidadNegocio);
            return result;
        }

        [HttpPost]
        public IHttpActionResult Post(Usuario usuario)
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
        public IHttpActionResult CambiarContraseña(int idUsuario, string nuevaContraseña)
        {
            try
            {
                // todo indagar con ernesto si en el api, debe tener todo ese proceso que se tiene en la vista
                // a veces de validar contrasena anterior, u otra opcion, analizarlo con el
                BLLPrestamo.Instance.CambiarContrasena(idUsuario, nuevaContraseña);
                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception("Registro no pudo ser guardado");
            }
            //return RedirectToAction("CreateOrEdit");
        }

        [HttpPost]
        public IHttpActionResult OlvideLaContraseña(int idUsuario)
        {
            // metodo para restaurar contrasena, sera segun el usuario siu
            
            return null;
        }


        [HttpPost]
        public IHttpActionResult Delete(int idUsuario)
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
