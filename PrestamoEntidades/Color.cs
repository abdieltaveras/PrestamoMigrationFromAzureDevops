using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Color : BaseCatalogo
    {
        public virtual int IdColor { get; set; } = 0;

        public override int GetId() => this.IdColor;
    }
    public class ColorGetParams : BaseGetParams 
    {
        public int IdColor { get; set; } = -1;

    }
}
