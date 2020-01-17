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

namespace PrestamosMVC5.Controllers
{
    
    public class AccountController : Controller
    {

        [HttpGet]
        public ActionResult Login(string returnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return LogOut();
            }
            var model = new LoginModel { ReturnUrl = returnUrl };
            var prevRequest = HttpContext.Request;
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginView, string ReturnUrl = "")
        {
            if (ModelState.IsValid)
            {
                // getUserInfo
                var usuario = new Usuario { LoginName = loginView.LoginName, IdNegocio = 1, ImgFilePath = "/Content/Images/ForEntities/ToTest.jpg" };
                AuthInSession.CreateUserWithIdNegocioInSession(HttpContext.Session, usuario.IdNegocio, usuario.LoginName, usuario.ImgFilePath);
            
                if (loginView.ReturnUrl == null)
                {
                    return RedirectToAction("index", "home");
                }
                else
                {
                    return Redirect(loginView.ReturnUrl);
                }
            }
            else
            {
                ModelState.AddModelError("", "Something Wrong : Username or Password invalid ^_^ ");
                return View(loginView);
            }
            
        }

        [HttpGet]
        public ActionResult ForgotPassword(string returnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return LogOut();
            }
            var model = new LoginModel { ReturnUrl = returnUrl };
            var prevRequest = HttpContext.Request;
            return View(model);
        }

        [HttpGet]
        public ActionResult ResetPassword(string returnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return LogOut();
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
        public ActionResult LogOut()
        {
            AuthInSession.Logout();
            return RedirectToAction("Login", "Account", null);
        }
    }

}
