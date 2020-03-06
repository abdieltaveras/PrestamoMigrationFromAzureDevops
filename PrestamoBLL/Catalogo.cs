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
        public IEnumerable<BaseCatalogo>CatalogosGet(BaseCatalogoGetParams searchParam)
        {
            switch (searchParam.NombreTabla)
            {
                case "tblOcupaciones":
                    return BllAcciones.GetData<Ocupacion, BaseCatalogoGetParams>(searchParam, "spGetCatalogos", GetValidation);
                case "tblVerificadorDirecciones":
                    return BllAcciones.GetData<VerificadorDireccion, BaseCatalogoGetParams>(searchParam, "spGetCatalogos", GetValidation);
                case "tblTipoTelefonos":
                    return BllAcciones.GetData<TipoTelefono, BaseCatalogoGetParams>(searchParam, "spGetCatalogos", GetValidation);
                case "tblTipoSexos":
                    return BllAcciones.GetData<TipoSexo, BaseCatalogoGetParams>(searchParam, "spGetCatalogos", GetValidation);
                case "tblTasadores":
                    return BllAcciones.GetData<Tasador, BaseCatalogoGetParams>(searchParam, "spGetCatalogos", GetValidation);
                case "tblLocalizadores":
                    return BllAcciones.GetData<Localizador, BaseCatalogoGetParams>(searchParam, "spGetCatalogos", GetValidation);
                case "tblEstadosCiviles":
                    return BllAcciones.GetData<EstadoCivil, BaseCatalogoGetParams>(searchParam, "spGetCatalogos", GetValidation);
                default:
                    throw new Exception($"La tabla {searchParam.NombreTabla} , no se encontro en ninguna eleccion para ejecutar una consulta de datos");
            }
        }
        public void CatalogoInsUpd(Catalogo insUpdParams )
        {
            BllAcciones.InsUpdData(insUpdParams, "spInsUpdCatalogo");
        }
        public void CatalogoToggleStatus(ToggleStatusCatalogo toggleStatusParams)
        {
            BllAcciones.CancelData(toggleStatusParams, "spToggleStatusCatalogo");
        }
        public void CatalogoDel(DelCatalogo delParams)
        {
            BllAcciones.CancelData(delParams, "spDelCatalogo");
        }
    }
}
