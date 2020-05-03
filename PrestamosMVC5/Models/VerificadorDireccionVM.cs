using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.Models
{
    public class VerificadorDireccionVM
    {
        public VerificadorDireccion Model { get; set; }
        public IEnumerable<VerificadorDireccion> Lista { get; set; }
    }
}