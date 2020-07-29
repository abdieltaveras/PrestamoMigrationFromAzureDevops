using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Entidades
{
    public class Operacion
    {
        public static string[] Grupos = {
            "Tasa interes", // 0
            "Moras", // 1
            "Clientes",
            "Garantias",
            "Catalogos comunes",
            "Reportes",
            "Otros",
        };

        public int IdOperacion { get; set; } = 0;
        public string Codigo { get; set; } = string.Empty;
        public int Grupo { get; set; } = 0;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }

    public class OperacionGetParams : BaseGetParams
    {
        public int IdOperacion { get; set; } = -1;
    }
}
