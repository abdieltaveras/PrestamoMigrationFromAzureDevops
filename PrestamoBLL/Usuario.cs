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
            GetValidation(searchParam);
            return BllAcciones.GetData<Usuario, UsuarioGetParams>(searchParam, "spGetUsuarios");
        }

        public void InsUpdUsuario(Usuario insUpdParam, string from = "")
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
            BllAcciones.insUpdData<Usuario>(insUpdParam, "spInsUpdUsuario");
        }
        

        private bool ExistUsers => ExistDataForTable("tblUsuarios");
        
    }

}