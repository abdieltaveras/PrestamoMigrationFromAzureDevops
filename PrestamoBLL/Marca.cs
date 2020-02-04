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
        public IEnumerable<Marca>MarcasGet(MarcaGetParams searchParam)
        {
            return BllAcciones.GetData<Marca, MarcaGetParams>(searchParam, "spGetMarcas", GetValidation);
        }
        public void MarcaInsUpd(Marca insUpdParam)
        {
            BllAcciones.InsUpdData<Marca>(insUpdParam, "spInsUpdMarca");
        }

    }
}
