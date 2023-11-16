using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PrestamoBLL.BLLPrestamo;

namespace PrestamoBLL
{
    public class UsuarioBLL
    {
        public void InsUpd(Ingreso insUpdParam)
        {
            BllAcciones.InsUpdData<Ingreso>(insUpdParam, "spInsUpdIngresos");
        }
    }
}
