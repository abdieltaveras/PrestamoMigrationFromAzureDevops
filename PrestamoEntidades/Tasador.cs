using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Tasador : BaseCatalogo
    {
        public int IdTasador { get; set; } = 0;

        public override int GetId()
        {
            return IdTasador;
        }
    }

    public class TasadorGetParams : BaseCatalogoGetParams
    {
        public int IdTasador { get; set; } = -1;
    }
}
