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
        public IEnumerable<BaseCatalogo> CatalogosGet(BaseCatalogoGetParams searchParam, string tipoCatalogo = null)
        {
            switch (tipoCatalogo.ToLower())
            {
                case "ocupacion":
                    return BllAcciones.GetData<Ocupacion, BaseCatalogoGetParams>(searchParam, "spGetCatalogos", GetValidation);
                case "verificadordireccion":
                    return BllAcciones.GetData<VerificadorDireccion, BaseCatalogoGetParams>(searchParam, "spGetCatalogos", GetValidation);
                case "tipotelefono":
                    return BllAcciones.GetData<TipoTelefono, BaseCatalogoGetParams>(searchParam, "spGetCatalogos", GetValidation);
                case "tiposexo":
                    return BllAcciones.GetData<TipoSexo, BaseCatalogoGetParams>(searchParam, "spGetCatalogos", GetValidation);
                case "tasador":
                    return BllAcciones.GetData<TipoTelefono, BaseCatalogoGetParams>(searchParam, "spGetCatalogos", GetValidation);
                case "localizador":
                    return BllAcciones.GetData<Localizador, BaseCatalogoGetParams>(searchParam, "spGetCatalogos", GetValidation);
                case "estadocivil":
                    return BllAcciones.GetData<EstadoCivil, BaseCatalogoGetParams>(searchParam, "spGetCatalogos", GetValidation);
                default:
                    throw new Exception($"El tipocatalogo {tipoCatalogo} , no encontro ninguna eleccion para ejecutar ninguna consulta de datos");
            }
        }
    }
}
