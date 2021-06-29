using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

using PrestamoBLL;
using Microsoft.AspNetCore.Mvc;

namespace PrestamoWS.Controllers
{
    public class UsuariosController : ControllerBasePrestamoWS
    {
        public IEnumerable<Usuario> Get([FromQuery] UsuarioGetParams getParams)
        {
            //GetValidation(searchParam);
            return BLLPrestamo.Instance.GetUsuarios(getParams);
        }

        public IEnumerable<UsuarioRole> GetRoles([FromQuery] BuscarUserRolesParams getParams)
        {
            return BLLPrestamo.Instance.UserRolesSearch(getParams);
            //return BllAcciones.GetData<UsuarioRole, BuscarUserRolesParams>(searchParam, "spBuscarUsuarioRoles", GetValidation);
        }

        public IEnumerable<UsuarioRole> GetRolesAll([FromQuery] BuscarUserRolesParams getParams)
        {
            return BLLPrestamo.Instance.UserRolesSearchAll(getParams );
        }
        [HttpPost]
        public int UsuarioInsUpd([FromBody] Usuario usuario, string from = "")
        {
            return BLLPrestamo.Instance.InsUpdUsuario(usuario, from);
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
        public IActionResult InsUpdRoleUsuario(
            List<UsuarioRoleIns> dataAInsertar,
            List<UsuarioRoleIns> dataAAnular,
            List<UsuarioRoleIns> dataAModificar,
            string usuario)
        {
             BLLPrestamo.Instance.InsUpdRoleUsuario(dataAInsertar,dataAAnular,dataAModificar,usuario);
            return Ok();
        }

        public List<string> GetOperaciones([FromQuery] UsuarioOperacionesGetParams getParams)
        {
            return BLLPrestamo.Instance.GetOperaciones(getParams);
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
