using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBox.Core.Classes.Configuration
{
    public class DiaFeriado : IEquatable<DiaFeriado>, IEquatable<DateTime>, IComparable<DiaFeriado>, IComparable<DateTime>
    {
        public int idDiaFeriado { get; set; }
        public DateTime Dia { get; set; }
        public string Descripcion { get; set; }
        public override string ToString() => $"{Dia.ToShortDateString()} - {Descripcion}";

        public bool Equals(DiaFeriado other)
        {
            return Dia.Date.Equals(other.Dia.Date);
        }

        public bool Equals(DateTime other)
        {
            return Dia.Date.Equals(other);
        }

        public int CompareTo(DiaFeriado other)
        {
            return Dia.CompareTo(other.Dia);
        }

        public int CompareTo(DateTime other)
        {
            return Dia.CompareTo(other);
        }
    }
    public class DiaFeriadoNullable : DiaFeriado
    {
        new public DateTime? Dia { get; set; }
    }
}
