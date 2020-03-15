using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public class Role : BaseCatalogo
    {
        public int IdRole { get; set; } = 0;
        //public string Codigo { get; set; } = string.Empty;
        //public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        public override int GetId()
        {
            throw new NotImplementedException();
        }

        //public IEnumerable<Operacion> Permisos { get; set; }
    }

    public class RoleGetParams : BaseGetParams
    {
        public int IdRole { get; set; } = -1;
    }

    public class RoleOperacionInsUpdParams
    {
        public int IdRole { get; set; } = -1;
        public string Values { get; set; } = string.Empty;
    }
}
