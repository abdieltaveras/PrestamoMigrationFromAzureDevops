using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class TipoSexo : BaseInsUpdCatalogo
    {
        public int IdTipoSexo { get; set; } = 0;

        public override int GetId()
        {
            return IdTipoSexo;
        }
    }

    public class TipoSexoGetParams : BaseCatalogoGetParams
    {
        public int IdTipoSexo { get; set; } = -1;
    }
}
