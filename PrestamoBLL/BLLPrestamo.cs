using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public partial class BLLPrestamo
    {
        #region StaticBLL
        static private BLLPrestamo _bll = null;
        static public BLLPrestamo Instance
        {
            get
            {
                if (_bll == null)
                {
                    _bll = new BLLPrestamo();
                }
                return _bll;
            }
        }
        #endregion StaticBLL
        private void DatabaseError(Exception e)
        {
            var mensaje = e.Message;
            if (e.Message == "Object reference not set to an instance of an object.")
                mensaje = $"La cadena de conexion [{ConexionDB.Server}] indicada no permitio establecer la conexion";
            throw new Exception(mensaje);
        }
        //new Exception("Lo siento ha ocurrido un error a nivel de la base de datos");

    }
}
