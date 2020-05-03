using PrestamoBLL.Entidades;
using PrestamosMVC5.SiteUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Models
{
    public class ClienteModel
    {
        public Cliente Cliente { get; set; }
        public InfoLaboral InfoLaboral { get; set; } = new InfoLaboral();
        public Conyuge Conyuge { get; set; } = new Conyuge();
        public Direccion Direccion { get; set; } = new Direccion();
        //public Referencia[] Referencia { get; set; } = new Referencia[5];
        public List<Referencia> Referencias { get; set; } = new List<Referencia>();
        public int? Selected { get; set; } = 2;
        public string TipoBusqueda { get; set; } = "normal";
        
        public string MensajeError { get; set; } = string.Empty;
        public int SelectedReferencia { get; set; } = 0;
        [Range(minimum: 0, maximum: 9)]
        public int SelectedVinculo { get; set; } = 0;
        public ClienteModel() {
            Cliente = new Cliente();
        }
        public ClienteModel(Cliente cliente)
        {
            this.Cliente = cliente;
            if (this.Cliente == null)
            {
                throw new Exception("No puedo aceptar un cliente nulo");
            }
        }
        [Display(Name ="Localidad Seleccionada")]
        public string InputRutaLocalidad { get; set; } = string.Empty;
        /// <summary>
        /// propiedad que guarda el contenido de la imagen
        /// </summary>
        public HttpPostedFileBase ImagenCliente1 { get; set; }
        public HttpPostedFileBase ImagenCliente2 { get; set; }
        public string image1PreviewValue { get; set; } = string.Empty;
        public string image2PreviewValue { get; set; } = string.Empty;

        

        //public IEnumerable<System.Web.HttpPostedFileBase> ImagesForCliente { get; set; }

        //public ImagesFor imgsForCliente => new ImagesFor("ImagesForCliente", "Clientes") { Qty = 3 };
    }
}