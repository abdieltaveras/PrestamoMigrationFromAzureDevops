using emtSoft.DAL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public partial class BLLPrestamo
    {
        public IEnumerable<Role> RolesGet(RoleGetParams searchParam)
        {
            return BllAcciones.GetData<Role, RoleGetParams>(searchParam, "spGetRoles", GetValidation);
        }

        public int RoleInsUpd(Role insUpdParam)
        {
            return BllAcciones.InsUpdData<Role>(insUpdParam, "spInsUpdRole");
        }

        public void RoleOperacionInsUpd(RoleOperacionInsUpdParams insUpdParam)
        {
            try
            {
                var _insUpdParam = SearchRec.ToSqlParams(insUpdParam);
                var response = PrestamosDB.ExecSelSP("spInsUpdRoleOperacion", _insUpdParam);
            }
            catch (Exception e)
            {
                
            }
        }

        public IEnumerable<UsuarioRole> UserRolesSearch(BuscarUserRolesParams searchParam)
        {
            return BllAcciones.GetData<UsuarioRole, BuscarUserRolesParams>(searchParam, "spBuscarUsuarioRoles", GetValidation);
        }
        public IEnumerable<RoleOperacion> RoleOperacionesSearch(BuscarRoleOperacionesParams searchParam)
        {
            return BllAcciones.GetData<RoleOperacion, BuscarRoleOperacionesParams>(searchParam, "spBuscarRoleOperaciones", GetValidation);
        }
        

    }
}
