using Newtonsoft.Json;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using PrestamosMVC5.SiteUtils;
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

        public string BuscarPrestamos(string TextToSearch, int SearchType, bool CargarImagenesClientes)
        {
            IEnumerable<PrestamoSearch> prestamos = null;
            //if(TextToSearch.Length > 0)
            //{
            //    prestamos = BLLPrestamo.Instance.SearchPrestamos(new PrestamosSearchParams { TextToSearch = TextToSearch, IdNegocio = pcpUserIdNegocio });
            //}

            
            prestamos = BLLPrestamo.Instance.SearchPrestamos(new PrestamosSearchParams { TextToSearch = TextToSearch, IdNegocio = pcpUserIdNegocio, SearchType = SearchType });
            
            if (CargarImagenesClientes)
            {
                foreach (var prestamo in prestamos)
                {
                    prestamo.FotoCliente = Url.Content(SiteDirectory.ImagesForClientes + "/" + prestamo.FotoCliente);
                }
            }
            
            return JsonConvert.SerializeObject(prestamos);
        }

        public string GetPrestamo(int idprestamo)
        {
            
            var prestamo = BLLPrestamo.Instance.GetPrestamoConDetalle(idprestamo, DateTime.Now);
            
            return JsonConvert.SerializeObject(prestamo);
        }

        public void MakePay(string idprestamo, string fechaPrestamo, string montoPrestamo)
        {

            //var prestamo = BLLPrestamo.Instance.GetPrestamoConDetalle(idprestamo, DateTime.Now);
            var asd = 4;
            //return JsonConvert.SerializeObject(prestamo);
        }

    }
}