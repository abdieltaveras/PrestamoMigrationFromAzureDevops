﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Reports.Entities.Bases;
namespace PrestamoBlazorApp.Reports.Entities.Catalogos
{
    public class Listado : BaseReporteMulti
    {
        public int Id { get; set; }
        public string Catalogo { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
    }
}
