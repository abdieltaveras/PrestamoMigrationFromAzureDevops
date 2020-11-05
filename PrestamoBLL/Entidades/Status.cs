using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace PrestamoBLL.Entidades
{
    public class Status : BaseInsUpd
    {
        public int IdStatus { get; set; }
        public int IdTipoStatus { get; set; } 
        public string Concepto { get; set; }
        public bool Estado { get; set; }
       // public ListaTipo ListaTipo { get; set; }
        //public SelectList ListaTipo { get; set; }
        public ListaTipoStatus ListaTipo { get; set; }
        public string Tipo { get; set; }
        

    }

    public class StatusGetParams : BaseGetParams
    {
        public int IdStatus { get; set; } = -1;
        public int IdTipoStatus { get; set; } = -1;
    }
    public enum ListaTipoStatus { Garantia = 1, Prestamo, GPS }

}
