using emtSoft.DAL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrestamoBLL
{
    public class CuotasConCalculo
    {
        public static IGeneradorCuotas GetGeneradorDeCuotas(IInfoGeneradorCuotas info)
        {
            IGeneradorCuotas generadorCuotas = null;
            var tipoAmortizacion = info.TipoAmortizacion;
            switch (tipoAmortizacion)
            {
                case TiposAmortizacion.No_Amortizable_cuotas_fijas:
                    generadorCuotas = new GeneradorCuotasFijasNoAmortizable(info);
                    break;
                case TiposAmortizacion.Amortizable_por_dia_abierto:
                    break;
                case TiposAmortizacion.Amortizable_por_periodo_abierto:

                    break;
                case TiposAmortizacion.Amortizable_cuotas_fijas:
                    break;
                case TiposAmortizacion.No_Amortizable_abierto:
                    break;
                default:
                    break;
            }

            if (generadorCuotas == null)
            {
                throw new NotImplementedException("no se ha implementado la generacion de cuotas aun para " + tipoAmortizacion.ToString());
            }
            return generadorCuotas;
        }
    }
    public partial class BLLPrestamo
    {
        internal void InsUpdCuotas(IEnumerable<Cuota> cuotas)
        {
            var cuotasDataTable = cuotas.ToDataTable();
            try
            {
                /// preparar el parametro a enviar
                /// las propiedades deben estar en el mismo orden que en el tipo de sql server
                /// debe crear un parametro anonimo que coincida el nombre del parametro
                /// y asignarle un objeto datatable
                var _insUpdParam = SearchRec.ToSqlParams(new { cuotas = cuotasDataTable });
                DBPrestamo.ExecSelSP("spInsUpdCuotas", _insUpdParam);
            }
            catch (Exception e)
            {
                DatabaseError(e);
            }
        }

        
    }
}
