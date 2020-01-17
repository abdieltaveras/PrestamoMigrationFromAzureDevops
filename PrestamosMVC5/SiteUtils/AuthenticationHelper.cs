using PrestamoEntidades;
using System;
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
        public static readonly string AnonimousUser = "Anonimo";

        public static string GetLoginName(HttpSessionStateBase sessionState = null)
        {
            object usuarioObj = null;
            usuarioObj = getKeyValue(sessionState, UsuarioKey);

            return usuarioObj == null ? AnonimousUser : usuarioObj.ToString();
        }
        
        private static object getKeyValue(HttpSessionStateBase sessionState, string key)
        {
            return sessionState == null ?
                HttpContext.Current.Session[key] :
                sessionState[key];
        }

        public static string GetUserImageFilePath(HttpSessionStateBase sessionState = null)
        {
            var imagePath = getKeyValue(sessionState, UserImageFilePathKey);
            return imagePath == null ? string.Empty : imagePath.ToString();
        }
        /// <summary>
        /// To assigt values to Usuario and IdNegocio variables from session info
        /// </summary>
        /// <param name="entidad"></param>
        public static void SetUsuarioYIdNegocioTo(BaseUsuarioEIdNegocio entidad)
        {
            entidad.IdNegocio = GetIdNegocio();
            entidad.Usuario = GetLoginName();
        }
        // to retrieve IdNegocio value from session
        public static int GetIdNegocio(HttpSessionStateBase sessionState = null)
        {
            object idNegObj = getKeyValue(sessionState, NegocioKey);
            return idNegObj==null? -1 : Convert.ToInt32(idNegObj);
        }


        public static void CreateUserWithIdNegocioInSession(HttpSessionStateBase sessionState, int idNegocio, string usuario, string userImageFilePath)
        {
            sessionState.Add(UsuarioKey, usuario);
            sessionState.Add(NegocioKey, idNegocio);
            sessionState.Add(UserImageFilePathKey, userImageFilePath);
            sessionState.Timeout = 60 * 5;
        }

        public static void Logout()
        {
            var sessionState = HttpContext.Current.Session;
            sessionState.Remove(UsuarioKey);
            sessionState.Remove(NegocioKey);
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