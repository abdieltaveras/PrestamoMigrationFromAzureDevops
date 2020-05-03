using emtSoft.DAL;
using PrestamoBLL.Entidades;
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

        public void RoleOperacionInsUpd(
            List<RoleOperacionIns> dataAInsertar,
            List<RoleOperacionIns> dataAModificar,
            List<RoleOperacionIns> dataAAnular,
            string usuario
        )
        {
            var DataTableInsertar = dataAInsertar.ToDataTable();
            var DataTableModificar = dataAModificar.ToDataTable();
            var DataTableAnular = dataAAnular.ToDataTable();

            try
            {
                var _insUpdParam = SearchRec.ToSqlParams(new {
                    RoleOperacionInsertar = DataTableInsertar,
                    RoleOperacionModificar = DataTableModificar,
                    RoleOperacionAnular = DataTableAnular,
                    Usuario = usuario
                });
                var response = PrestamosDB.ExecSelSP("spInsUpdRoleOperacion", _insUpdParam);
            }
            catch (Exception e)
            {
                return;
            }
        }

        
        public IEnumerable<RoleOperacion> RoleOperacionesSearch(BuscarRoleOperacionesParams searchParam)
        {
            return BllAcciones.GetData<RoleOperacion, BuscarRoleOperacionesParams>(searchParam, "spBuscarRoleOperaciones", GetValidation);
        }

        public IEnumerable<RoleOperacion> RoleOperacionesGet(RoleOperacionGetParams data)
        {
            var searchSqlParams = SearchRec.ToSqlParams(data);
            var operaciones = new List<RoleOperacion>();

            try
            {
                operaciones = PrestamosDB.ExecReaderSelSP<RoleOperacion>("RoleOperacionesSpGet", searchSqlParams);
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }

            return operaciones;
        }
        

    }
}
