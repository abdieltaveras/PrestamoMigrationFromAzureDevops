using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Comentario : BaseInsUpd
    {
        public int IdComentario { get; set; }
        public int IdTransaccion { get; set; }
        public string TablaOrigen { get; set; }
        public string Detalle { get; set; }
    }

    public class ComentarioGetParams
    {
        public int IdComentario { get; set; }
        public int IdTransaccion { get; set; }
        public string TablaOrigen { get; set; }
    }
}
