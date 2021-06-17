using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.Models
{
    public class OcupacionVM
    {
        public Ocupacion Model { get; set; }
        public IEnumerable<BaseCatalogo> Lista { get; set; }
    }
}