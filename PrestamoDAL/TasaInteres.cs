using PrestamoEntidades;
using System.Collections.Generic;
using System.Linq;

namespace PrestamoDAL
{
    public partial class DALPrestamoInMemory
    {
        List<TasaInteres> intereses = new List<TasaInteres>{
                new TasaInteres{ IdTasaInteres = 1, InteresMensual = 10, Activo = false, RequiereAutorizacion = false},
                new TasaInteres{ IdTasaInteres = 1, InteresMensual = 20, Activo = true, RequiereAutorizacion = true},
                new TasaInteres{ IdTasaInteres = 1, InteresMensual = 30, Activo = true, RequiereAutorizacion = true},
            };
        public IEnumerable<TasaInteres> GetTasaInteres(TasaInteresGetParams param)
        {
            var result = intereses.Where(data => data.InteresMensual <= param.InteresMensualMenorOIgualA);

            return result;
        }
    }
}
