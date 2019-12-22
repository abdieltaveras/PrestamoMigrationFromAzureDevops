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
        public IEnumerable<Usuario> GetUsuarios(UsuarioGetParams searchParam)
        {
            GetValidation(searchParam);
            return BllAcciones.GetData<Usuario, UsuarioGetParams>(searchParam, "spGetUsuarios");
        }

        public void InsUpdUsuario(Usuario insUpdParam)
        {
            BllAcciones.insUpdData<Usuario>(insUpdParam, "spInsUpdUsuario");
        }
    }
}