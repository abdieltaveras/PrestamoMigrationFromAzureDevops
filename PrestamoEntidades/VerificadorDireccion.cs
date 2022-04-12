using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class VerificadorDireccion : BaseInsUpdCatalogo
    {
        public int IdVerificadorDireccion { get; set; } = 0;

        public override int GetId()
        {
            return IdVerificadorDireccion;
        }
    }

    public class VerificadorDireccionGetParams : BaseCatalogoGetParams
    {
        public int IdVerificadorDireccion { get; set; } = -1;
    }
}
