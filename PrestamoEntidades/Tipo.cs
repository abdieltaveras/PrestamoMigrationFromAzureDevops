using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Tipo : BaseInsUpd
    {
        public int IdTipo { get; set; } = 0;
        public int IdClasificacion { get; set; } = 1;
        public string Nombre { get; set; } = string.Empty;
    }
    public class TipoGetParams
    {
        public int IdTipo { get; set; } = -1;
        public int IdClasificacion { get; set; } = -1;
        public int IdNegocio { get; set; } = -1;
    }
    public class TipoInsUpdParams : Tipo
    {
        public string InsertadoPor { get; set; } = string.Empty;
    }
}
