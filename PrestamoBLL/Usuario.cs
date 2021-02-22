using emtSoft.DAL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{

    public partial class BLLPrestamo
    {
        
        public IEnumerable<Usuario> UsuariosGet(UsuarioGetParams searchParam)
        {
            //GetValidation(searchParam);
            return BllAcciones.GetData<Usuario, UsuarioGetParams>(searchParam, "spGetUsuarios", GetValidation);
        }

        public IEnumerable<UsuarioRole> UserRolesSearch(BuscarUserRolesParams searchParam)
        {
            return BllAcciones.GetData<UsuarioRole, BuscarUserRolesParams>(searchParam, "spBuscarUsuarioRoles", GetValidation);
        }

        public IEnumerable<UsuarioRole> UserRolesSearchAll(BuscarUserRolesParams searchParam)
        {
            return BllAcciones.GetData<UsuarioRole, BuscarUserRolesParams>(searchParam, "spBuscarTodosUsuarioRoles", GetValidation);
        }

        public int UsuarioInsUpd(Usuario insUpdParam, string from = "")
        {
            
            if ((insUpdParam.LoginName.ToLower() == "admin") && (from != bllUser))
            {
                throw new Exception("No puede crear el usuario administrador desde la pantalla de creacion de usuario");
            }
            var vigenteHasta = insUpdParam.VigenteHasta;
            if (vigenteHasta != InitValues._19000101 ? vigenteHasta.CompareTo(DateTime.Now) <= 0 : false)
            {
                throw new Exception("La fecha de vigencia de la cuenta no es valida debe ser mayor a la de hoy");
            }
            insUpdParam.LoginName = insUpdParam.LoginName.ToLower();
            return BllAcciones.InsUpdData<Usuario>(insUpdParam, "spInsUpdUsuario");
        }
        

        private bool ExistUsers => ExistDataForTable("tblUsuarios");

        public void InsUpdRoleUsuario(
            List<UsuarioRoleIns> dataAInsertar,
            List<UsuarioRoleIns> dataAAnular,
            List<UsuarioRoleIns> dataAModificar,
            string usuario)
        {

            var DataTableInsertar = dataAInsertar.ToDataTable();
            var DataTableModificar = dataAModificar.ToDataTable();
            var DataTableAnular = dataAAnular.ToDataTable();

            try
            {
                var _insUpdParam = SearchRec.ToSqlParams(new
                {
                    UsuarioRoleInsertar = DataTableInsertar,
                    UsuarioRoleModificar = DataTableModificar,
                    UsuarioRoleAnular = DataTableAnular,
                    Usuario = usuario
                });
                var response = DBPrestamo.ExecSelSP("spInsUpdUserRoles", _insUpdParam);
            }
            catch (Exception e)
            {
                return;
            }
        }

        public List<string> GetOperaciones(UsuarioOperacionesGetParams data)
        {
            var searchSqlParams = SearchRec.ToSqlParams(data);
            List<string> operaciones = new List<string>();
            var operaciones2 = new List<CodigoOperacion>();
            try
            {
                operaciones2 = DBPrestamo.ExecReaderSelSP<CodigoOperacion>("UsuarioListaOperacionesSpGet", searchSqlParams);
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            operaciones2.ForEach(op => operaciones.Add(op.Codigo));
            
            return operaciones;
        }
        private class CodigoOperacion
        {
            public string Codigo { get; set; }
        }

    }

}