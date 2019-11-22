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
        private void DatabaseError(Exception e) => throw e;
        //new Exception("Lo siento ha ocurrido un error a nivel de la base de datos");

    }
}
