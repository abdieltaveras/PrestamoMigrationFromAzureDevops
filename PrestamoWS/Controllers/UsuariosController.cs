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
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsuariosController : ControllerBasePrestamoWS
    {
        [HttpGet]
        public IEnumerable<Users> Get([FromQuery] UsuarioGetParams getParams)
        {
            //GetValidation(searchParam);
            return BLLPrestamo.Instance.GetUsuarios(getParams);
        }
        [HttpGet]
        public IEnumerable<UsuarioRole> GetRoles([FromQuery] BuscarUserRolesParams getParams)
        {
            return BLLPrestamo.Instance.UserRolesSearch(getParams);
            //return BllAcciones.GetData<UsuarioRole, BuscarUserRolesParams>(searchParam, "spBuscarUsuarioRoles", GetValidation);
        }
        [HttpGet]
        public IEnumerable<UsuarioRole> GetRolesAll([FromQuery] BuscarUserRolesParams getParams)
        {
            return BLLPrestamo.Instance.UserRolesSearchAll(getParams);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Users usuario, string from = "")
        {
            try
            {
                BLLPrestamo.Instance.InsUpdUsuario(usuario, from);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem("Error al guardar " + e.Message);
            }

        }


        [HttpPost]
        public IActionResult PostRoles([FromBody] UpdateRoles roles)
            
        {
            BLLPrestamo.Instance.InsUpdRoleUsuario(roles.dataAInsertar, roles.dataAAnular, roles.dataAModificar, roles.usuario);
            return Ok();
        }
        [HttpGet]
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

    public class UpdateRoles
    {
        public IEnumerable<UsuarioRoleIns> dataAInsertar { get; set; }
        public IEnumerable<UsuarioRoleIns> dataAAnular { get; set; }
        public IEnumerable<UsuarioRoleIns> dataAModificar { get; set; }
        public string usuario
        {
            get; set;

        }
    }
}
