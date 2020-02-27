using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.Models
{
    public class CatalogoVM 
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string TipoCatalogo { get; set; }
        public IEnumerable<BaseCatalogo> Lista { get; set; } = new List<BaseCatalogo>();
        public BaseCatalogo Data { get; set; }

    }
}