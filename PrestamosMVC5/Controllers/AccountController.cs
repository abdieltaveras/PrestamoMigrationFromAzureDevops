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
                if (result.UserValidationResult != BLLPrestamo.UserValidationResult.Sucess)
                {
                    ModelState.AddModelError("", result.Mensaje);
                }
                else
                {
                    this.LoginUserIntoSession(loginView.IdNegocio, loginView.LoginName, loginView.ImagePath);
                    //AuthInSession.CreateUserWithIdNegocioInSession(this.Session, loginView.IdNegocio, loginView.LoginName, string.Empty);
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
        // Task: develope Forgot Password
        [HttpGet]
        public ActionResult ForgotPassword(string returnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return LogOutUser();
            }
            var model = new LoginModel { ReturnUrl = returnUrl };
            var prevRequest = HttpContext.Request;
            return View(model);
        }
        // Task: develope Reset Password
        [HttpGet]
        public ActionResult ResetPassword(string returnUrl = "")
        {
            this.LogOutUser();
            if (pcpIsUserAuthenticated)
            {
                return LogOutUser();
            }
            var model = new LoginModel { ReturnUrl = returnUrl };
            var prevRequest = HttpContext.Request;
            return View(model);
        }

        [HttpGet]
        public ActionResult AskActionsForAccount()
        {
            return Content("not implemented action yet");
        }
        public ActionResult LogOutUser()
        {
            this.pcpLogout();
            //return RedirectToAction("Login", "Account", null);
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
