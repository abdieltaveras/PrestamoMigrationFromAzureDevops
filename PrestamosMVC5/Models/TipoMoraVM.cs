using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.Models
{
    public class TipoMoraVM
    {
        public TipoMora TipoMora { get; set; } = new TipoMora();
        public IEnumerable<TipoMora> ListaTipoMoras { get; set; }
    }
}