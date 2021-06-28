using DevBox.Core.DAL.SQLServer;
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
        public IEnumerable<Marca> GetMarcas(MarcaGetParams searchParam)
        {
            return BllAcciones.GetData<Marca, MarcaGetParams>(searchParam, "spGetMarcas", GetValidation);
        }
        public void InsUpdMarca(Marca insUpdParam)
        {
            BllAcciones.InsUpdData<Marca>(insUpdParam, "spInsUpdMarca");
        }

    }
}
