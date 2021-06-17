using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.Models
{
    public class NegocioVM
    {
        public Negocio Negocio { get; set; }

        public string image1PreviewValue { get; set; } = string.Empty;

        public string ImagenNegocio1 { get; set; } = string.Empty;
    }
}