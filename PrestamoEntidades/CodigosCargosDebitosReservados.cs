﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class CodigosCargosDebitosReservados:BaseInsUpd
    {
        public int IdCodigoCargo { get; set; } = 0;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

    }
    public class CodigosCargosGetParams
    {
        public int IdCodigoCargo { get; set; }
    }
}