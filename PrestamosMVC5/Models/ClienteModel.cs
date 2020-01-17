using PrestamoEntidades;
using System;
using System.Collections.Generic;
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
        
        public string TipoBusqueda { get; set; } = "normal";
        public string MensajeError { get; set; } = string.Empty;
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

        public string NombreLocalidad { get; set; } = string.Empty;
    }
}