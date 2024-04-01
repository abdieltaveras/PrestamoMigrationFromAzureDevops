using DevBox.Core.DAL.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrestamoEntidades
{
    public enum TiposCargosPrestamo { Capital, InteresCapital, InteresDespuesDeVencido, GastoDeCierre, InteresGastoDeCierre, OtrosCargos, InteresOtrosCargos, CargoPorAtraso }
    public enum TiposDrCr { Debito='D', Credito='C' };
    public enum TiposTransaccionesCxCPrestamo { Cuota, Pago, NotaDeDebito, NotaDeCredito, Recargo }
    //public abstract class CxCPrestamoMaestroBase
    //{
    //    public virtual int IdCxCM { get; set; }
    //    public virtual int IdPrestamo { get; set; } = 0;
    //    /// <summary>
    //    /// Para especificar si es un debito o un credito
    //    /// </summary>
    //    public virtual TiposDrCr TipoDrCr { get; protected set; }

    //    /// <summary>
    //    /// para indicar el tipo de transaccion si es Cuota, NotaDeDebito, etc
    //    /// </summary>
    //    public virtual TiposTransaccionesCxCPrestamo TipoTransaccionCxC { get; protected set; }

    //    //public virtual string NoTransaccion
    //    //{
    //    //    get; internal set;
    //    //}

    //    public virtual string NoTransaccionManual { get; set; } = string.Empty;
    //    public virtual DateTime Fecha { get; set; } = InitValues._19000101;
    //    public virtual Decimal Monto { get; set; } = 0;


    //    public CxCPrestamoMaestroBase(TiposDrCr tipoDrCr, TiposTransaccionesCxCPrestamo tipoTransaccion)
    //    {

    //        this.TipoDrCr = tipoDrCr;
    //        this.TipoTransaccionCxC = tipoTransaccion;
    //    }

    //    //public void Add(IEnumerable<CxCPrestamoDetalle> items)
    //    //{
    //    //    items.ToList().ForEach(item => this.AddDetalle(item));
    //    //}
    //    //public void Remove(CxCPrestamoDetalle item)
    //    //{
    //    //    this.ItemsCxC.Remove(item);
    //    //    this.Monto -= item.Monto;
    //    //    this.Balance -= item.Balance;
    //    //}
    //    //public void Remove(IEnumerable<CxCPrestamoDetalle> items)
    //    //{
    //    //    items.ToList().ForEach(item => this.Remove(item));
    //    //}
    //}

    //public interface ICxCPrestamoDrMaestroBase
    //{
    //    decimal Balance { get; }
    //    decimal CapitalBce { get; }
    //    decimal CapitalMonto { get; set; }
    //    decimal CargoPorAtrasoBce { get; }
    //    decimal CargoPorAtrasoMonto { get; set; }
    //    decimal GastoDeCierreBce { get; }
    //    decimal GastoDeCierreMonto { get; set; }
    //    decimal InteresCapitalBce { get; }
    //    decimal InteresCapitalMonto { get; set; }
    //    decimal InteresDespuesDeVencidoBce { get; }
    //    decimal InteresDespuesDeVencidoMonto { get; set; }
    //    decimal InteresGastoDeCierreMonto { get; set; }
    //    decimal InteresOtrosCargosMonto { get; set; }

    //    decimal OtrosCargosBce { get; }
    //    decimal OtrosCargosMonto { get; set; }

    //    string ToString();
    //}

    //public abstract class CxCPrestamoDrMaestroBase : CxCPrestamoMaestroBase, ICxCPrestamoDrMaestroBase
    //{
    //    public CxCPrestamoDrMaestroBase(TiposTransaccionesCxCPrestamo tipoTransaccion) : base(TiposDrCr.Debito, tipoTransaccion)
    //    {

    //    }
    //    public virtual Decimal Balance => this.ItemsDrCxC.Select(item => item.Balance).Sum();
    //    //public List<CxCPrestamoDrItemBase> _ItemsDrCxC 
    //    public List<CxCPrestamoDrItemBase> ItemsDrCxC { get; internal set; } = new List<CxCPrestamoDrItemBase>();
    //    protected void InsUpdItemCxC(CxCPrestamoDrItemBase item)
    //    {
    //        // busquemos si ya previamente existe
    //        var itemInserted = this.ItemsDrCxC.Where(current => current.TipoCargo == item.TipoCargo).FirstOrDefault();

    //        if (itemInserted == null)
    //        {
    //            item.Balance = item.Monto;
    //            this.ItemsDrCxC.Add(item);
    //            this.Monto += item.Monto;
    //        }
    //        else
    //        {
    //            var diferencia = itemInserted.Monto - item.Monto;
    //            itemInserted.Monto = item.Monto;
    //            itemInserted.Balance = item.Monto;
    //            this.Monto += diferencia;
    //        }
    //    }

    //    /// <summary>
    //    /// Agrega el cargo debe ser una cuota nueva
    //    /// </summary>
    //    /// <param name="tipoCargo"></param>
    //    /// <param name="monto"></param>

    //    internal void InsUpdCargoMonto(TiposCargosPrestamo tipoCargo, Decimal monto)
    //    {
    //        if (monto <= 0)
    //        {
    //            return;
    //        }
    //        else
    //        {
    //            var detalleCargoCuota = new CuotaPrestamoDetalle(tipoCargo, monto);
    //            this.InsUpdItemCxC(detalleCargoCuota);
    //        }
    //    }

    //    /// <summary>
    //    /// El valor original del capital de la cuota
    //    /// </summary>
    //    public Decimal CapitalMonto
    //    { get { return this.GetCapitalMonto(); } set { this.SetCapitalMonto(value); } }
    //    /// <summary>
    //    /// El balance del capital
    //    /// </summary>

    //    public Decimal CapitalBce { get { return this.GetCapitalBce(); } }


    //    public Decimal InteresCapitalMonto { get { return this.GetInteresCapitalMonto(); } set { this.SetInteresCapitalMonto(value); } }


    //    public Decimal InteresCapitalBce { get { return this.GetInteresCapitalBce(); } }

    //    public Decimal GastoDeCierreMonto { get { return this.GetGastoDeCierreMonto(); } set { this.SetGastoDeCierreMonto(value); } }

    //    public Decimal InteresGastoDeCierreMonto { get { return this.GetInteresGastoDeCierreMonto(); } set { this.SetInteresGastoDeCierreMonto(value); } }


    //    public Decimal GastoDeCierreBce { get { return this.GetInteresCapitalBce(); } }


    //    public Decimal OtrosCargosMonto { get { return this.GetOtrosCargosMonto(); } set { this.SetOtrosCargosMonto(value); } }

    //    public Decimal InteresOtrosCargosMonto { get { return this.GetInteresOtrosCargosMonto(); } set { this.SetInteresOtrosCargosMonto(value); } }


    //    public Decimal OtrosCargosBce { get { return this.GetInteresOtrosCargosBce(); } }


    //    public Decimal InteresDespuesDeVencidoMonto { get { return this.GetInteresDespuesDeVencidoMonto(); } set { this.SetInteresDespuesDeVencidoMonto(value); } }


    //    public Decimal InteresDespuesDeVencidoBce { get { return this.GetInteresDespuesDeVencidoBce(); } }


    //    public Decimal CargoPorAtrasoMonto { get { return this.GetCargosPorAtrasoMonto(); } set { this.SetCargosPorAtrasoMonto(value); } }


    //    public Decimal CargoPorAtrasoBce { get { return this.GetCargosPorAtrasoBce(); } }

    //    public override string ToString()
    //    {
    //        return $" Transaccion {this.TipoTransaccionCxC} Tipo Contable {this.TipoDrCr}   Monto {this.Monto} Balance {this.Balance} Items {this.ItemsDrCxC.Count}";
    //    }

    //}


    //public abstract class CxCPrestamoDrItemBase : IEquatable<CxCPrestamoDrItemBase>
    //{
    //    public int IdCxCD { get; internal set; }

    //    public int IdCxCM { get; internal set; }

    //    [IgnoreOnParams]
    //    public string IdGuid { get; private set; }

    //    public string Cuenta { get; internal set; }
    //    public Decimal Monto { get; internal set; }

    //    public TiposCargosPrestamo TipoCargo { get; internal set; }
    //    public Decimal Balance { get; set; }

    //    public virtual List<CxCPrestamoDrItemBase> ItemsCxC { get; internal set; } = new List<CxCPrestamoDrItemBase>();
    //    public CxCPrestamoDrItemBase()
    //    {
    //        IdGuid = Guid.NewGuid().ToString();
    //    }

    //    public bool Equals(CxCPrestamoDrItemBase other) => other == null ? false : this.IdCxCD.Equals(other.IdCxCD) && this.IdGuid.Equals(other.IdGuid);

    //    public override bool Equals(object obj)
    //    {
    //        if (obj == null)
    //        {
    //            return false;
    //        }

    //        var cxcDObj = obj as CxCPrestamoDrItemBase;
    //        if (cxcDObj == null)
    //        {
    //            return false;
    //        }
    //        else
    //        {
    //            return Equals(cxcDObj);
    //        }
    //    }

    //    public override int GetHashCode()
    //    {
    //        return HashCode.Combine(this.IdCxCD, this.IdGuid);
    //    }

    //    public override string ToString()
    //    {
    //        return $"{TipoCargo} Monto Original {Monto} Balance {Balance}";
    //    }
    //}


    //public enum ResultadosOperacionesCxC { Exitosa, Fallida }

    //public class CuotaPrestamo : CxCPrestamoDrMaestroBase
    //{
    //    public int NumeroCuota { get; protected set; }


    //    public virtual DateTime FechaMoraAplicada { get; set; }
    //    public virtual DateTime FechaMoraTransitoria { get; set; }

    //    public virtual DateTime FechaInteresVencidoAplicado { get; set; }

    //    public virtual DateTime FechaInteresVencidoTransitorio { get; set; }


    //    [IgnoreOnParams]
    //    public string Detalle { get; set; }

    //    public CuotaPrestamo(int numeroCuota, int idPrestamo, DateTime fecha) : base(TiposTransaccionesCxCPrestamo.Cuota)
    //    {
    //        this.IdPrestamo = idPrestamo;
    //        this.NumeroCuota = numeroCuota;
    //        this.Fecha = fecha;
    //        this.FechaMoraAplicada = fecha;
    //        this.FechaMoraTransitoria = fecha;
    //        this.FechaInteresVencidoAplicado = fecha;
    //        this.FechaInteresVencidoTransitorio = fecha;
    //        this.TipoTransaccionCxC = TiposTransaccionesCxCPrestamo.Cuota;
    //        this.TipoDrCr = TiposDrCr.Debito;
    //    }
    //}

    //public class CuotasPrestamo
    //{
    //    public IEnumerable<CuotaPrestamo> Cuotas { get; set; }
    //    public CuotasPrestamo(IEnumerable<CuotaPrestamo> cuotas)
    //    {
    //        this.Cuotas = cuotas;
    //    }

    //}

    //public class CuotaPrestamoDetalle : CxCPrestamoDrItemBase
    //{
    //    public CuotaPrestamoDetalle(TiposCargosPrestamo tipoCargoPrestamo, Decimal monto) : base()
    //    {
    //        this.TipoCargo = tipoCargoPrestamo;
    //        this.Monto = monto;
    //    }

    //}

    public class CuotaModel
    {
        public int NumeroCuota { get; set; }
        public decimal Monto { get; set; }
        public decimal Balance { get; set; }
    }
    //public static class ExtMetCuotaPrestamo
    //{
    //    private static decimal GetMonto(CxCPrestamoDrItemBase cxCPrestamoDetalle) => cxCPrestamoDetalle == null ? 0 : cxCPrestamoDetalle.Monto;

    //    private static decimal GetBalance(CxCPrestamoDrItemBase cxCPrestamoDetalle) => cxCPrestamoDetalle == null ? 0 : cxCPrestamoDetalle.Balance;

    //    internal static Decimal GetCapitalMonto(this CxCPrestamoDrMaestroBase cuota) => GetMonto(cuota.ItemsDrCxC.Where(cta => cta.TipoCargo == TiposCargosPrestamo.Capital).FirstOrDefault());

    //    internal static Decimal GetCapitalBce(this CxCPrestamoDrMaestroBase cuota) => GetBalance(cuota.ItemsDrCxC.Where(cta => cta.TipoCargo == TiposCargosPrestamo.Capital).FirstOrDefault());

    //    internal static Decimal GetInteresCapitalMonto(this CxCPrestamoDrMaestroBase cuota) => GetMonto(cuota.ItemsDrCxC.Where(cta => cta.TipoCargo == TiposCargosPrestamo.InteresCapital).FirstOrDefault());

    //    internal static Decimal GetInteresCapitalBce(this CxCPrestamoDrMaestroBase cuota) => GetBalance(cuota.ItemsDrCxC.Where(cta => cta.TipoCargo == TiposCargosPrestamo.InteresCapital).FirstOrDefault());



    //    internal static Decimal GetGastoDeCierreMonto(this CxCPrestamoDrMaestroBase cuota) => GetMonto(cuota.ItemsDrCxC.Where(cta => cta.TipoCargo == TiposCargosPrestamo.GastoDeCierre).FirstOrDefault());


    //    internal static Decimal GetGastoDeCierreBce(this CxCPrestamoDrMaestroBase cuota) => GetBalance(cuota.ItemsDrCxC.Where(cta => cta.TipoCargo == TiposCargosPrestamo.GastoDeCierre).FirstOrDefault());

    //    internal static Decimal GetInteresGastoDeCierreMonto(this CxCPrestamoDrMaestroBase cuota) => GetMonto(cuota.ItemsDrCxC.Where(cta => cta.TipoCargo == TiposCargosPrestamo.InteresGastoDeCierre).FirstOrDefault());

    //    internal static Decimal GetInteresGastoDeCierreBce(this CxCPrestamoDrMaestroBase cuota) => GetBalance(cuota.ItemsDrCxC.Where(cta => cta.TipoCargo == TiposCargosPrestamo.InteresGastoDeCierre).FirstOrDefault());
    //    internal static Decimal GetOtrosCargosMonto(this CxCPrestamoDrMaestroBase cuota) => GetMonto(cuota.ItemsDrCxC.Where(cta => cta.TipoCargo == TiposCargosPrestamo.OtrosCargos).FirstOrDefault());

    //    internal static Decimal GetOtrosCargosBce(this CxCPrestamoDrMaestroBase cuota) => GetBalance(cuota.ItemsDrCxC.Where(cta => cta.TipoCargo == TiposCargosPrestamo.OtrosCargos).FirstOrDefault());

    //    internal static Decimal GetInteresOtrosCargosMonto(this CxCPrestamoDrMaestroBase cuota) => GetMonto(cuota.ItemsDrCxC.Where(cta => cta.TipoCargo == TiposCargosPrestamo.InteresOtrosCargos).FirstOrDefault());

    //    internal static Decimal GetInteresOtrosCargosBce(this CxCPrestamoDrMaestroBase cuota) => GetBalance(cuota.ItemsDrCxC.Where(cta => cta.TipoCargo == TiposCargosPrestamo.InteresOtrosCargos).FirstOrDefault());

    //    internal static Decimal GetInteresDespuesDeVencidoMonto(this CxCPrestamoDrMaestroBase cuota) => GetMonto(cuota.ItemsDrCxC.Where(cta => cta.TipoCargo == TiposCargosPrestamo.InteresDespuesDeVencido).FirstOrDefault());


    //    internal static Decimal GetInteresDespuesDeVencidoBce(this CxCPrestamoDrMaestroBase cuota) => GetBalance(cuota.ItemsDrCxC.Where(cta => cta.TipoCargo == TiposCargosPrestamo.InteresDespuesDeVencido).FirstOrDefault());

    //    internal static Decimal GetCargosPorAtrasoMonto(this CxCPrestamoDrMaestroBase cuota) => GetMonto(cuota.ItemsDrCxC.Where(cta => cta.TipoCargo == TiposCargosPrestamo.CargoPorAtraso).FirstOrDefault());


    //    internal static Decimal GetCargosPorAtrasoBce(this CxCPrestamoDrMaestroBase cuota) => GetBalance(cuota.ItemsDrCxC.Where(cta => cta.TipoCargo == TiposCargosPrestamo.CargoPorAtraso).FirstOrDefault());


    //    internal static void SetCapitalMonto(this CxCPrestamoDrMaestroBase cuota, decimal monto) => cuota.InsUpdCargoMonto(TiposCargosPrestamo.Capital, monto);

    //    internal static void SetInteresCapitalMonto(this CxCPrestamoDrMaestroBase cuota, decimal monto) => cuota.InsUpdCargoMonto(TiposCargosPrestamo.InteresCapital, monto);

    //    internal static void SetOtrosCargosMonto(this CxCPrestamoDrMaestroBase cuota, decimal monto) => cuota.InsUpdCargoMonto(TiposCargosPrestamo.OtrosCargos, monto);

    //    internal static void SetInteresOtrosCargosMonto(this CxCPrestamoDrMaestroBase cuota, decimal monto) => cuota.InsUpdCargoMonto(TiposCargosPrestamo.InteresOtrosCargos, monto);

    //    internal static void SetGastoDeCierreMonto(this CxCPrestamoDrMaestroBase cuota, decimal monto) => cuota.InsUpdCargoMonto(TiposCargosPrestamo.GastoDeCierre, monto);

    //    internal static void SetInteresGastoDeCierreMonto(this CxCPrestamoDrMaestroBase cuota, decimal monto) => cuota.InsUpdCargoMonto(TiposCargosPrestamo.InteresGastoDeCierre, monto);

    //    internal static void SetInteresDespuesDeVencidoMonto(this CxCPrestamoDrMaestroBase cuota, decimal monto) => cuota.InsUpdCargoMonto(TiposCargosPrestamo.InteresDespuesDeVencido, monto);

    //    internal static void SetCargosPorAtrasoMonto(this CxCPrestamoDrMaestroBase cuota, decimal monto) => cuota.InsUpdCargoMonto(TiposCargosPrestamo.CargoPorAtraso, monto);


    //    public static Decimal TotalCapitalMonto(this IEnumerable<CxCPrestamoDrMaestroBase> items) => items.Select(item => item.CapitalMonto).Sum();

    //    //TotalMontoOriginalPorTipoCargo(cuotas, TiposCargosPrestamo.Capital);

    //    public static Decimal TotalInteresMonto(this IEnumerable<CxCPrestamoDrMaestroBase> cuotas) => TotalMontoOriginalPorTipoCargo(cuotas, TiposCargosPrestamo.InteresCapital);

    //    public static Decimal TotalMontoOriginalPorTipoCargo(this IEnumerable<CxCPrestamoDrMaestroBase> cuotas, TiposCargosPrestamo tipoCargo)
    //    {
    //        var total = 0m;
    //        foreach (var itemCuota in cuotas)
    //        {
    //            var result = itemCuota.ItemsDrCxC.Where(itemDetalle => itemDetalle.TipoCargo == tipoCargo).Sum(data => data.Monto);
    //            total += result;
    //        }
    //        return total;
    //    }



    //    public static Decimal TotalBcePorTipoCargo(this IEnumerable<CxCPrestamoDrMaestroBase> cuotas, TiposCargosPrestamo tipoCargo)
    //    {
    //        var total = 0m;
    //        foreach (var itemCuota in cuotas)
    //        {
    //            var result = itemCuota.ItemsDrCxC.Where(itemDetalle => itemDetalle.TipoCargo == tipoCargo).Sum(data => data.Monto);
    //            total += result;
    //        }
    //        return total;
    //    }
    //}

}
