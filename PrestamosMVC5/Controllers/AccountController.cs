﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PrestamoBLL;
using PrestamoEntidades;
using PrestamosMVC5.Models;
using PrestamosMVC5.SiteUtils;
using System.Web.Helpers;
using static PrestamoBLL.BLLPrestamo;
using CaptchaMvc.HtmlHelpers;
// Todo: Poner en las opciones que permita ver el listado o directamente ir a crear sin necesidad de ver el listado   en guardarpermitir  en las pantallas guardar sin salir desde donde fue llamado, podemos hacerlo interceptando el metodo onsubmit del    post, y retornando de nuevo al formulario con valores iniciales correctos
namespace PrestamosMVC5.Controllers
{

    // todo: valodar r creando el procedimiento de creacion de una compania donde debe crear un administrador inicial con unos parametros dados pero que permita inmediatament sometan un login name que indique que debe cambiar la contrasena debe automaticamente redirigir al usuario a ello no esperar que ponga la contrasena. sino habilitar que realice el cambio de inmediato revisar el procedimiento que sea seguro.
    public class AccountController : ControllerBasePcp
    {
        public ActionResult test(string returnUrl = "")
        {
            var param = new NegociosGetParams { IdNegocio = -1 };
            var data = BLLPrestamo.Instance.GetNegocios(param);
            return Content(data.ToJson());
        }

