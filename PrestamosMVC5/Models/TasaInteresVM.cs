using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.Models
{
    public class TasaInteresVM
    {
        public TasaInteres TasaInteres { get; set; } = new TasaInteres();
        public IEnumerable<TasaInteres> ListaTasaInteres { get; set; }

    }
}