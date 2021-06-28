using DevBox.Core.DAL.SQLServer;
using DevBox.Core.Classes.Utils;
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
        public IEnumerable<Clasificacion> ClasificacionQueRequierenGarantias(int idNegocio)
        {
            var searchParam = new BaseCatalogoGetParams { NombreTabla = "tblClasificaciones", IdNegocio= idNegocio, IdTabla = "IdClasificacion" };
            var result =  BllAcciones.GetData<Clasificacion, BaseCatalogoGetParams>(searchParam, "spGetCatalogos", GetValidation);
            var result2 = result.Where(clas => clas.RequiereGarantia);
            return result2;
        }
        public IEnumerable<Clasificacion> GetClasificaciones(ClasificacionesGetParams searchParam)
        {

            searchParam = (searchParam.IsNull()) ? new ClasificacionesGetParams() : searchParam;
            return BllAcciones.GetData<Clasificacion, ClasificacionesGetParams>(searchParam, "spGetClasificaciones", GetValidation);
        }
        public void InsUpdClasificacion(Clasificacion insUpdParam)
        {
            BllAcciones.InsUpdData<Clasificacion>(insUpdParam, "spInsUpdClasificacion");
        }
    }
}