        public ActionResult EstaElEquipoRegistrado()
        {
            if (RegistroEquipo.EstaRegistrado(this.Request))
            {
                return Content("Este equipo ya ha sido registrado");
            }
            else
            {
                return Content("Este equipo aun no ha sido registrado");
            }
        }
        [HttpGet]
        public ActionResult Login(string returnUrl = "")
        {

            this.pcpLogout();
            var model = new LoginModel { ReturnUrl = returnUrl };
#if (DEBUG)
            model.LoginName = "bryan";
            model.Password = "1";
            model.ValidateCaptcha = false;
#endif
            var negociosMatriz = BLLPrestamo.Instance.GetNegociosMatrizRaiz();
            model.SoloHayUnNegocioMatriz = negociosMatriz.Count() == 1;

            if (model.SoloHayUnNegocioMatriz)
            {
                TempData.Add("SoloHayUnNegocioMatriz", model.SoloHayUnNegocioMatriz);
                TempData.Add("IdNegocioMatriz", negociosMatriz.FirstOrDefault().IdNegocioMatriz);
                model.NegocioMatrizCuandoSoloHayUno = negociosMatriz.FirstOrDefault();

            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Login2(string returnUrl = "")
        {
            this.pcpLogout();
            var model = new LoginModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginView)
        {
            ActionResult _actResult = View(loginView);
            bool validateCaptcha = true;
#if (DEBUG)
            validateCaptcha = loginView.ValidateCaptcha;
#endif
            if (validateCaptcha && !this.IsCaptchaValid(""))
            {
                ModelState.AddModelError("CaptchaInputText", "las letras digitadas no coinciden");
                return View();
            }

            if (ModelState.IsValid)
            {
                var getUsr = new Usuario { LoginName = loginView.LoginName, IdNegocio = loginView.IdNegocio, Contraseña = loginView.Password };


                var result = BLLPrestamo.Instance.LoginUser(getUsr);

                if (result.ValidationMessage.UserValidationResult != BLLPrestamo.UserValidationResult.Sucess)
                {
                    this.UpdShowSummaryErrorsTime(9);
                    ModelState.AddModelError("", result.ValidationMessage.Mensaje + ", tambien podria no haber elegido la empresa o negocio correcto");
                    _actResult = WhatTodo(result.ValidationMessage, _actResult, loginView);
                }
                else
                {
                    if (TempData["SoloHayUnNegocioMatriz"] == null)
                    {
                        var idNegocioMatriz = Convert.ToInt32(TempData["SoloHayUnNegocioMatriz"]);
                        TempData["loginResponse"] = result;
                        //_actResult = RedirectToAction("ElegirNegocioAOperar", new { idNegocio = result.Usuario.IdNegocio, returnUrl = loginView.ReturnUrl, token = internalToken });
                        return ElegirNegocioAOperar(loginView.IdNegocio,  result.Usuario, loginView.ReturnUrl);
                    }
                    //_actResult = GoToNextUrl(loginView);
                }
            }
            return _actResult;
        }

        private ActionResult GoToNextUrl(string returnUrl)
        {
            ActionResult _actResult;
            if (string.IsNullOrEmpty(returnUrl) || returnUrl == "/")
            {
                _actResult = RedirectToAction("index", "home");
            }
            else
            {
                _actResult = Redirect(returnUrl);
            }
            return _actResult;
        }

        private void SetLoginDataIntoSessiond(int IdNegocioMatriz, Usuario usuario)
        {
            this.LoginUserIntoSession(IdNegocioMatriz , usuario.LoginName, usuario.IdUsuario, usuario.ImgFilePath);
            //AuthInSession.CreateUserWithIdNegocioInSession(this.Session, loginView.IdNegocio, loginView.LoginName, string.Empty);
            var operacionesConAcceso = BLLPrestamo.Instance.GetOperaciones(new UsuarioOperacionesGetParams() { IdUsuario = usuario.IdUsuario });
            AuthInSession.SetOperacionesToUserSession(operacionesConAcceso);
        }

        // attention prevent a method from being called by url declare private  son only another method in the controller will have access, REMEMBER TO SPECIFY the Vie by its name if it is not specified will call the view with the name of the method that was called from.
        
        [HttpGet]
        private ActionResult ElegirNegocioAOperar(int idNegocioMatriz, Usuario usuario, string returnUrl = "")
        {
             var model = new ListaNegocioVM();
            TempData["idNegocioMatriz"] = idNegocioMatriz;
            TempData["Usuario"] = usuario;
            // var negocio = BLLPrestamo.Instance.GetNegocios(new NegociosGetParams { IdNegocio = idNegocioMatriz  });
            // var negocios = BLLPrestamo.Instance.GetNegocioYSusHijos(usuario.IdNegocio);
            // model.NombreEmpresaMatriz = BLLPrestamo.Instance.GetNegocios(new NegociosGetParams { IdNegocio = idNegocioMatriz }).FirstOrDefault().NombreComercial;
            model.returnUrl = returnUrl;
            model.SelectNegocios = SelectItems.NegociosOperacionalesForMatriz(usuario.IdNegocio);
            model.NombreEmpresaMatriz = BLLPrestamo.Instance.GetNegocios(new NegociosGetParams { IdNegocio = idNegocioMatriz }).FirstOrDefault().NombreComercial;
            return View("ElegirNegocioAOperar",model);
        }

        [HttpPost]
        public ActionResult IniciarOperacion(ListaNegocioVM model)
        {
            var usuarioInfo = TempData["usuario"] as Usuario;
            var idNegocioMatrizValue = TempData["idNegocioMatriz"];
            int idNegocioMatriz = 0;
            if (idNegocioMatrizValue != null)
            {
                idNegocioMatriz = (int)idNegocioMatrizValue;
            }
            SetLoginDataIntoSessiond(idNegocioMatriz, usuarioInfo);
            return GoToNextUrl(model.returnUrl);
        }
        private ActionResult WhatTodo(UserValidationResultWithMessage userValidationResultMessage, ActionResult actResult, LoginModel loginModel)
        {

            var userValidationResult = userValidationResultMessage.UserValidationResult;

            switch (userValidationResult)
            {
                case BLLPrestamo.UserValidationResult.MustChangePassword:
                    var data = new ChangePasswordModel { LoginName = loginModel.LoginName, IdNegocio = loginModel.IdNegocio, Motivo = userValidationResultMessage.Mensaje };
                    actResult = RedirectToAction("ChangePassword", data);
                    break;
                case BLLPrestamo.UserValidationResult.ExpiredPassword:
                    //ATTENTION: si envias el valor del route asi RedirectToAction("ChangePassword", new {model=data} en los redirectoActoion no funciona, se envio directamente el objero RedirectToAction("ChangePassword", data);
                    data = new ChangePasswordModel { LoginName = loginModel.LoginName, IdNegocio = loginModel.IdNegocio, Motivo = userValidationResultMessage.Mensaje };
                    return RedirectToAction("ChangePassword", data);

                /*case BLLPrestamo.UserValidationResult.NoUserFound:
                    break;
                case BLLPrestamo.UserValidationResult.InvalidPassword:
                    break;
                case BLLPrestamo.UserValidationResult.Blocked:
                    break;
                case BLLPrestamo.UserValidationResult.Inactive:
                    break;
                case BLLPrestamo.UserValidationResult.ExpiredAccount:
                    break;
                case BLLPrestamo.UserValidationResult.Sucess:
                    break;
                case BLLPrestamo.UserValidationResult.NoUserRegistered:
                    break;
                */
                default:
                    break;
            }
            return actResult;
        }

        // Task: develope Forgot Password
        [HttpGet]
        public ActionResult ForgotPassword(int IdNegocio, string LoginName)
        {
            return Content("not implemented yet");

        }
        [HttpGet]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel model, string NoData = "")
        {
            // Attention: Cuando se ponen propiedades en el html fuera del Form estas no hacen binding
            // ni tampoco sin son label tienen que ser input dentro del form para hacer el binding
            try
            {
                model.LoginName = AuthInSession.IsAnonimousUser ? model.LoginName : AuthInSession.GetLoginName();
                var searchData = new UsuarioGetParams { IdNegocio = model.IdNegocio, LoginName = model.LoginName };
                var usuario = BLLPrestamo.Instance.GetUsuarios(searchData).FirstOrDefault();
                model.IdUsuario = usuario.IdUsuario;
                var data = new ChangePassword { Contraseña = model.Contraseña, Usuario = model.LoginName, IdUsuario = model.IdUsuario };
                BLLPrestamo.Instance.UsuarioChangePassword(data);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }

            return RedirectToAction("login");

        }
        // Task: develope Reset Password
        [HttpGet]
        public ActionResult ResetPassword(string returnUrl = "")
        {
            return Content("not implemented action yet");
        }

        [HttpGet]
        public ActionResult AskActionsForAccount()
        {
            return Content("not implemented action yet");
        }
        public ActionResult LogOutUser()
        {
            this.pcpLogout();
            return Redirect(Url.Content("~/"));
        }
        #region operaciones
        private void SendEmail(string to, string content)
        {
            try
            {
                WebMail.SmtpServer = "smtp.example.com"; WebMail.SmtpPort = 587; WebMail.EnableSsl = true; WebMail.UserName = "mySmtpUsername"; WebMail.Password = "mySmtpPassword"; WebMail.From = "rsvps@example.com";
                string subject = "Notificacion Cambio de contrasena";
                string from = "noreply@pcpapps.com";
                WebMail.Send(to, subject, subject, from);
            }
            catch (Exception e)
            {

            }
        }

        #endregion
    }
}
