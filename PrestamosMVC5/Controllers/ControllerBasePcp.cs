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
        protected bool pcpIsUserAuthenticated=> (AuthInSession.GetLoginName() != AuthInSession.AnonimousUser);
        private string pcpUserLoginName => AuthInSession.GetLoginName();
        protected string pcpUserImageFile => AuthInSession.GetUserImageFilePath();
        protected int pcpUserIdNegocio => AuthInSession.GetIdNegocio();
        protected int pcpUserIdUsuario => AuthInSession.GetIdUsuario();
        protected void LoginUserIntoSession(int idNegocio, string loginName, string userImageFile)=>
            AuthInSession.LoginUserToSession(idNegocio, loginName, userImageFile);

        protected void pcpLogout() => AuthInSession.Logout();

        protected  void pcpSetUsuarioAndIdNegocioTo(BaseUsuarioEIdNegocio entidad)
        {
            entidad.IdNegocio = pcpUserIdNegocio;
            entidad.Usuario = pcpUserLoginName;
        }
        
        protected void pcpSetUsuarioAndIdNegocioTo(BaseAnularParams entidad)
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

    }
}