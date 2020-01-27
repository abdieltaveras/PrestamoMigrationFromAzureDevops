using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Color : BaseInsUpd
    {
        public int IdColor { get; set; } = 0;
        public string Nombre { get; set; } = string.Empty;
    }
    public class ColorGetParams
    {
        public int IdColor { get; set; } = -1;
        public int IdNegocio { get; set; } = -1;
    }
}
