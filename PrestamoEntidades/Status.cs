using emtSoft.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Status : BaseInsUpd
    {
        public int IdStatus { get; set; }
        public int IdTipoStatus { get; set; } 
        public string Concepto { get; set; }
        public bool Estado { get; set; } = true;
        // public ListaTipo ListaTipo { get; set; }
        //public SelectList ListaTipo { get; set; }
        //public string Tipo { get; set; }
       // [IgnorarEnParam]


    }

    public class StatusGetParams : BaseGetParams
    {
        public int IdStatus { get; set; } = -1;
        public int IdTipoStatus { get; set; } = -1;
    }
    public enum ListaTipoStatus { Prestamo = 1,Cliente, GPS }

}
