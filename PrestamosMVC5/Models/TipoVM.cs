using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.Models
{
    public class TipoVM
    {
        public TipoGarantia Tipo { get; set; }
        public IEnumerable<TipoGarantia> ListaTipos { get; set; }
    }
}