using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Entidades
{
    enum TiposClasificacionesFinanciera { Consumo=1, Hipotecario, Personal, Estudio }
    public class Clasificacion : BaseCatalogo
    {
        public int IdClasificacion { get; set; } = 0;
        public bool RequiereAutorizacion { get; set; }
        public bool RequiereGarantia { get; set; }
        public override int GetId() => this.IdClasificacion;
        public int idClasificacionFinanciera { get; set; } = 1;
    }
    public class ClasificacionesGetParams : BaseGetParams
    {
        public int IdColor { get; set; } = -1;
    }
}
