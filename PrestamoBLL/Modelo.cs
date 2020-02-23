using emtSoft.DAL;
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
        public void ModeloInsUpd(Modelo insUpdParam)
        {
            BllAcciones.InsUpdData<Modelo>(insUpdParam, "spInsUpdModelo");
        }

        public IEnumerable<ModeloWithMarca> ModelosGet(ModeloGetParams searchParam)
        {
            return BllAcciones.GetData<ModeloWithMarca, ModeloGetParams>(searchParam, "spGetModelos", GetValidation);
        }

        public IEnumerable<Modelo> ModelosByMarcaGet(ModeloGetParams searchParam)
        {
            return BllAcciones.GetData<Modelo, ModeloGetParams>(searchParam, "spGetModelosGarantiaByMarca", GetValidation);
        }
    }
}
