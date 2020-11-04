using Newtonsoft.Json;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using PrestamosMVC5.Exceptions;
using PrestamosMVC5.Models;
using PrestamosMVC5.SiteUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PrestamosMVC5.Models.StatusVM;

namespace PrestamosMVC5.Controllers
{
    public class StatusController : ControllerBasePcp
    {
        // GET: Status
        public StatusController()
        {
            UpdViewBag_LoadCssAndJsForDatatable(true);
        }
        public ActionResult Index()
        {
            UpdViewBag_LoadCssAndJsForDatatable(true);
            var statuslist = GetStatuses();
            ActionResult _actResult = View(statuslist);
            return _actResult;
        } 

        // GET: Status/Create
        public ActionResult CreateOrEdit(int id = -1, List<ResponseMessage> responses = null,StatusVM status = null)
        {
            
            StatusVM datos = status == null ? new StatusVM() : status;
            datos.Status = new Status();
            datos.Status.ListaTipo = GetListaTipoStatus();
            
            if (id != -1)
            {
                var datosStatus = GetStatus(id).DataList.FirstOrDefault();
                datos.Status = datosStatus;
                datos.Status.Tipo = 1;
            }
            return View(datos);
        }

        // POST: Status/Create
        [HttpPost]
        public ActionResult CreateOrEdit(Status status)
        {
            List<ResponseMessage> listaMensajes = new List<ResponseMessage>();
            try
            {
                status.Usuario = "Luis";
                BLLPrestamo.Instance.InsUpdStatus(status);
            }
            catch(Exception e)
            {
                string err = e.Message + e.StackTrace;
            }
            return RedirectToAction("Index", new { listaMensajes });
        }

        // GET: Status/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Status/Edit/5
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

        // GET: Status/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Status/Delete/5
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

        public static SelectList GetListaTipoStatus()
        {
            var enumValues = Enum.GetValues(typeof(ListaTipo)).Cast<ListaTipo>().Select(e => new { Value = Convert.ToInt32(e), Text = e.ToString() }).ToList();
            return new SelectList(enumValues,"Value", "Text", "");

        }

        public SeachResult<Status> GetStatus(int id)
        {
            //
            // TODO: Add test logic here
            //
            string error = "";
            try
            {
                var parametros = new StatusGetParams
                {
                    IdStatus = id
                };
                BLLPrestamo.Instance.GetStatus(parametros);
                pcpSetUsuarioAndIdNegocioTo(parametros);
                var statuses = BLLPrestamo.Instance.GetStatus(parametros);
                var result = new SeachResult<Status>(statuses);
                return result;
            }
            catch (Exception e)
            {
                error = e.Message + e.StackTrace;
                throw;
            }
        }
        public IEnumerable<Status> GetStatuses()
        {
            //
            // TODO: Add test logic here
            //
            string error = "";
            try
            {
                var parametros = new StatusGetParams
                {
                };
              return  BLLPrestamo.Instance.GetStatus(parametros);
             
            }
            catch (Exception e)
            {
                error = e.Message + e.StackTrace;
                throw;
            }
        }
    }
}
