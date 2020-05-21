using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Models
{
    public class PrestamoListVM
    {
        [HiddenInput]
        public int idPrestamo { get; set; }
        public string PrestamoNumero { get; set; }
        public string Fecha { get; set; }
        public string NombreCliente { get; set; }
        public decimal MontoPrestado { get; set; }
    }
}