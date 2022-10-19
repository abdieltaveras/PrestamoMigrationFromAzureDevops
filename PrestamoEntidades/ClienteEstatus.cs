using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class ClienteEstatus : BaseInsUpd
    {
        public int IdClienteEstatus { get; set; }
        public int IdEstatus { get; set; }
        public int IdCliente { get; set; }
        public string Comentario { get; set; }
    }
    public class ClienteEstatusGet
    {
        public int IdEstatus { get; set; }
        public int IdCliente { get; set; }
        public int Activo { get; set; }
        public string EstatusName { get; set; }
        public string EstatusDescription { get; set; }
    }
    public class ClienteEstatusGetParams
    {
        public int IdEstatus { get; set; } = -1;
        public int IdCliente { get; set; } = -1;
    }
}
