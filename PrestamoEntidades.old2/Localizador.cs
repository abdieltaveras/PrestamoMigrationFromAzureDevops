using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Localizador : BaseCatalogo
    {
        public int IdLocalizador { get; set; } = 0;

        public override int GetId()
        {
            return IdLocalizador;
        }
    }

    public class LocalizadorGetParams : BaseCatalogoGetParams
    {
        public int IdLocalizador { get; set; } = -1;
    }
}
