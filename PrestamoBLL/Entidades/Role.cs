using emtSoft.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PrestamoBLL.Entidades
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

    public class RoleOperacionGetParams
    {
        public int IdRole { get; set; } = -1;
    }

    public class RoleOperacionInsUpdParams
    {
        public int IdRole { get; set; } = -1;
        public string Values { get; set; } = string.Empty;
    }

    public class RoleOperacion
    {
        public int IdRole { get; set; }
        public int IdOperacion { get; set; }
        [IgnorarEnParam()]
        [HiddenInput(DisplayValue = false)]
        public string InsertadoPor { get; set; } = string.Empty;
        [IgnorarEnParam()]
        [HiddenInput(DisplayValue = false)]
        public DateTime FechaInsertado { get; set; } = InitValues._19000101;
        [IgnorarEnParam()]
        [HiddenInput(DisplayValue = false)]
        public string ModificadoPor { get; set; } = string.Empty;
        [IgnorarEnParam()]
        [HiddenInput(DisplayValue = false)]
        public DateTime FechaModificado { get; set; } = InitValues._19000101;
        [IgnorarEnParam()]
        [HiddenInput(DisplayValue = false)]
        public string AnuladoPor { get; set; } = string.Empty;
        [IgnorarEnParam()]
        [HiddenInput(DisplayValue = false)]
        public DateTime FechaAnulado { get; set; } = InitValues._19000101;
        public bool Anulado() => string.IsNullOrEmpty(AnuladoPor);

    }

    public class RoleOperacionIns
    {
        public int IdRole { get; set; }
        public int IdOperacion { get; set; }        
    }

    public class BuscarUserRolesParams : BaseGetParams
    {
        public int IdUsuario { get; set; }
    }
    public class BuscarRoleOperacionesParams : BaseGetParams
    {
        public int IdRole { get; set; }
    }
}
