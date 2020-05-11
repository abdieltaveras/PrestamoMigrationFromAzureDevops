using PrestamoBLL;
using PrestamoBLL.Entidades;
using PrestamosMVC5.Models;
using PrestamosMVC5.SiteUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Controllers
{
    public class NegociosController : ControllerBasePcp
    {
        public NegociosController()
        {
            UpdViewBag_LoadCssAndJsGrp2(true);
            this.UpdViewBag_LoadCssAndJsForDatatable(true);
        }
        // GET: Negocios
        public ActionResult Index()
        {
            var searchParam = new NegociosGetParams { IdNegocio = AuthInSession.GetIntValueForKey(AuthInSession. NegocioMatrizIdNegocio), Usuario = this.pcpUserLoginName };
            var negocios = BLLPrestamo.Instance.GetNegocios(searchParam);
            return View(negocios);
        }

        // GET: Negocios/Details/5
        public ActionResult Details(int id=-1)
        {
            return View();
        }

        // GET: Negocios/Create
        public ActionResult CreateOrEdit(int idNegocio=-1)
        {
            var searchParam = new NegociosGetParams { IdNegocio = idNegocio, Usuario = this.pcpUserLoginName };
            var negocio = idNegocio <= 0 ? new Negocio() : BLLPrestamo.Instance.GetNegocios(searchParam).FirstOrDefault();
            var data = new NegocioVM { Negocio = negocio };
            return View(data);
        }

        // POST: Negocios/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Negocios/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Negocios/Edit/5
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

        // GET: Negocios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Negocios/Delete/5
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
