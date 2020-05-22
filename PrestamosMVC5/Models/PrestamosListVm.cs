using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Models
{
    public class PrestamosListVm
    {
        [HiddenInput]
        public int idPrestamo { get; set; }
        public string PrestamoNumero { get; set; }
        public string Fecha { get; set; }
        public string NombreCliente { get; set; }
        public decimal MontoPrestado { get; set; }
    }

    public class PrestamoVm
    {
        public string MensajeError { get; internal set; }
        public Prestamo Prestamo { get; set; } = new Prestamo();

    }
}