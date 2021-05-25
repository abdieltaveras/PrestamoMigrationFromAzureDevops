using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Http;

namespace WSPrestamo.Controllers
{
    
    public abstract class BaseApiController : ApiController
    {

        protected int IdLocalidadNegocio { get; } = 1;

        protected int IdNegocio { get; } = 1;

        protected string LoginName { get { return this.GetLoginName(); }  }

        

        private string GetLoginName()
        {
            return "development"+DateTime.Now;
        }

        protected bool IsUserAuthenticaded => UserIsAuthenticated();
        protected InfoAccion InfoAccion { get { return this.InfoAccionFromSesion(); } }

        private bool UserIsAuthenticated() => !string.IsNullOrEmpty(this.LoginName);

        private InfoAccion InfoAccionFromSesion()
        {
            // esto lo obtendra mas real por ahora es para desarrollo
            // la logica mas real sera mediante la vinculacion del sessionId y los valores
            // del login
            return new InfoAccion
            {
                IdAplicacion = 1,
                IdDispositivo = 1,
                IdLocalidadNegocio = 1,
                IdUsuario = 1,
                Usuario = "UsrDevelopement"
            };
        }

    }


    
}
