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
        
        public IEnumerable<TasaInteres> GetTasasInteres(TasaInteresGetParams param)
        {
            var result = DALPrestamoMSSql.Instance.GetTasasInteres(param);
            
            return result;
        }
        public void insUpdTasaInteres(TasaInteres data)
        {
            DALPrestamoMSSql.Instance.insUpdTasaInteres(data);
        }
        public void DeleteTasaInteres(TasaInteresDelParams data)
        {
            DALPrestamoMSSql.Instance.DeleteTasaInteres(data);
        }
    }
}
