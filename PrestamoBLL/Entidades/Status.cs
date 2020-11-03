using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Entidades
{
    public class Status : BaseInsUpd
    {
        public int IdStatus { get; set; }
        public int IdTipoStatus { get; set; }
        public string Tipo { get; set; }
        public string Concepto { get; set; }
        public int Estado { get; set; }
    }

    public class StatusGetParams : BaseGetParams
    {
        public int IdStatus { get; set; } = -1;
        public int IdTipo { get; set; } = -1;
    }
}
