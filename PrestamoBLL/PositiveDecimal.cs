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

        public static implicit operator PositiveDecimal(decimal value) => new PositiveDecimal(value);

        public override string ToString() => PositiveValue.ToString();
    }
}
