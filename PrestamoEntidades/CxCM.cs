using DevBox.Core.DAL.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{


    public enum TiposDrCr { Dr, Cr };

    public enum TiposTranDrCr {Pago, Debito, Credito } 
    public class CxCM : BaseInsUpd
    {
        public virtual int IdCxCM { get; set; }
        public virtual int IdPrestamo { get; set; } = 0;
        public virtual TiposDrCr TipoDrCr { get; set; }

        [IgnoreOnParams]
        public virtual string TipoSt => TipoDrCr.ToString();

        public virtual TiposTranDrCr TipoTranDrCr { get; set; }
        public virtual string NoTransaccion
        {
            get; set;
        } 

        public virtual string NoTransaccionManual { get; set; } = string.Empty;
        public virtual DateTime Fecha { get; set; } = InitValues._19000101;
        public virtual Decimal Monto { get; private set; } = 0;

        public virtual Decimal Balance { get; private set; } = 0;

        public virtual string Detalle { get; set; } = string.Empty;

        private List<CxCD> ItemsCxC { get; set; } = new List<CxCD>();
        public void Add(CxCD item)
        {
            this.ItemsCxC.Add(item);
            item.Balance = item.Monto;
            this.Monto += item.Monto;
            this.Balance += item.Monto;
        }
        public void Add(IEnumerable<CxCD> items)
        {
            items.ToList().ForEach(item => this.Add(item));
        }
        public void Remove(CxCD item)
        {
            this.ItemsCxC.Remove(item);
            this.Monto -= item.Monto;
            this.Balance -= item.Balance;
        }
        public void Remove(IEnumerable<CxCD> items)
        {
            items.ToList().ForEach(item => this.Remove(item));
        }
        public List<CxCD> GetItems => this.ItemsCxC;
    }

    public class CxCD : IEquatable<CxCD>
    {
        public int IdCxCD { get; set; }

        public int IdCxCM { get; set; }

        public string Cuenta { get; set; }
        public Decimal Monto { get; set; }
        public Decimal Balance { get; set; }

        public bool Equals(CxCD other) => other == null ? false : this.IdCxCD == other.IdCxCD;

        public override bool Equals(object obj)
        {

            
            if (obj == null)
            {
                return false;
            }

            CxCD cxcDObj = obj as CxCD;
            if (cxcDObj == null)
            {
                return false;
            }
            else
            {
                return this.Equals(cxcDObj);
            }
        }
    }
}
