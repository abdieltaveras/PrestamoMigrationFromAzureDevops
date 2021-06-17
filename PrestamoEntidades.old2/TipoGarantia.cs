using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class TipoGarantia : BaseCatalogo
    {
        public int IdTipoGarantia { get; set; } = 0;
        public int IdClasificacion { get; set; } = 1;

        public override int GetId()
        {
            throw new NotImplementedException();
        }
        //public string Nombre { get; set; } = string.Empty;
    }
    public class TipoGetParams : BaseGetParams
    {
        public int IdTipoGarantia { get; set; } = -1;
        public int IdClasificacion { get; set; } = -1;
    }
    public class TipoInsUpdParams : TipoGarantia
    {
    }
}
