using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.Models
{
    public class LocalidadVM
    {
        public Localidad Localidad { get; set; }
        public IEnumerable<Localidad> ListaLocalidades { get; set; }

    }
}