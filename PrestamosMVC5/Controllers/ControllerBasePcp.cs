using PrestamoBLL;
using PrestamoEntidades;
using PrestamosMVC5.SiteUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Controllers
{
    /// <summary>
    /// this class is a base class with session authentication and other important things
    /// </summary>
    public class ControllerBasePcp : Controller
    {
        // indicate if user is authenticated
        protected bool pcpIsUserAuthenticated => (AuthInSession.GetLoginName() != AuthInSession.AnonimousUser);
        protected string pcpUserLoginName => AuthInSession.GetLoginName(); //TODO: verificar si es necesario que este private
        protected string pcpUserImageFile => AuthInSession.GetUserImageFilePath();
        protected int pcpUserIdNegocio => AuthInSession.GetIdNegocio();
        protected int pcpUserIdUsuario => AuthInSession.GetIdUsuario();
        protected void LoginUserIntoSession(int idNegocio, string loginName, int idUsuario, string userImageFile) =>
            AuthInSession.LoginUserToSession(idNegocio, loginName, idUsuario, userImageFile);
        protected void SetOperacionesToSession(List<string> operaciones) =>
            AuthInSession.SetOperacionesToUserSession(operaciones);

        protected void pcpLogout() => AuthInSession.Logout();

        protected void pcpSetUsuarioAndIdNegocioTo(BaseUsuarioEIdNegocio entidad)
        {
            entidad.IdNegocio = pcpUserIdNegocio;
            entidad.Usuario = pcpUserLoginName;
        }
        /// <summary>
        ///  set Id negocio property 
        /// </summary>
        /// <param name="entidad"></param>
        protected void pcpSetIdNegocioTo(BaseIdNegocio entidad)
        {
            entidad.IdNegocio = pcpUserIdNegocio;
        }
        /// <summary>
        ///  set usuario property 
        /// </summary>
        /// <param name="entidad"></param>
        protected void pcpSetUsuarioTo(BaseUsuario entidad)
        {
            entidad.Usuario = pcpUserLoginName;
        }
        public IEnumerable<ModelError> GetErrorsFromModelState(ModelStateDictionary modelState)
        {
            var result = modelState.Values.SelectMany(v => v.Errors);
            return result;
        }

        protected string GetErrorsFromModelStateAsString(ModelStateDictionary modelState)
        {
            var result = string.Join("; ", GetErrorsFromModelState(modelState).Select(x => x.ErrorMessage));
            return result;
        }
    }

}