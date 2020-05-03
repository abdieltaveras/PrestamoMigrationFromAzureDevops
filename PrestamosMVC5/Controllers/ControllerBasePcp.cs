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
        public ControllerBasePcp()
        {
            UpdViewBag_ShowSummaryErrorsTime(8);
            UpdViewBag_ShowSideBar(true);
            UpdViewBag_LoadCssAndJsGrp2(false);
        }
        public int ShowSummaryErrorsTime = 5;

        // indicate if user is authenticated
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            //ViewBag.ShowSummaryErrorsTime = 11;
        }

        protected void UpdViewBag_LoadCssAndJsGrp2(bool value)
        {
            ViewBag.LoadCssAndJsGrp2 = value;
        }

        /// <summary>
        /// update value for the ViewBag.ShowSummaryErrorsTime so in javascript 
        /// the funcion multiply this value by 1000 to get time in seconds
        /// </summary>
        /// <param name="seconds"></param>
        protected void UpdViewBag_ShowSummaryErrorsTime(int seconds)
        {
            ViewBag.ShowSummaryErrorsTime = seconds;
            
        }

        /// <summary>
        /// update value for the ViewBag.ShowSummaryErrorsTime so in javascript 
        /// the funcion multiply this value by 1000 to get time in seconds
        /// </summary>
        /// <param name="seconds"></param>
        public void UpdViewBag_ShowSideBar(bool value)
        {
            ViewBag.ShowSideBar = value;
        }

        protected bool pcpIsUserAuthenticated => (AuthInSession.GetLoginName() != AuthInSession.AnonimousUser);
        protected string pcpUserLoginName => AuthInSession.GetLoginName(); //TODO: verificar si es necesario que este private
        protected string pcpUserImageFile => AuthInSession.GetUserImageFilePath();
        protected int pcpUserIdNegocio => AuthInSession.GetSelectedNegocioId();
        protected int pcpUserIdUsuario => AuthInSession.GetIdUsuario();
        
        protected void LoginUserIntoSession(int idNegocioSelectedToWork, string loginName, int idUsuario, string nombreReal, string userImageFile) =>
            AuthInSession.LoginUserToSession(idNegocioSelectedToWork, loginName, idUsuario, nombreReal, userImageFile);
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
        /// <summary>
        /// Permite obtener el valor de una llave especificada del TempData , valida si esta nula devuelve un 
        /// objeto por defecto o devuelve el contenido convertido al tipo deseado
        /// debe ser un objeto si es string lo devolvera vacio
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        protected T GetValueFromTempData<T>(string key) where T : class, new()
        {
            var result = (TempData[key] == null) ? new T() : TempData[key] as T;
            return result;
        }
    }

}