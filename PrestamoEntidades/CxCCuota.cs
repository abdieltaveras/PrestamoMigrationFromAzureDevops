﻿using DevBox.Core.DAL.SQLServer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoEntidades
{
    //todo eliminar warw objeto y usar el DebitoViewModel
    // <summary>
    // la representacion de la cuota como va en la tabla
    // </summary>
    public abstract class CxCCuotaBase
    {
        public string NumeroTransaccion { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal Capital { get; set; } = 0;
        public decimal Interes { get; set; } = 0;
        public decimal GastoDeCierre { get; set; } = 0;
        public decimal InteresDelGastoDeCierre { get; set; } = 0;
        public decimal InteresDespuesDeVencido { get; set; } = 0;
        public decimal OtrosCargos { get; set; } = 0;
        public decimal InteresOtrosCargos { get; set; } = 0;
        
        public List<IDetalleDebitoCxC> DetallesOtrosCargos { get; set; }
        public bool Vencida(DateTime fecha) => this.Fecha.CompareTo(fecha) < 0;
        public bool MenorOIgualALaFecha(DateTime fecha) => this.Fecha.CompareTo(fecha) <= 0;

        //[IgnoreOnParams]
        //public string Comentario { get; set; } = String.Empty;
        public override string ToString() => DetallesCuotaText.ToString();


        private string DetallesCuotaText => $"Valores Originales Cuota No {NumeroTransaccion} Fecha {Fecha} Total {TotalOrig} Capital {Capital} Interes {Interes} G/Credito {GastoDeCierre} Int G/Credito {InteresDelGastoDeCierre} Otros Cargos {OtrosCargos} Interes otros cargos {InteresOtrosCargos} ";

        public decimal TotalOrig
        {
            get
            {
                var valor = (Capital) + (Interes) + (GastoDeCierre) + (InteresDelGastoDeCierre);
                //var valor2 = Capital + Interes  + +GastoDeCierre  + InteresDelGastoDeCierre + OtrosCargos  + InteresOtrosCargos ;
                return valor;
            }
        }
    }
    public class CxCCuota : CxCCuotaBase, ICxCDebitoPrestamo
    {
        //public string FechaSt => Fecha.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

        
        //public virtual decimal BceGeneral => BceCapital + BceInteres + BceGastoDeCierre + BceInteresDelGastoDeCierre; // + BceOtrosCargos??0 + BceInteresOtrosCargos??0;
        //public decimal BceCapital { get; internal set; } = 0;
        //public decimal BceInteres { get; internal set; } = 0;
        //public decimal BceGastoDeCierre { get; internal set; } = 0;
        //public decimal BceInteresDelGastoDeCierre { get; internal set; } = 0;

        //[IgnoreOnParams]
        //public DateTime? UltActFechaMora { get; set; } = InitValues._19000101;
        //[IgnoreOnParams]
        //public DateTime? UltActFechaInteres { get; set; } = InitValues._19000101;

        
        //public string BalanceToString()
        //{
        //    return $"Balancess Cuota No {Numero} Fecha {Fecha} Balance {BceGeneral} Capital {BceCapital} Interes {BceInteres} G/C {BceGastoDeCierre} Int G/C {BceInteresDelGastoDeCierre}";
        //}
    }
}