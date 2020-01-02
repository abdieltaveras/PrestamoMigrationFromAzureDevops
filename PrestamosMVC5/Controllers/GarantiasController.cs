using Newtonsoft.Json;
using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Controllers
{

    public class GarantiasController : Controller
    {
        const int BUSCAR_A_PARTIR_DE = 2;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateOrEdit()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult CreateOrEdit(Garantia garantia)
        {

            // Convertir detalles a JSON y crear el objeto de garantia para insertar / modificar en la tabla
            string JsonDetalesGarantia = JsonConvert.SerializeObject(garantia.Detalles);
            GarantiaInsUptParams garantiaInsUpd = new GarantiaInsUptParams()
            {
                IdGarantia = garantia.IdGarantia,
                IdClasificacion = garantia.IdClasificacion,
                NoIdentificacion = garantia.NoIdentificacion,
                IdModelo = garantia.IdModelo,
                IdTipo = garantia.IdTipo,
                IdNegocio = 1,
                Detalles = JsonDetalesGarantia
            };

            BLLPrestamo.Instance.GuardarGarantia(garantiaInsUpd);

            return View("Index");
        }

        public string BuscarGarantias(string searchToText)
        {
            IEnumerable<GarantiaInsUptParams> garantias = null;
            if (searchToText.Length >= BUSCAR_A_PARTIR_DE)
            {
                garantias = BLLPrestamo.Instance.BuscarGarantia(new BuscarGarantiaParams { Search = searchToText, IdNegocio = 1 });
            }
            return JsonConvert.SerializeObject(garantias);
        }

    }
}