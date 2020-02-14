using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Models
{
    public class GarantiaVM
    {
        public Garantia Garantia { get; set; }
        public SelectList ListaTipos { get; set; }
        public IEnumerable<TipoGarantia> ListaTiposReal { get; set; }
        //public IEnumerable<Marca> ListaMarcasReal { get; set; }
        
        public SelectList ListaMarcas { get; set; }
        public SelectList ListaModelos { get; set; }
        public SelectList ListaColores { get; set; }
        public IEnumerable<ResponseMessage> ListaMensajes { get; set; }
        public string Mensaje { get; set; }

    }
}