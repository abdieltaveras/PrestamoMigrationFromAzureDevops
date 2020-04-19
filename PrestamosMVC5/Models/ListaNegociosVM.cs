using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Models
{
    public class ListaNegocioVM
    {
        public int idNegocioSelected { get; set; }
        public string NombreEmpresaMatriz { get; set; }

        public SelectList SelectNegocios { get; set; }

        public string returnUrl { get; set; }
    }
}