using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    partial class BLLPrestamo
    {
        public IEnumerable<Ocupacion> OcupacionesGet(OcupacionGetParams searchParam)
        {
            return BllAcciones.GetData<Ocupacion, OcupacionGetParams>(searchParam, "spGetOcupaciones", GetValidation);
        }
        public IEnumerable<BaseCatalogo> CatalogosGet(BaseGetParams searchParam, string tipoCatalogo = null)
        {
            switch (tipoCatalogo)
            {
                case "ocupacion":
                    return BllAcciones.GetData<Ocupacion, OcupacionGetParams>((OcupacionGetParams)searchParam, "spGetOcupaciones", GetValidation);
                case "verificadordirreccion":
                    return BllAcciones.GetData<VerificadorDireccion, VerificadorDireccionGetParams>((VerificadorDireccionGetParams)searchParam, "spGetVerificadoresDireccion", GetValidation);
                default:
                    return null;
            }
        }
        public void OcupacionInsUpd(Ocupacion insUpdParam)
        {
            BllAcciones.InsUpdData<Ocupacion>(insUpdParam, "spInsUpdOcupacion");
        }
    }
}
