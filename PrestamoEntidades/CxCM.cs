using DevBox.Core.DAL.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    public enum TiposCargosPrestamo { Capital, Interes, InteresDespuesDeVencido, GastoDeCierre, InteresGastoDeCierre, OtrosCargos, InteresOtrosCargos, CargoPorAtraso }
    public enum TiposDrCr { Dr, Cr };
    public enum TiposTransaccionesCxCPrestamo { Cuota, Pago, NotaDeDebito, NotaDeCredito }
    public abstract class CxCPrestamoMaestroBase 
    {
        public virtual int IdCxCM { get; set; }
        public virtual int IdPrestamo { get; set; } = 0;
        /// <summary>
        /// Para especificar si es un debito o un credito
        /// </summary>
        public virtual TiposDrCr TipoDrCr { get; protected set; }

        /// <summary>
        /// para indicar el tipo de transaccion si es Cuota, NotaDeDebito, etc
        /// </summary>
        public virtual TiposTransaccionesCxCPrestamo TipoTransaccionCxC { get; protected set; }
        
        //public virtual string NoTransaccion
        //{
        //    get; internal set;
        //}

        public virtual string NoTransaccionManual { get; set; } = string.Empty;
        public virtual DateTime Fecha { get; set; } = InitValues._19000101;
        public virtual Decimal Monto { get; internal set; } = 0;

        public virtual Decimal Balance { get; private set; } = 0;

        public virtual string Detalle { get; set; } = string.Empty;

        private List<CxCPrestamoItemBase> ItemsCxC { get; set; } = new List<CxCPrestamoItemBase>();

        protected void InsUpdItemCxC(CxCPrestamoItemBase item)
        {
            // busquemos si ya previamente existe
            var itemInserted = this.ItemsCxC.Where(current => current.TipoCargo == item.TipoCargo).FirstOrDefault();

            if (itemInserted == null)
            {
                item.Balance = item.Monto;
                this.ItemsCxC.Add(item);
                this.Monto += item.Monto;
                this.Balance += item.Monto;
            }
            else
            {
                
                var diferencia = itemInserted.Monto - item.Monto;
                itemInserted.Monto = item.Monto;
                itemInserted.Balance = item.Monto;
                this.Monto += diferencia;
                this.Balance = this.Monto;
            }
        }
        //public void Add(IEnumerable<CxCPrestamoDetalle> items)
        //{
        //    items.ToList().ForEach(item => this.AddDetalle(item));
        //}
        //public void Remove(CxCPrestamoDetalle item)
        //{
        //    this.ItemsCxC.Remove(item);
        //    this.Monto -= item.Monto;
        //    this.Balance -= item.Balance;
        //}
        //public void Remove(IEnumerable<CxCPrestamoDetalle> items)
        //{
        //    items.ToList().ForEach(item => this.Remove(item));
        //}
        public List<CxCPrestamoItemBase> GetItems => this.ItemsCxC;

        public override string ToString()
        {
            return $" Transaccion {this.TipoTransaccionCxC} Tipo Contable {this.TipoDrCr}   {this.Monto} Items {this.GetItems.Count}";
        }
    }

    public abstract class CxCPrestamoItemBase : IEquatable<CxCPrestamoItemBase>
    {
        public int IdCxCD { get; internal set; }

        public int IdCxCM { get; internal set; }
        
        [IgnoreOnParams]
        public string IdGuid { get; private set; }

        public string Cuenta { get; internal set; }
        public Decimal Monto { get; internal set; }

        public TiposCargosPrestamo TipoCargo { get; internal set; }
        public Decimal Balance { get; internal  set; }


        public CxCPrestamoItemBase()
        {
            IdGuid = Guid.NewGuid().ToString();
        }
        public bool Equals(CxCPrestamoItemBase other) => other == null ? false : this.IdCxCD.Equals(other.IdCxCD) && this.IdGuid.Equals(other.IdGuid);

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var cxcDObj = obj as CxCPrestamoItemBase;
            if (cxcDObj == null)
            {
                return false;
            }
            else
            {
                return Equals(cxcDObj);
            }
        }

        
        public override int GetHashCode()
        {
            return HashCode.Combine(this.IdCxCD, this.IdGuid); 
        }

        public override string ToString()
        {
            return $"{TipoCargo} Monto Original {Monto} Balance {Balance}";
        }

        
    }


    public enum ResultadosOperacionesCxC { Exitosa, Fallida }

    public class CuotaPrestamoMaestro : CxCPrestamoMaestroBase
    {
        public int NumeroCuota { get; protected set; }
        List<CuotaPrestamoMaestro> CuotasPrestamo = new List<CuotaPrestamoMaestro>();

        public ResultadosOperacionesCxC ResultadoOperacion { get; private set; } = ResultadosOperacionesCxC.Fallida;

        public CuotaPrestamoMaestro(int numeroCuota, int idPrestamo, DateTime Fecha)
        {
            this.IdPrestamo = idPrestamo;
            this.NumeroCuota = numeroCuota;
            this.Fecha = Fecha;
            this.TipoTransaccionCxC = TiposTransaccionesCxCPrestamo.Cuota;
        
            this.TipoDrCr = TiposDrCr.Dr;
        }

        /// <summary>
        /// Agrega el cargo debe ser una cuota nueva
        /// </summary>
        /// <param name="tipoCargo"></param>
        /// <param name="monto"></param>

        public void InsUpdCargoMonto(TiposCargosPrestamo tipoCargo, Decimal monto)
        {
            if (monto <= 0)
            {
                return;
            }
            else
            {
                var detalleCargoCuota = new CuotaPrestamoDetalle(tipoCargo, monto);
                this.InsUpdItemCxC(detalleCargoCuota);
            }
        }

    }

    public class CuotaPrestamoDetalle : CxCPrestamoItemBase
    {
        public CuotaPrestamoDetalle(TiposCargosPrestamo tipoCargoPrestamo, Decimal monto): base()
        {
            this.TipoCargo = tipoCargoPrestamo;
            this.Monto = monto;
        }

    }
    

    public static class ExtMetCuotaPrestamo
    {
        private static decimal GetMonto(CxCPrestamoItemBase cxCPrestamoDetalle) => cxCPrestamoDetalle == null ? 0 : cxCPrestamoDetalle.Monto;

        public static Decimal GetCapitalMontoOriginal(this CuotaPrestamoMaestro cuota) => GetMonto(cuota.GetItems.Where(cta => cta.TipoCargo == TiposCargosPrestamo.Capital).FirstOrDefault());
        public static Decimal GetInteresMontoOriginal(this CuotaPrestamoMaestro cuota) => GetMonto(cuota.GetItems.Where(cta => cta.TipoCargo == TiposCargosPrestamo.Interes).FirstOrDefault());

        public static Decimal GetOtrosCargosMontoOriginal(this CuotaPrestamoMaestro cuota) => GetMonto(cuota.GetItems.Where(cta => cta.TipoCargo == TiposCargosPrestamo.OtrosCargos).FirstOrDefault());

        public static Decimal GetGastoDeCierreMontoOriginal(this CuotaPrestamoMaestro cuota) => GetMonto(cuota.GetItems.Where(cta => cta.TipoCargo == TiposCargosPrestamo.GastoDeCierre).FirstOrDefault());

        public static Decimal GetInteresGastoDeCierreMontoOriginal(this CuotaPrestamoMaestro cuota) => GetMonto( cuota.GetItems.Where(cta => cta.TipoCargo == TiposCargosPrestamo.InteresGastoDeCierre).FirstOrDefault());

        public static Decimal GetInteresOtrosCargosMontoOriginal(this CuotaPrestamoMaestro cuota) => GetMonto(cuota.GetItems.Where(cta => cta.TipoCargo == TiposCargosPrestamo.InteresOtrosCargos).FirstOrDefault());

        public static Decimal GetInteresDespuesDeVencidoMontoOriginal(this CuotaPrestamoMaestro cuota) => GetMonto(cuota.GetItems.Where(cta => cta.TipoCargo == TiposCargosPrestamo.InteresDespuesDeVencido).FirstOrDefault());



        public static void SetCapitalMontoOriginal(this CuotaPrestamoMaestro cuota, decimal monto) => cuota.InsUpdCargoMonto(TiposCargosPrestamo.Capital, monto);

        public static void SetInteresMontoOriginal(this CuotaPrestamoMaestro cuota, decimal monto) => cuota.InsUpdCargoMonto(TiposCargosPrestamo.Interes, monto);

        public static void SetOtrosCargosMontoOriginal(this CuotaPrestamoMaestro cuota, decimal monto) => cuota.InsUpdCargoMonto(TiposCargosPrestamo.OtrosCargos, monto);

        public static void SetInteresOtrosCargosMontoOriginal(this CuotaPrestamoMaestro cuota, decimal monto) => cuota.InsUpdCargoMonto(TiposCargosPrestamo.InteresOtrosCargos, monto);

        public static void SetGastoDeCierreMontoOriginal(this CuotaPrestamoMaestro cuota, decimal monto) => cuota.InsUpdCargoMonto(TiposCargosPrestamo.GastoDeCierre, monto);

        public static void SetInteresGastoDeCierreMontoOriginal(this CuotaPrestamoMaestro cuota, decimal monto) => cuota.InsUpdCargoMonto(TiposCargosPrestamo.InteresGastoDeCierre, monto);

        public static void SetInteresDespuesDeVencidoMontoOriginal(this CuotaPrestamoMaestro cuota, decimal monto) => cuota.InsUpdCargoMonto(TiposCargosPrestamo.InteresDespuesDeVencido, monto);



        public static Decimal TotalMontoOriginalPorTipoCargo(this IEnumerable<CuotaPrestamoMaestro> cuotas, TiposCargosPrestamo tipoCargo)
        {
            var total = 0m;
            foreach (var itemCuota in cuotas)
            {
                var result = itemCuota.GetItems.Where(itemDetalle => itemDetalle.TipoCargo == tipoCargo).Sum(data => data.Monto);
                total += result;
            }
            return total;
        }

        public static Decimal TotalBcePorTipoCargo(this IEnumerable<CuotaPrestamoMaestro> cuotas, TiposCargosPrestamo tipoCargo)
        {
            var total = 0m;
            foreach (var itemCuota in cuotas)
            {
                var result = itemCuota.GetItems.Where(itemDetalle => itemDetalle.TipoCargo == tipoCargo).Sum(data => data.Monto);
                total += result;
            }
            return total;
        }
    }

}
