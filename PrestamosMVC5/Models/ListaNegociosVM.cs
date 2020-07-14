using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrestamosMVC5.Models
{
    public class ListaLocalidadNegocioVM
    {
        public int idLocalidadNegocioSelected { get; set; }
        public string NombreNegocio { get; set; }

        public SelectList SelectLocalidadNegocio { get; set; }

        public string UsuarioNombreReal { get; set; }
    }
}