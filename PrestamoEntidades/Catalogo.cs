﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Catalogo : BaseCatalogo
    {
        public int Id { get; set; } = 0;
        public string IdTabla { get; set; } = "";
        public string NombreTabla { get; set; }

        public override int GetId()
        {
            throw new NotImplementedException();
        }
    }

    public class ToggleStatusCatalogo : BaseAnularParams
    {
        public string IdTabla { get; set; } = "";
        public string NombreTabla { get; set; }
        public bool Activo { get; set; }
    }

    public class DelCatalogo : BaseAnularParams
    {
        public string IdTabla { get; set; } = "";
        public string NombreTabla { get; set; }
    }
}
