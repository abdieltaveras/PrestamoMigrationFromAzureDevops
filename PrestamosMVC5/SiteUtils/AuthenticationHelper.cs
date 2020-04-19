using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public static readonly string UsuarioImageFile = "userImage";
        public static readonly string UsuarioIdKey = "idUsuario";
        public static readonly string NegocioIdKey = "idNegocio";
        public static readonly string NegocioNombreKey = "negocioNombre";
        public static readonly string NegocioLogoKey = "negocioLogo";
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
            idUsuarioObj = getKeyValue(UsuarioIdKey);
            return idUsuarioObj == null ? -1 : Convert.ToInt32(idUsuarioObj);
        }
        public static string GetUserImageFilePath(HttpSessionStateBase sessionState = null)
        {
            var imageFile = getKeyValue(UsuarioImageFile);
            return imageFile == null ? SiteImages.NoImage : SiteDirectory.ImagesForUsuario + "/" + imageFile;
        }
        
        // to retrieve IdNegocio value from session
        public static int GetIdNegocio(HttpSessionStateBase sessionState = null)
        {
            object idNegObj = getKeyValue(NegocioIdKey);
            var returnValue = idNegObj == null ? -1 : Convert.ToInt32(idNegObj);
            return returnValue;
        }

        public static string GetNegocioNombre(HttpSessionStateBase sessionState = null)
        {
            object data = getKeyValue(NegocioNombreKey);
            var returnValue = data == null ? string.Empty : data.ToString();
            return returnValue;
        }
        public static string GetNegocioLogo(HttpSessionStateBase sessionState = null)
        {
            object data = getKeyValue(NegocioLogoKey);
            var returnValue = data == null ? SiteImages.NoImage : SiteDirectory.ImagesForNegocio+"/"+ data.ToString();
            return returnValue;
        }
        public static void LoginUserToSession(int idNegocio, string usuario, int idUsuario, string userImageFilePath)
        {
            // get negocio info
            
            var sessionState = HttpContext.Current.Session;
            sessionState.Timeout = 60 * 5;
            sessionState.Add(UsuarioKey, usuario);
            sessionState.Add(NegocioIdKey, idNegocio);
            sessionState.Add(UsuarioIdKey, idUsuario);
            sessionState.Add(UsuarioImageFile, userImageFilePath);
            var negocio = BLLPrestamo.Instance.GetNegocios(new NegociosGetParams { IdNegocio = idNegocio, Usuario = usuario,  PermitirOperaciones=-1 }).FirstOrDefault();
            sessionState.Add(NegocioNombreKey, negocio.NombreComercial);
            sessionState.Add(NegocioLogoKey, negocio.Logo);
        }

        public static void SetOperacionesToUserSession(List<string> operaciones)
        {
            List<string> ListaOperaciones = operaciones;
            sessionState.Add(Operaciones, ListaOperaciones.ToArray());
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
            sessionState.Remove(UsuarioImageFile);
            sessionState.Remove(NegocioIdKey);
            sessionState.Remove(UsuarioIdKey);
            sessionState.Remove(Operaciones);
            sessionState.Remove(NegocioLogoKey);
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
            var idNegocioResult = session[AuthInSession.NegocioIdKey];
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
