using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Entidades
{
    public class EstadoCivil : BaseCatalogo
    {
        public int IdEstadoCivil { get; set; } = 0;

        public override int GetId()
        {
            return IdEstadoCivil;
        }
    }

    public class EstadoCivilGetParams : BaseCatalogoGetParams
    {
        public int IdEstadoCivil { get; set; } = -1;
    }
}