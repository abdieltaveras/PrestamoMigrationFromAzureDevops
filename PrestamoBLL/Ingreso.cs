using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrestamoEntidades;
using DevBox.Core.DAL.SQLServer;
namespace PrestamoBLL
{
    public partial class BLLPrestamo
    {
        

        public void InsUpdIngreso(Ingreso insUpdParam)
        {
            BllAcciones.InsUpdData<Ingreso>(insUpdParam, "spInsUpdIngresos");
        }

        public IEnumerable<Ingreso> GetIngresos(IngresoGetParams searchParam)
        {
            GetValidation(searchParam as BaseGetParams);
            return BllAcciones.GetData<Ingreso, IngresoGetParams>(searchParam, "spGetDatosPIngreso", GetValidation);
        }

        public void AnularIngreso(int idIngreso, string usuario)
        {
            PendienteDeImplementacionException();
        }

        public class AplicarIngreso
        {
            public AplicarIngreso(int idPrestamo, DateTime Fecha, Decimal Monto)
            {
                // debo primero buscar todos los cargos del prestamo
                    // necesitamos una aplicacion que me devuelva la cxc del prestamo
                // recorrer los cargos para ir aplicando el pago
                // guardar la informaciones del pago realizado con todos sus detalles
            }
        }
    }
}
