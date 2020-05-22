using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Entidades
{
    public class Clasificacion : BaseCatalogo
    {
        public int IdClasificacion { get; set; } = 0;

        public override int GetId() => this.IdClasificacion;

        public string ClasificacionFinanciera { get; set; } = string.Empty;

    }
    public class ClasificacionesGetParams : BaseGetParams
    {
        public int IdColor { get; set; } = -1;
    }
}
