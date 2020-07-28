using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.Models
{
    public class CatalogoVM : BaseInsUpd
    {
        public int Id { get; set; } = 0;
        public string NombreTabla { get; set; }
        public string IdTabla { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string TipoCatalogo { get; set; }

        public IEnumerable<BaseCatalogo> Lista { get; set; } = new List<BaseCatalogo>();
        public BaseCatalogo Data { get; set; }

    }
}