using PrestamoEntidades;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using PrestamoWS.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace PrestamoWS.Controllers
{
    
    public abstract class ControllerBasePrestamoWS : ControllerBase
    {
        [Inject]
        protected IPathProvider pathProvider { get; set; } = new PathProvider();

        protected int IdLocalidadNegocio { get; } = 1;

        protected int IdNegocio { get; } = 1;

        protected string LoginName { get { return this.GetLoginName(); }  }

        
        private string GetLoginName()
        {
            return "development"+DateTime.Now;
        }

        protected string ImagePathForClientes => pathProvider.MapPath(@"\images\clientes");
        protected string ImagePathForGarantia => pathProvider.MapPath(@"\images\garantias");
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

        #region errors
        protected IActionResult  InternalServerError(string message)
        {
            var ServerError = StatusCode(StatusCodes.Status500InternalServerError, message);
            return ServerError;
        }
        #endregion

    }



}
