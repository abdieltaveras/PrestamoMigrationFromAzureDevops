using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PrestamosMVC5.SiteUtils

{
    /// <summary>
    /// Authentication using session values
    /// </summary>
    public static class AuthInSession
    {

        public static readonly string UsuarioKey = "user";
        public static readonly string UserImageFilePathKey = "userImage";
        public static readonly string NegocioKey = "negocio";
        public static readonly string IdUsuarioKey = "idUsuario";
        public static readonly string AnonimousUser = "Anónimo";
        public static readonly string Operaciones = "operaciones";
        public static System.Web.SessionState.HttpSessionState sessionState => HttpContext.Current.Session;


        private static object getKeyValue(string key)
        {
            return sessionState[key];
            //HttpContext.Current.Session[key] :
            //sessionState[key];
        }
        public static bool IsAnonimousUser => (GetLoginName() == AnonimousUser);
        public static string GetLoginName(HttpSessionStateBase sessionState = null)
        //HttpSessionStateBase sessionState = null)
        {
            object usuarioObj = null;
            usuarioObj = getKeyValue(UsuarioKey);

            return usuarioObj == null ? AnonimousUser : usuarioObj.ToString();
        }
        public static int GetIdUsuario(HttpSessionStateBase sessionState = null)
        {
            object idUsuarioObj = null;
            idUsuarioObj = getKeyValue(IdUsuarioKey);
            return idUsuarioObj == null ? -1 : Convert.ToInt32(idUsuarioObj);
        }
        public static string GetUserImageFilePath(HttpSessionStateBase sessionState = null)
        {
            var imagePath = getKeyValue(UserImageFilePathKey);
            return imagePath == null ? string.Empty : imagePath.ToString();
        }
        
        // to retrieve IdNegocio value from session
        public static int GetIdNegocio(HttpSessionStateBase sessionState = null)
        {
            object idNegObj = getKeyValue(NegocioKey);
            var returnValue = idNegObj == null ? -1 : Convert.ToInt32(idNegObj);
            return returnValue;
        }


        public static void LoginUserToSession(int idNegocio, string usuario, int idUsuario, string userImageFilePath)
        {
            var sessionState = HttpContext.Current.Session;
            sessionState.Add(UsuarioKey, usuario);
            sessionState.Add(NegocioKey, idNegocio);
            sessionState.Add(NegocioKey, idNegocio);
            sessionState.Add(IdUsuarioKey, idUsuario);
            sessionState.Add(UserImageFilePathKey, userImageFilePath);
            sessionState.Timeout = 60 * 5;
        }

        public static void SetOperacionesToUserSession(List<string> operaciones)
        {
            //Session["your_array"] = new string[] { "Hola", "Adios" };
            sessionState.Add(Operaciones, operaciones.ToArray());
        }

        public static string[] GetOperacionesToUserSession()
        {
            return (string[])getKeyValue(Operaciones);
        }

        /// <summary>
        /// To assigt values to Usuario and IdNegocio variables from session info
        /// </summary>
        /// <param name="entidad"></param>
        //public static void SetUsuarioAndIdNegocioTo(BaseUsuarioEIdNegocio entidad)
        //{
        //    entidad.IdNegocio = GetIdNegocio();
        //    entidad.Usuario = GetLoginName();
        //}

        //public static void SetUsuarioTo(BaseUsuario entidad)
        //{
        //    entidad.Usuario = GetLoginName();
        //}

        public static void Logout()
        {
            var sessionState = HttpContext.Current.Session;
            sessionState.Remove(UsuarioKey);
            sessionState.Remove(NegocioKey);
            sessionState.Remove(IdUsuarioKey);
        }
    }

    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        private bool IsAuthenticated = false;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            IsUserAuthenticatedInSession(httpContext.Session);
            return IsAuthenticated;
        }

        private bool IsUserAuthenticatedInSession(HttpSessionStateBase session)
        {
            var user = session[AuthInSession.UsuarioKey];
            //var user = AuthInSession.GetLoginName();
            var idNegocioResult = session[AuthInSession.NegocioKey];
            //var idNegocioResult = AuthInSession.GetIdNegocio();
            IsAuthenticated = (user != null && idNegocioResult != null);
            return IsAuthenticated;
        }

        
        public override void OnAuthorization(AuthorizationContext contx)
        {
        //    bool IsAuthenticAttribute =
        //(filterContext.ActionDescriptor.IsDefined(typeof(Authenticate), true) ||
        //filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AuthenticateAttribute), true)) &&
        //filterContext.HttpContext.User.Identity.IsAuthenticated;

        //    if (!IsAuthenticAttribute)
        //    {
        //        base.OnAuthorization(filterContext);
        //    }
            
            IsUserAuthenticatedInSession(contx.HttpContext.Session);
            if (!IsAuthenticated)
            {
                HandleUnauthorizedRequest(contx); //call the HandleUnauthorizedRequest function
            }
            
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary(
                                new
                                {
                                    controller = "Account",
                                    action = "Login",
                                    ReturnUrl = HttpContext.Current.Request.Url,
                                }
                            )); ;
            }
        }
    }
}
