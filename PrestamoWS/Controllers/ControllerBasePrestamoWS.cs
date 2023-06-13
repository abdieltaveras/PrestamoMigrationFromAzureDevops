using PrestamoEntidades;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace PrestamoWS.Controllers
{
    
    public abstract class ControllerBasePrestamoWS : ControllerBase
    {
        public static string NoImageBase64 = "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";
        protected IWebHostEnvironment WebHostingEnvironment { get; set; }

        protected int IdLocalidadNegocio { get { return GetIdLocalidadNegocio(); } }

        protected int IdNegocio { get ; } = 1;

        protected string LoginName { get { return this.GetLoginName(); }  }


        private string GetLoginName()
        {
            return "Usuario_Logueado"; //this.User.Identity.Name + DateTime.Now;
        }

        private int GetIdLocalidadNegocio()
        {
            return 1;
        }

        
        //private string GetLoginName()
        //{
        //    return "development"+DateTime.Now;
        //}

        protected string currentDir => Directory.GetCurrentDirectory();
        protected string ImagePathForClientes => currentDir + @"\imagesFor\Clientes\";
        protected string ImagePathDeleted => currentDir + @"\imagesFor\Deleted\";

        protected string ImagePathForClientesIdentificaciones => currentDir + @"\imagesFor\Clientes\Identificaciones";

        protected string ImagePathForGarantia => currentDir+ @"\imagesFor\Garantias\";
        protected string ImagePathForCodeudores => currentDir + @"\imagesFor\Codeudores\";
        protected string ImagePathForInversionistas => currentDir + @"\imagesFor\Inversionistas\";
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
                //IdUsuario = ,
                Usuario = this.User.Identity.Name
            };
        }
        //private InfoAccion InfoAccionFromSesion()
        //{
        //    // esto lo obtendra mas real por ahora es para desarrollo
        //    // la logica mas real sera mediante la vinculacion del sessionId y los valores
        //    // del login
        //    return new InfoAccion
        //    {
        //        IdAplicacion = 1,
        //        IdDispositivo = 1,
        //        IdLocalidadNegocio = 1,
        //        IdUsuario = 1,
        //        Usuario = "UsrDevelopement"
        //    };
        //}

        #region errors
        protected IActionResult  InternalServerError(string message)
        {
            var ServerError = StatusCode(StatusCodes.Status500InternalServerError, message);
            return ServerError;
        }
        #endregion

    }



}
