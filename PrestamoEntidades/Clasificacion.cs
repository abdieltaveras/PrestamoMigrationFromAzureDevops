using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    enum TiposClasificacionesFinanciera { Consumo=1, Hipotecario, Personal, Estudio }
    public class Clasificacion : BaseCatalogo
    {
        public int IdClasificacion { get; set; } = 0;
        public bool RequiereAutorizacion { get; set; }
        public bool RequiereGarantia { get; set; }
        public override int GetId() => this.IdClasificacion;
        public int idClasificacionFinanciera { get; set; } = 1;
        public bool SaltarDomingo { get; set; }
    }
    public class ClasificacionesGetParams : BaseGetParams
    {
        public int IdClasificacion { get; set; } = -1;
        public string Nombre { get; set; } = string.Empty;
        
    }
}
