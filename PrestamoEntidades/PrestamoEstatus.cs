using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class PrestamoEstatus : BaseInsUpd
    {
        public int IdPrestamoEstatus { get; set; }
        public int IdEstatus { get; set; }
        public int IdPrestamo { get; set; }
        public string Comentario { get; set; }
    }
    public class PrestamoEstatusGet
    {
        public int IdEstatus { get; set; }
        public int IdPrestamo { get; set; }
        public int Activo { get; set; }
        public string EstatusName { get; set; }
        public string EstatusDescription { get; set; }
    }
    public class PrestamoEstatusGetParams
    {
        public int IdEstatus { get; set; } = -1;
        public int IdPrestamo { get; set; } = -1;
    }
}
