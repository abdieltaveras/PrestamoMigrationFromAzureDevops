using System;

namespace PrestamoBLL
{
    [System.Diagnostics.DebuggerDisplay("{PositiveValue}")]
    public struct PositiveDecimal // : IComparable, IFormattable, etc...
    {
        private decimal PositiveValue;
        private PositiveDecimal(decimal value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException("value", "Value cannot be less than zero");

            PositiveValue = value;
        }

        // permitir manejo de decimales, lo toma por el parametro
        public static implicit operator PositiveDecimal(decimal value) => new PositiveDecimal(value);

        public static implicit operator decimal(PositiveDecimal value) => new PositiveDecimal(value); //value.PositiveValue;
        public override string ToString() => PositiveValue.ToString();
        
    }
}
