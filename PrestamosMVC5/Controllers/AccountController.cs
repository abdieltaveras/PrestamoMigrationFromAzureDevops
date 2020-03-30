using Newtonsoft.Json;
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

namespace PrestamosMVC5.Controllers
{
    // TODO: VALIDAR si la cuenta esta fuera del periodo de vigencia,  si la contrasena ya expiro que debe cambiarla
    // todo: valodar r creando el procedimiento de creacion de una compania
    // donde debe crear un administrador inicial
    // con unos parametros datos pero que permita inmediatament sometan un login name
    // que indique que debe cambiar la contrasena debe automaticamente
    // redirigir al usuario a ello no esperar que ponga la contrasena.
    // sino habilitar que realice el cambio de inmediato
    // revisar el procedimiento que sea seguro.
    public class AccountController : ControllerBasePcp
    {
        public ActionResult test(string returnUrl = "")
        {
            var param = new NegociosGetParams { IdNegocio = -1 };
            var data = BLLPrestamo.Instance.GetNegocios(param);
            return Content(data.ToJson());
        }

        [HttpGet]
        public ActionResult Login(string returnUrl = "")
        {
            this.pcpLogout();
            var model = new LoginModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginView)
        {
            ActionResult _actResult = View(loginView);
            if (ModelState.IsValid)
            {
                var getUsr = new Usuario { LoginName = loginView.LoginName, IdNegocio = loginView.IdNegocio, Contraseña = loginView.Password };
                var result = BLLPrestamo.Instance.LoginUser(getUsr);
                if (result.ValidationMessage.UserValidationResult != BLLPrestamo.UserValidationResult.Sucess)
                {
                    ModelState.AddModelError("", result.ValidationMessage.Mensaje);
                    _actResult = WhatTodo(result.ValidationMessage.UserValidationResult, _actResult, loginView);
                }
                else
                {
                    this.LoginUserIntoSession(loginView.IdNegocio, loginView.LoginName, result.Usuario.IdUsuario, loginView.ImagePath);
                    //AuthInSession.CreateUserWithIdNegocioInSession(this.Session, loginView.IdNegocio, loginView.LoginName, string.Empty);
                    var operacionesConAcceso = BLLPrestamo.Instance.GetOperaciones(new UsuarioOperacionesGetParams() { IdUsuario = result.Usuario.IdUsuario });
                    AuthInSession.SetOperacionesToUserSession(operacionesConAcceso);
                    if (string.IsNullOrEmpty(loginView.ReturnUrl) || loginView.ReturnUrl == "/")
                    {
                        _actResult = RedirectToAction("index", "home");
                    }
                    else
                    {
                        _actResult = Redirect(loginView.ReturnUrl);
                    }
                }
            }
            return _actResult;
        }

        private ActionResult WhatTodo(BLLPrestamo.UserValidationResult userValidationResult, ActionResult actResult, LoginModel loginModel)
        {

            switch (userValidationResult)
            {
                case BLLPrestamo.UserValidationResult.MustChangePassword:
                    actResult = RedirectToAction("ChangePassword");
                    break;
                case BLLPrestamo.UserValidationResult.ExpiredPassword:
                    //ATTENTION: si envias el valor del route value asi new {model=data} no funciona
                    var data = new ChangePasswordModel { LoginName = loginModel.LoginName, IdNegocio = loginModel.IdNegocio };
                    return RedirectToAction("ChangePassword", data);
                //break;
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
        public ActionResult ForgotPassword(string returnUrl = "")
        {
            return Content("not implemented action yet");
        }
        [HttpGet]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            //ori
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel model, bool fromAntoherUser = false)
        {
            // Attention: Cuando se ponen propiedades en el html fuera del Form estas no hacen binding
            // ni tampoco sin son label tienen que ser input dentro del form para hacer el binding
            try
            {
                if (fromAntoherUser)
                {
                    model.LoginName = AuthInSession.GetLoginName();
                }
                else
                {
                    var searchData = new UsuarioGetParams { IdNegocio = model.IdNegocio, LoginName = model.LoginName };
                    var usuario = BLLPrestamo.Instance.GetUsuarios(searchData).FirstOrDefault();
                    model.IdUsuario = usuario.IdUsuario;
                }
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
