﻿using emtSoft.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoBLL.Entidades
{
    public enum TiposAmortizacion { Cuotas_fijas_No_amortizable = 1, Abierto_amortizable_por_dia, Abierto_Amortizable_por_periodo_abierto, Cuotas_fijas_amortizable, Abierto_No_Amortizable }
    public class InfoDeudaPrestamo : IInfoDeudaPrestamo
    {
        public int CantidadDeCuotas { get; internal set; }

        public double CuotasAtrasadas { get; internal set; }

        public double CuotasNoVencidas { get; internal set; }

        public decimal TotalCapital { get; internal set; }

        public decimal TotalInteres { get; internal set; }

        public decimal TotalInteresDespuesDeVencido { get; internal set; }

        public decimal TotalMoras { get; internal set; }

        public decimal TotalOtrosCargos { get; internal set; }

        public decimal DeudaTotal { get; internal set; }
        public decimal DeudaVencida { get; internal set; }
        public decimal DeudaNoVencida => DeudaTotal - DeudaVencida;

        public string OtrosDetalles { get; internal set; } = string.Empty;
    }

    public class InfoPrestamoDrCr : Prestamo, IInfoPrestamoDrCr
    {
        public string NombreClasificacion { get; internal set; }

        public string TipoMora { get; internal set; }

        public string OtrosDetalles { get; internal set; }

        public string NombreTipoAmortizacion { get; internal set; }

        public string NombreTipoMora { get; internal set; }

        public string NombrePeriodo { get; internal set; }
    }

    public class PrestamoConDetallesParaCreditosYDebitos : IPrestamoConDetallesParaCreditosyDebitos
    {
        public IInfoPrestamoDrCr infoPrestamo { get; internal set; }

        public IInfoClienteDrCr infoCliente { get; internal set; }

        public IInfoGarantia infoGarantia { get; internal set; }

        public IInfoDeudaPrestamo InfoDeuda { get; internal set; }
    }
    public class Prestamo : BaseInsUpd, IPrestamoForGeneradorCuotas
    {
        public int IdPrestamo { get; set; }

        [IgnorarEnParam]
        public string PrestamoNumero { get; internal set; } = string.Empty;

        public int? IdPrestamoARenovar { get; set; } = 0;
        [IgnorarEnParam]
        /// attention analizar poner un objeto InfoPrestamoForView que permita poner todos los campos que uno pudiera necesitar como este NumeroPrestamoARenovar, etc
        public string NumeroPrestamoARenovar { get; internal set; }

        public int IdClasificacion { get; set; }

        private int _IdTipoAmortizacion { get; set; } = 1;
        public int IdTipoAmortizacion { 
                get { return _IdTipoAmortizacion; }  
                internal set {
                _IdTipoAmortizacion = value;
                TipoAmortizacion = (TiposAmortizacion)IdTipoAmortizacion; } }

        [ignorarEnParam]
        public TiposAmortizacion TipoAmortizacion {
            get { return (TiposAmortizacion)IdTipoAmortizacion; }
            set { _IdTipoAmortizacion = (int)value; } } 

        /// <summary>
        /// retorna true o false al contar si hay o no garantias para este prestamo
        /// </summary>
        [IgnorarEnParam]
        public bool TieneGarantias { get { return IdGarantias.Count() > 0; } }
        /// <summary>
        /// Los id de los clientes asignado a este prestamo
        /// </summary>
        public int IdCliente { get; set; } = 0;

        [IgnorarEnParam]
        public List<Garantia> _Garantias { get; set; } = new List<Garantia>();
        [IgnorarEnParam]
        public List<int> IdGarantias { get; set; } = new List<int>();

        [IgnorarEnParam]
        public List<Codeudor> _Codeudores { get; set; }

        [IgnorarEnParam]
        public List<int> IdCodeudores { get; set; }

        public DateTime FechaEmisionReal { get; set; } = DateTime.Now;

        public DateTime FechaEmisionParaCalculos { get; internal set; } = DateTime.Now;
        public DateTime FechaVencimiento { get; internal set; }
        public int IdTasaInteres { get; set; }
        [IgnorarEnParam]
        public decimal TasaDeInteresPorPeriodo { get; set; }

        public int IdTipoMora { get; set; }

        public int IdPeriodo { get; set; }
        [IgnorarEnParam]
        public Periodo Periodo { get; internal set; }

        public int CantidadDePeriodos { get; set; }

        public decimal MontoPrestado { get; set; }
        public decimal DeudaRenovacion { get; set; }
        /// <summary>
        /// tiene sumado el dinero emitido al cliente (monto prestado) + le deuda de la r
        /// </summary>
        [IgnorarEnParam]
        public decimal TotalPrestado => MontoPrestado + DeudaRenovacion;
        // { get { return MontoPrestado + DeudaRenovacion } internal set { var valor = value;} }

        public int IdDivisa { get; set; }
        [IgnorarEnParam]
        public bool LlevaGastoDeCierre => InteresGastoDeCierre > 0;
        public decimal InteresGastoDeCierre { get; set; }

        public decimal MontoGastoDeCierre { get; internal set; }

        public bool GastoDeCierreEsDeducible { get; set; }

        public bool SumarGastoDeCierreALasCuotas { get; set; }

        public bool CargarInteresAlGastoDeCierre { get; set; }

        public bool AcomodarFechaALasCuotas { get { return FechaInicioPrimeraCuota != InitValues._19000101; } }
        /// <summary>
        ///  si se acomoda el prestamo se debe indicar cual es la fecha en que desea que la primera cuota sea generada
        /// </summary>

        public DateTime FechaInicioPrimeraCuota { get; internal set; } = InitValues._19000101;

        /// <summary>
        /// este campo es el que tendra la fecha real de donde partira a generar las cuotas y sus fechas de vencimientos, es necesario para cuando al prestamo se le acomode las cuotas
        /// </summary>

        public Prestamo()
        {

        }

        public Prestamo(Periodo periodo)
        {
            var periodoDest = new Periodo();
            _type.CopyPropertiesTo(periodo, periodoDest);
            this.Periodo = periodoDest;
        }
        //internal IEnumerable<Cuota> _Cuotas { get; set; } = new List<Cuota>();
        //public DataTable Cuotas => this._Cuotas.ToDataTable();
        //public DataTable Garantias => this.IdGarantias.Select(gar => new { idGarantia = gar }).ToDataTable();
        ////this._Garantias.ToDataTable();
        //public DataTable Codeudores => this.IdCodeudores.Select(cod => new { idCodeudor = cod }).ToDataTable();
    }

    public class PrestamoInsUpdParam : Prestamo
    {
        private IEnumerable<Cuota> _CuotasList = new List<Cuota>();
        private IEnumerable<Codeudor> __Codeudores = new List<Codeudor>();
        private IEnumerable<Garantia> __Garantias = new List<Garantia>();

        public PrestamoInsUpdParam(Prestamo prestamo, IEnumerable<Cuota> cuotas, IEnumerable<Codeudor> codeudores, IEnumerable<Garantia> garantias)
        {
            this._CuotasList = cuotas;
            //var data = codeudores.Select(cod => new { idCodeudor = cod.IdCodeudor });
            //this.__Codeudores = codeudores != null ? codeudores : new List<Codeudor>(); ;
            //this.__Garantias = garantias != null ? garantias : new List<Garantia>(); ;
        }
        public DataTable Garantias => this.IdGarantias.Select(gar => new { idGarantia = gar }).ToDataTable();
        //this._Garantias.ToDataTable();
        public DataTable Codeudores => this.IdCodeudores.Select(cod => new { idCodeudor = cod }).ToDataTable();
        public DataTable Cuotas => this._CuotasList.ToDataTable();

    }



    internal class PrestamoGarantias
    {
        public int IdGarantia { get; set; }
    }

    internal class PrestamoCodeudores
    {
        public int IdCodeudor { get; set; }
    }

    public class PrestamoGetParam
    {
        public int idPrestamo { get; set; } = -1;
        public int idCliente { get; set; } = -1;
        public int idGarantia { get; set; } = -1;
        public DateTime fechaEmisionRealDesde { get; set; } = InitValues._19000101;
        public DateTime fechaEmisionRealHasta { get; set; } = InitValues._19000101;
    }
}



