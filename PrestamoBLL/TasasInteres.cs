using PrestamoDAL;
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
        
        public IEnumerable<TasaInteres> GetTasaInteres(TasaInteresGetParams param)
        {
            var result = DALPrestamoInMemory.Instance.GetTasaInteres(param);
            return result;
        }
    }
}
