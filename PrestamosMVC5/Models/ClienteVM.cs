using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Models
{
    public class ClienteVM
    {
        public Cliente Cliente { get; set; }
        public InfoLaboral InfoLaboral { get; set; } = new InfoLaboral();
        public Conyuge Conyuge { get; set; } = new Conyuge();
        public Direccion Direccion { get; set; } = new Direccion();
        
        public string TipoBusqueda { get; set; } = "normal";
        public string MensajeError { get; set; } = string.Empty;
        public ClienteVM() { }
        public ClienteVM(Cliente cliente)
        {
            this.Cliente = cliente;
            if (this.Cliente == null)
            {
                throw new Exception("No puedo aceptar un cliente nulo");
            }
            if (cliente.IdCliente != 0)
            {
                fillOtherInfo(cliente.IdCliente);
            };
        }

        private void fillOtherInfo(int idCliente)
        {

        }
    }
}