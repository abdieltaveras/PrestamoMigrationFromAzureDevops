using Newtonsoft.Json;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Controllers
{
    public class IngresosController : ControllerBasePcp
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public string BuscarPrestamos(string TextToSearch)
        {
            IEnumerable<PrestamoSearch> prestamos = null;
            if(TextToSearch.Length > 0)
            {
                prestamos = BLLPrestamo.Instance.SearchPrestamos(new PrestamosSearchParams { TextToSearch = TextToSearch, IdNegocio = pcpUserIdNegocio });
            }
            
            return JsonConvert.SerializeObject(prestamos);
        }

        public string GetPrestamo(int idprestamo)
        {
            
            var prestamo = BLLPrestamo.Instance.GetPrestamoConDetalle(idprestamo, DateTime.Now);
            
            return JsonConvert.SerializeObject(prestamo);
        }

    }
}