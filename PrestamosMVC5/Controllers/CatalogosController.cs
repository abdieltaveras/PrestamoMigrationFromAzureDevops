using PrestamoBLL;
using PrestamoEntidades;
using PrestamosMVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Controllers
{
    public class CatalogosController : ControllerBasePcp
    {
        // GET: Catalogos
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateOrEdit(string tipoCatalogo)
        {
            //Hacer peticion al catalogo elegido

            CatalogoVM data = new CatalogoVM();

            data.Lista = BLLPrestamo.Instance.CatalogosGet(new BaseCatalogoGetParams { IdNegocio = pcpUserIdNegocio }, tipoCatalogo);
            data.TipoCatalogo = tipoCatalogo;


            //switch (tipoCatalogo)
            //{
            //    case "ocupacion":
            //        OcupacionVM ocupacion = new OcupacionVM();
            //        ocupacion.Lista = BLLPrestamo.Instance.CatalogosGet(new OcupacionGetParams { IdNegocio = pcpUserIdNegocio });
            //        data.TipoCatalogo = "Ocupacion";
            //        data.Lista = ocupacion;
            //        break;
            //    case "verificadordireccion":
            //        VerificadorDireccionVM verificadorDireccion = new VerificadorDireccionVM();
            //        verificadorDireccion.Lista = BLLPrestamo.Instance.VerificardorDireccionGet(new VerificadorDireccionGetParams { IdNegocio = pcpUserIdNegocio });
            //        data.TipoCatalogo = "Verificadores de direccion";
            //        data.Data = verificadorDireccion;
            //        break;
            //    default:
            //        break;
            //}

            return View("Catalogos", data);
        }


        [HttpPost]
        public RedirectToRouteResult CreateOrEdit(Marca marca)
        {
            this.pcpSetUsuarioAndIdNegocioTo(marca);
            BLLPrestamo.Instance.MarcaInsUpd(marca);
            return RedirectToAction("CreateOrEdit");
        }
    }
}