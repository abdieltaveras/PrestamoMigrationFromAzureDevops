using emtSoft.DAL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL
{
    public partial class BLLPrestamo
    {
        public IEnumerable<BaseCatalogo> GetCatalogos(BaseCatalogoGetParams searchParam)
        {
            if (string.IsNullOrEmpty(searchParam.IdTabla))
            {
                throw new NullReferenceException("El valor de la propiedad IdTabla esta vacia, esta propiedad debe asignarle una cadena igual al nombre de la columna principal de esa tabla ejemplo IdColor, IdClasifiacion, idEstadoCivil, etc.");
            }
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
                    return BllAcciones.GetData<TipoTelefono, BaseCatalogoGetParams>(searchParam, "spGetCatalogos", GetValidation);
                case "tblLocalizadores":
                    return BllAcciones.GetData<Localizador, BaseCatalogoGetParams>(searchParam, "spGetCatalogos", GetValidation);
                case "tblEstadosCiviles":
                    return BllAcciones.GetData<EstadoCivil, BaseCatalogoGetParams>(searchParam, "spGetCatalogos", GetValidation);
                case "tblClasificaciones":
                    return BllAcciones.GetData<Clasificacion, BaseCatalogoGetParams>(searchParam, "spGetCatalogos", GetValidation);
                default:
                    throw new Exception($"La tabla {searchParam.NombreTabla} , no se encontro en ninguna eleccion para ejecutar una consulta de datos");
            }
        }
        public void InsUpdCatalogo(Catalogo insUpdParams )
        {
            BllAcciones.InsUpdData(insUpdParams, "spInsUpdCatalogo");
        }
        public void CatalogoToggleStatus(ToggleStatusCatalogo toggleStatusParams)
        {
            BllAcciones.CancelData(toggleStatusParams, "spToggleStatusCatalogo");
        }
        public void AnularCatalogo(AnularCatalogo delParams)
        {
            BllAcciones.CancelData(delParams, "spDelCatalogo");
        }

        public List<T> SearchCatalogos<T>(SearchCatalogoParams searchParams) where T : class
        {
            var searchSqlParams = SearchRec.ToSqlParams(searchParams);
            List<T> catalogo = new List<T>();
            try
            {
                catalogo = DBPrestamo.ExecReaderSelSP<T>("CatalogoSpBuscar", searchSqlParams);
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return catalogo;
        }
        public List<T> GetCatalogosNew<T>(CatalogoGetParams searchParams) where T : class
        {
            var searchSqlParams = SearchRec.ToSqlParams(searchParams);
            List<T> catalogo = new List<T>();
            try
            {
                catalogo = DBPrestamo.ExecReaderSelSP<T>("spGetCatalogos", searchSqlParams);
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
            return catalogo;
        }
    }
}
