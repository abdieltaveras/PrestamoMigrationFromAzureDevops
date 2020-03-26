using emtSoft.DAL;
using PrestamoEntidades;
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
        
        public IEnumerable<Usuario> GetUsuarios(UsuarioGetParams searchParam)
        {
            //GetValidation(searchParam);
            return BllAcciones.GetData<Usuario, UsuarioGetParams>(searchParam, "spGetUsuarios", this.GetValidation);
        }

        public int InsUpdUsuario(Usuario insUpdParam, string from = "")
        {
            //TODO Agregar columna inicioVigenciaContraseña
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

        public void InsUpdRoleUsuario(List<UsuarioRole> data)
        {
            var UserRoleDataTable = data.ToDataTable();

            try
            {
                var _insUpdParam = SearchRec.ToSqlParams(new { userrole = UserRoleDataTable });
                var response = PrestamosDB.ExecSelSP("spInsUpdUserRoles", _insUpdParam);
            }
            catch (Exception e)
            {
                return;
            }
        }

        public List<string> GetOperaciones(UsuarioOperacionesGetParams data)
        {
            //List<CodigoOperacion> operaciones = new List<CodigoOperacion>();
            //try
            //{
            //    operaciones = PrestamosDB.ExecReaderSelSP<CodigoOperacion>("UsuarioListaOperacionesSpGet", SearchRec.ToSqlParams(data));

            //}
            //catch (Exception e)
            //{
            //}


            List<string> operaciones = new List<string>();
            try
            {
                var searchSqlParams = SearchRec.ToSqlParams(data);
                //operaciones = PrestamosDB.ExecReaderSelSP<CodigoOperacion>("UsuarioListaOperacionesSpGet", searchSqlParams);
                using(
                var response = PrestamosDB.ExecReaderSelSP("UsuarioListaOperacionesSpGet", searchSqlParams))
                {
                    while (response.Read())
                    {
                        operaciones.Add(response["Codigo"].ToString());
                    }
                }
                
            }
            catch (Exception e)
            {
                
            }
            return operaciones;


        }

    }

    public class CodigoOperacion
    {
        public string Codigo { get; set; }
    }

}