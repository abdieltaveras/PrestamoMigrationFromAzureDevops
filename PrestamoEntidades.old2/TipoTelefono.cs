using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class TipoTelefono : BaseCatalogo
    {
        public int IdTipoTelefono { get; set; } = 0;

        public override int GetId()
        {
            return IdTipoTelefono;
        }
    }

    public class TipoTelefonoGetParams : BaseCatalogoGetParams
    {
        public int IdTipoTelefono { get; set; } = -1;
    }
}
