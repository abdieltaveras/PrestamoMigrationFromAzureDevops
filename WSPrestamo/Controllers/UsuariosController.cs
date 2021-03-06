using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PrestamoBLL;
using emtSoft.DAL;

namespace WSPrestamo.Controllers
{
    public class UsuariosController : BaseApiController
    {
        public IEnumerable<Usuario> Get(UsuarioGetParams searchParam)
        {
            //GetValidation(searchParam);
            return BLLPrestamo.Instance.GetUsuarios(searchParam);
        }

        public IEnumerable<UsuarioRole> GetRoles(BuscarUserRolesParams searchParam)
        {
            return BLLPrestamo.Instance.UserRolesSearch(searchParam);
            //return BllAcciones.GetData<UsuarioRole, BuscarUserRolesParams>(searchParam, "spBuscarUsuarioRoles", GetValidation);
        }

        public IEnumerable<UsuarioRole> GetRolesAll(BuscarUserRolesParams searchParam)
        {
            //return BllAcciones.GetData<UsuarioRole, BuscarUserRolesParams>(searchParam, "spBuscarTodosUsuarioRoles", GetValidation);
            return BLLPrestamo.Instance.UserRolesSearchAll(searchParam);
        }
        [HttpPost]
        public int UsuarioInsUpd(Usuario insUpdParam, string from = "")
        {
            return BLLPrestamo.Instance.InsUpdUsuario(insUpdParam, from);
            //if ((insUpdParam.LoginName.ToLower() == "admin") && (from != bllUser))
            //{
            //    throw new Exception("No puede crear el usuario administrador desde la pantalla de creacion de usuario");
            //}
            //var vigenteHasta = insUpdParam.VigenteHasta;
            //if (vigenteHasta != InitValues._19000101 ? vigenteHasta.CompareTo(DateTime.Now) <= 0 : false)
            //{
            //    throw new Exception("La fecha de vigencia de la cuenta no es valida debe ser mayor a la de hoy");
            //}
            //insUpdParam.LoginName = insUpdParam.LoginName.ToLower();
            //return BllAcciones.InsUpdData<Usuario>(insUpdParam, "spInsUpdUsuario");

        }


        [HttpPost]
        public IHttpActionResult InsUpdRoleUsuario(
            List<UsuarioRoleIns> dataAInsertar,
            List<UsuarioRoleIns> dataAAnular,
            List<UsuarioRoleIns> dataAModificar,
            string usuario)
        {
             BLLPrestamo.Instance.InsUpdRoleUsuario(dataAInsertar,dataAAnular,dataAModificar,usuario);
            return Ok();
        }

        public List<string> GetOperaciones(UsuarioOperacionesGetParams data)
        {
            return BLLPrestamo.Instance.GetOperaciones(data);
            //var searchSqlParams = SearchRec.ToSqlParams(data);
            //List<string> operaciones = new List<string>();
            //var operaciones2 = new List<CodigoOperacion>();
            //try
            //{
            //    operaciones2 = DBPrestamo.ExecReaderSelSP<CodigoOperacion>("UsuarioListaOperacionesSpGet", searchSqlParams);
            //}
            //catch (Exception e)
            //{
            //    DatabaseError(e);
            //}
            //operaciones2.ForEach(op => operaciones.Add(op.Codigo));

            //return operaciones;

        }
        private class CodigoOperacion
        {
            public string Codigo { get; set; }
        }
    }
}
