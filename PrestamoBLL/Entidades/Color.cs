using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Entidades
{
    public class Color : BaseCatalogo
    {
        public int IdColor { get; set; } = 0;

        public override int GetId()
        {
            throw new NotImplementedException();
        }
    }
    public class ColorGetParams : BaseGetParams
    {
        public int IdColor { get; set; } = -1;
    }
}
