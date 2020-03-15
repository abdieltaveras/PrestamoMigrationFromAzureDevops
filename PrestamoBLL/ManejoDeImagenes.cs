using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public    class ManejoDeImagenes
    {


        public bool Save(string fileName, string tableNamecation, string imagenGuardada)
        {
            if (idApplication != 0)
            {
                string file = SiteDirectory.Files + "/" + fileName + "." + extension;
                if (imagenGuardada == file) return false;
            }
            return true;
        }
    }
}
