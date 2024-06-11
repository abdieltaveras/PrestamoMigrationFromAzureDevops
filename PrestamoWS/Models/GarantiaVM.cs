using Microsoft.AspNetCore.Mvc.Rendering;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace PrestamoWS.Models
{
    public class GarantiaVM
    {
        public Garantia Garantia { get; set; }
        public SelectList ListaTipos { get; set; }

        public IEnumerable<TipoGarantia> ListaTiposReal { get; set; }
        //public SelectList ListaMarcas { get; set; }
        //Luis public IEnumerable<Marca> ListaMarcas { get; set; }
        public SelectList ListaMarcas { get; set; }
        public SelectList ListaModelos { get; set; }
        public SelectList ListaColores { get; set; }
        //public IEnumerable<ResponseMessage> ListaMensajes { get; set; }
        public string Mensaje { get; set; }

        public GarantiaVM()
        {
            Garantia = new Garantia();
        }
        public string image1PreviewValue { get; set; } = string.Empty;
        public string image2PreviewValue { get; set; } = string.Empty;
        public string image3PreviewValue { get; set; } = string.Empty;
        public string image4PreviewValue { get; set; } = string.Empty;


        //*************************Imagenes***************************//
        //[Required(ErrorMessage = "Please select file.")]
        //[Display(Name = "Browse File")]
        //public HttpPostedFileBase[] files { get; set; }

    }
}
