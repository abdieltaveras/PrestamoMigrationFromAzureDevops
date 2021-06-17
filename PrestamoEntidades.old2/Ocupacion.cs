using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Ocupacion : BaseCatalogo
    {
        public int IdOcupacion { get; set; } = 0;

        public override int GetId()
        {
            return IdOcupacion;
        }
    }

    public class OcupacionGetParams : BaseGetParams
    {
        public int IdOcupacion { get; set; } = -1;
        
    }
}
