using PrestamoBLL;
using PrestamoEntidades;
using PrestamosMVC5.Models;
using PrestamosMVC5.SiteUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {

            return View(BLLPrestamo.Instance.GetUsuarios(new UsuarioGetParams { IdNegocio = AuthInSession.GetIdNegocio(), Usuario = AuthInSession.GetLoginName() }));
        }

        public ActionResult Test()
        {
            return View(new UserModel());
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        // GET: User/Create
        public ActionResult CreateOrEdit(int id = -1)
        {
            var model = new UserModel();
            if (id > 0)
            {
                var getUsuarioParam = new UsuarioGetParams
                {
                    Usuario = AuthInSession.GetLoginName(),
                    IdUsuario = id,
                };
                model.Usuario = BLLPrestamo.Instance.GetUsuarios(getUsuarioParam).FirstOrDefault();
                model.Usuario.Usuario = AuthInSession.GetLoginName();
            }
            return View(model);
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult CreateOrEdit(UserModel userModel)
        {
            ActionResult actionResult = View(userModel);
            if (userModel.Usuario.DebeCambiarContraseñaAlIniciarSesion)
            {
                ModelState.Remove("Contraseña");
                ModelState.Remove("ConfirmarContraseña");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    userModel.Usuario.Usuario = AuthInSession.GetLoginName();
                    BLLPrestamo.Instance.InsUpdUsuario(userModel.Usuario);
                    actionResult = RedirectToAction("index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(" ", e.Message);
                    
                }
            }
            return actionResult;
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
