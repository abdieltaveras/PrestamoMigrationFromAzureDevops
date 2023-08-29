﻿using PrestamoEntidades;
using System;
using System.Collections.Generic;

namespace PrestamoBLL
{
    public static class CuotasGenerator
    {
        public static IEnumerable<CxCCuota> CreateCuotas(IInfoGeneradorCuotas infGenCuotas)
        {
            var result = GetGeneradorDeCuotas(infGenCuotas);
            var cuotas = result.GenerarCuotas();
            return cuotas;
        }
        private static IGeneradorCuotas GetGeneradorDeCuotas(IInfoGeneradorCuotas info)
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
    //public class PrestamoManager
    //{
    //    //private TipoMoraBLL _TipoMoraBLL { get; set; } 
    //    //private PeriodoBLL _PeriodoBLL { get; set; }

    //    #region property members

    //    private PrestamoConCalculos prestamoInProgress = new PrestamoConCalculos();

    //    public IEnumerable<string> ErrorMessages { get; set; } = new List<string>();
    //    #endregion property members

    //    internal static async Task<PrestamoManager> Create(Prestamo prestamo)
    //    {

    //        var myclass = new PrestamoManager();
    //        await myclass.SetPrestamo(prestamo);
    //        return myclass;
    //    }
    //    private  PrestamoManager()
    //    {

    //    }

    //    private void NotificadorDeMensaje(object sender, string e)
    //    {

    //    }

    //    //Todo: debo arreglar esto con luis

    //    /// <summary>
    //    /// recibe un prestamo para validarlo y hacer todo el proceso necesario de calculos antes de enviarlo
    //    /// a la base de datos.
    //    /// </summary>
    //    /// <param name="prestamo"></param>
    //    private async Task SetPrestamo(Prestamo prestamo)
    //    {
    //        BLLValidations.ValueGreaterThanZero(prestamo.IdLocalidadNegocio, "Id Localidad negocio");
    //        BLLValidations.StringNotEmptyOrNull(prestamo.Usuario, "Usuario");
    //        // hay que enviar usuario e idlocalidad
    //        //_TipoMoraBLL = new TipoMoraBLL(prestamo.IdLocalidadNegocio,prestamo.Usuario);
    //        //_PeriodoBLL = new PeriodoBLL(prestamo.IdLocalidadNegocio, prestamo.Usuario);
    //        var clasificaciones = BLLPrestamo.Instance.GetClasificaciones(new ClasificacionesGetParams { IdNegocio = prestamo.IdNegocio });
    //        var tiposMora = new TipoMoraBLL(prestamo.IdLocalidadNegocio, prestamo.Usuario).GetTiposMoras(new TipoMoraGetParams { IdNegocio = prestamo.IdNegocio });
    //        var tasasDeInteres = new TasaInteresBLL(prestamo.IdLocalidadNegocio,prestamo.Usuario).GetTasasDeInteres(new TasaInteresGetParams { IdNegocio = prestamo.IdNegocio});
    //        var periodos = new PeriodoBLL(prestamo.IdLocalidadNegocio, prestamo.Usuario).GetPeriodos(new PeriodoGetParams { IdNegocio = prestamo.IdNegocio });

    //        prestamoInProgress = new PrestamoConCalculos();
    //        prestamoInProgress = prestamo.ToJson().ToType<PrestamoConCalculos>();
    //        prestamoInProgress.SetServices(this.NotificadorDeMensaje, clasificaciones, tiposMora, tasasDeInteres, periodos);

    //        await prestamoInProgress.ExecCalcs();
    //    }

    //    private IGeneradorCuotas GetGeneradorCuotas()
    //    {
    //        prestamoInProgress.ProyectarPrimeraYUltima = false;
    //        //var tipoAmortizacion = (TiposAmortizacion)prestamoInProgress.IdTipoAmortizacion;
    //        return CuotasConCalculo.GetGeneradorDeCuotas(prestamoInProgress);
    //    }


    //    public PrestamoInsUpdParam Build()
    //    {
    //        IGeneradorCuotas genCuotas = GetGeneradorCuotas();
    //        var cuotas = genCuotas.GenerarCuotas();
    //        // var cuotasVacias = new List<Cuota>();
    //        var prestamoConDependencias = new PrestamoInsUpdParam(cuotas);
    //        _type.CopyPropertiesTo(prestamoInProgress, prestamoConDependencias);
    //        return prestamoConDependencias;
    //    }
    //}


}
