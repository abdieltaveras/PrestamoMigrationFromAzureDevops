using DevBox.Core.Classes.Utils;
using DevBox.Core.DAL.SQLServer;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using static PrestamoBLL.BLLPrestamo;

namespace PrestamoBLL
{
    //public class CuotasConCalculo
    //{
    //    public static IGeneradorCuotas GetGeneradorDeCuotas(IInfoGeneradorCuotas info)
    //    {
    //        IGeneradorCuotas generadorCuotas = null;
    //        var tipoAmortizacion = info.TipoAmortizacion;
    //        switch (tipoAmortizacion)
    //        {
    //            case TiposAmortizacion.No_Amortizable_cuotas_fijas:
    //                generadorCuotas = new GeneradorCuotasFijasNoAmortizable(info);
    //                break;
    //            case TiposAmortizacion.Amortizable_por_dia_abierto:
    //                break;
    //            case TiposAmortizacion.Amortizable_por_periodo_abierto:

    //                break;
    //            case TiposAmortizacion.Amortizable_cuotas_fijas:
    //                break;
    //            case TiposAmortizacion.No_Amortizable_abierto:
    //                break;
    //            default:
    //                break;
    //        }

    //        if (generadorCuotas == null)
    //        {
    //            throw new NotImplementedException("no se ha implementado la generacion de cuotas aun para " + tipoAmortizacion.ToString());
    //        }
    //        return generadorCuotas;
    //    }
    //}
    public partial class BLLPrestamo
    {
        
        
    }
    
}
