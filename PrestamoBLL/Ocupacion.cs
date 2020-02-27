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
        public IEnumerable<Ocupacion> OcupacionesGet(OcupacionGetParams searchParam)
        {
            return BllAcciones.GetData<Ocupacion, OcupacionGetParams>(searchParam, "spGetOcupaciones", GetValidation);
        }
        public IEnumerable<BaseCatalogo> CatalogosGet(BaseCatalogoGetParams searchParam, string tipoCatalogo = null)
        {
            switch (tipoCatalogo.ToLower())
            {
                case "ocupacion":
                    return BllAcciones.GetData<Ocupacion, BaseCatalogoGetParams>(searchParam, "spGetOcupaciones", GetValidation);
                case "verificadordireccion":
                    return BllAcciones.GetData<VerificadorDireccion, BaseCatalogoGetParams>(searchParam, "spGetVerificadoresDireccion", GetValidation);
                default:
                    throw new Exception($"El tipocatalogo {tipoCatalogo} , no encontro ninguna eleccion para ejecutar ninguna consulta de datos");
            }
        }
        public void OcupacionInsUpd(Ocupacion insUpdParam)
        {
            BllAcciones.InsUpdData<Ocupacion>(insUpdParam, "spInsUpdOcupacion");
        }
    }
}
