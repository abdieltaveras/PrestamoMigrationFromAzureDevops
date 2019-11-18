using PrestamoEntidades;
using System.Collections.Generic;
using System.Linq;

namespace PrestamoDAL
{
    public partial class DALPrestamoInMemory
    {
        List<TasaInteres> intereses = new List<TasaInteres>{
                new TasaInteres{ idTasaInteres = 1, InteresMensual = 10, Activo = false, RequiereAutorizacion = false},
                new TasaInteres{ idTasaInteres = 2, InteresMensual = 20, Activo = true, RequiereAutorizacion = true},
                new TasaInteres{ idTasaInteres = 3, InteresMensual = 30, Activo = true, RequiereAutorizacion = true},
            };
        public void insUpdTasaInteres(TasaInteres data)
        {
            data.idTasaInteres = intereses.Count() + 1;
            intereses.Add(data);
        }
        public IEnumerable<TasaInteres> GetTasasInteres(TasaInteresGetParams Searchdata)
        {
            var result = intereses.Where(data => data.InteresMensual == Searchdata.InteresMensual);
            return result;
        }
    }
}
