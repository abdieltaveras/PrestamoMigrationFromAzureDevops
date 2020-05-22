using PrestamoBLL;
using PrestamoBLL.Entidades;
using PrestamosMVC5.Models;
using PrestamosMVC5.SiteUtils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Controllers
{
    [AuthorizeUser]
    public class PrestamosController : ControllerBasePcp
    {
        public PrestamosController()
        {

        }
        // GET: Prestamos
        public ActionResult Index()
        {
            
            UpdViewBag_LoadCssAndJsForDatatable(true);
            var pres = BLLPrestamo.Instance.GetPrestamos(new PrestamosGetParams { idPrestamo=-1} ); 
            var prestamosList = pres.Select(pr => new PrestamosListVm { Fecha = pr.FechaEmisionReal.ToShortDateString(), idPrestamo = pr.IdPrestamo, PrestamoNumero = pr.PrestamoNumero, MontoPrestado = pr.MontoPrestado, NombreCliente = BLLPrestamo.Instance.ClientesGet(new ClienteGetParams { IdCliente = pr.IdCliente, IdNegocio = this.pcpUserIdNegocio }).FirstOrDefault().NombreCompleto });
            return View(prestamosList);
        }

        public ActionResult CreateOrEdit(int id = -1, string mensaje = "")
        {
            var cl = new Cliente() { Activo = false };
            var model = new PrestamoVm();
            model.MensajeError = mensaje;
            if (id != -1)
            {
                // Buscar el cliente
                var searchResult = getPrestamo(id);
                if (searchResult.DatosEncontrados)
                {
                    var data = searchResult.DataList.FirstOrDefault();
                    model = new PrestamoVm();
                    model.Prestamo = data;
                    TempData["Prestamo"] = data;
                }
                else
                {
                    model.MensajeError = "Lo siento no encontramos datos para su peticion";
                }
            }
            pcpSetUsuarioAndIdNegocioTo(model.Prestamo);
            //model.Referencias = new List<Referencia>(new Referencia[4]);
            //model.Referencias[0] = new Referencia() { Tipo = 2 };
            //model.Referencias.Add(new Referencia() { NombreCompleto = "hola" });
            //model.Referencias.Add(new Referencia() { NombreCompleto = "adios" });
            // model.Referencias.Add(new Referencia() {});
            return View(model);
        }

        private SeachResult<Prestamo> getPrestamo(int id)
        {
            var searchData = new PrestamosGetParams { idPrestamo = id };
            pcpSetIdNegocioTo(searchData);
            var prestamo = BLLPrestamo.Instance.GetPrestamos(searchData);
            var result = new SeachResult<Prestamo>(prestamo);
            return result;
        }
        
    }
}