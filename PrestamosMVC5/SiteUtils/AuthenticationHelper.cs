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

        public static readonly string UsuarioLoginName = "sal*$%#dsd@##DSas@#";
        public static readonly string UsuarioNombreReal = "$$dfgf^%%$%ASasds@##$4";
        public static readonly string UsuarioImageFile = "%$#@SDFR$";
        public static readonly string UsuarioId = "@S_DF#(J#$$df%^%^fr";
        public static readonly string NegocioSelectedIdNegocio = "$#!sdf&^$#";
        public static readonly string NegocioSelectedNombre = "@@]/*-*/kiSel0p9sdf&^$#";
        public static readonly string NegocioLogoKey = "$#@@fdf09&^%8jhkds&";
        public static readonly string AnonimousUser = "Anonimo";
        public static readonly string Operaciones = "#$%%^&&&$kjklsal";

        public static readonly string NegocioMatrizIdNegocio= "$ed!@rdr^%%sdsd";
        public static readonly string NegocioMatrizNombre = "$$##dsd%$$%%";

        public static readonly string NegocioPadreIdNegocio = "@df))7%$%h&^7";
        public static readonly string NegocioPadreNombre = "nn**/-@7%$%h&^7df))";
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
            usuarioObj = getKeyValue(UsuarioLoginName);

            return usuarioObj == null ? AnonimousUser : usuarioObj.ToString();
        }

        public static string GetUsuarioNombreReal(HttpSessionStateBase sessionState = null)
        //HttpSessionStateBase sessionState = null)
        {
            object usuarioObj = null;
            usuarioObj = getKeyValue(UsuarioNombreReal);
            return usuarioObj == null ? AnonimousUser : usuarioObj.ToString();
        }

        private static string GetAnonimoName() => "Anonimo";
        

        public static int GetIdUsuario(HttpSessionStateBase sessionState = null)
        {
            object idUsuarioObj = null;
            idUsuarioObj = getKeyValue(UsuarioId);
            return idUsuarioObj == null ? -1 : Convert.ToInt32(idUsuarioObj);
        }

        public static string GetUserImageFilePath(HttpSessionStateBase sessionState = null)
        {
            var imageFile = getKeyValue(UsuarioImageFile);
            return imageFile == null ? SiteImages.NoImage : SiteDirectory.ImagesForUsuario + "/" + imageFile;
        }
        
        // to retrieve IdNegocio value from session
        public static int GetSelectedNegocioId(HttpSessionStateBase sessionState = null)
        {
            object idNegObj = getKeyValue(NegocioSelectedIdNegocio);
            var returnValue = idNegObj == null ? -1 : Convert.ToInt32(idNegObj);
            return returnValue;
        }

        public static string GetNegocioSelectedNombre(HttpSessionStateBase sessionState = null)
        {
            object data = getKeyValue(NegocioSelectedNombre);
            var returnValue = data == null ? string.Empty : data.ToString();
            return returnValue;
        }
        public static string GetStringValueForKey(string key, HttpSessionStateBase sessionState = null)
        {
            object data = getKeyValue(key);
            var returnValue = data == null ? string.Empty : data.ToString();
            return returnValue;
        }

        public static int GetIntValueForKey(string key, HttpSessionStateBase sessionState = null)
        {
            object idNegObj = getKeyValue(key);
            var returnValue = idNegObj == null ? -1 : Convert.ToInt32(idNegObj);
            return returnValue;
            
        }
        public static string GetNegocioLogo(HttpSessionStateBase sessionState = null)
        {
            object data = getKeyValue(NegocioLogoKey);
            var returnValue = data == null ? SiteImages.NoImage : SiteDirectory.ImagesForNegocio+"/"+ data.ToString();
            return returnValue;
        }
        public static void LoginUserToSession(int idNegocio, string usuario, int idUsuario, string usuarioNombreReal,  string userImageFilePath)
        {
            // get negocio info
            var sessionState = HttpContext.Current.Session;
            var negociosPadres = BLLPrestamo.Instance.GetNegocioySusPadres(idNegocio);
            var negocioMatriz = negociosPadres.Where(neg => neg.IdNegocioPadre <= 0).FirstOrDefault();
            var negocioSelected = negociosPadres.Where(neg => neg.IdNegocio == idNegocio).FirstOrDefault();
            var negocioPadre = BLLPrestamo.Instance.GetNegocios(new NegociosGetParams { IdNegocio = negocioSelected.IdNegocioPadre }).FirstOrDefault();

            sessionState.Timeout = 60 * 5;
            sessionState.Add(UsuarioLoginName, usuario);
            sessionState.Add(UsuarioId, idUsuario);
            sessionState.Add(UsuarioNombreReal, usuarioNombreReal);
            sessionState.Add(UsuarioImageFile, userImageFilePath);

            sessionState.Add(NegocioSelectedIdNegocio, idNegocio);
            sessionState.Add(NegocioSelectedNombre, negocioSelected.NombreComercial);

            sessionState.Add(NegocioMatrizIdNegocio, negocioMatriz.IdNegocio);
            sessionState.Add(NegocioMatrizNombre, negocioMatriz.NombreComercial);

            sessionState.Add(NegocioPadreIdNegocio, negocioPadre.IdNegocio);
            sessionState.Add(NegocioPadreNombre, negocioPadre.NombreComercial);

            var negocio = BLLPrestamo.Instance.GetNegocios(new NegociosGetParams { IdNegocio = idNegocio, Usuario = usuario,  PermitirOperaciones=-1 }).FirstOrDefault();
            // si el hijo no tiene logo buscar el de un papa que tenga logo
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
            sessionState.Remove(UsuarioLoginName);
            sessionState.Remove(UsuarioImageFile);
            sessionState.Remove(NegocioSelectedNombre);
            sessionState.Remove(UsuarioId);
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
            var user = session[AuthInSession.UsuarioLoginName];
            //var user = AuthInSession.GetLoginName();
            var idNegocioResult = session[AuthInSession.NegocioSelectedNombre];
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
