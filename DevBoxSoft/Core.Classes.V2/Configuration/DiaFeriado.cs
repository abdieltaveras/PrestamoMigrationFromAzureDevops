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
        /// <summary>
        /// Devuelve la fecha del año en la que cae la Pascua de Resurrección. Se utiliza para ello el Algoritmo de Butcher 
        /// </summary>
        /// <param name="anno">Año a calcular</param>
        /// <returns></returns>
        public static DateTime DomingoResurreccion(int anno)
        {
            int M = 24; int N = 5; int APRIL = 4; int MARCH = 3;
            if (anno >= 1583 && anno <= 1699)
            {
                M = 22;
                N = 2;
            }
            else if (anno >= 1700 && anno <= 1799)
            {
                M = 23;
                N = 3;
            }
            else if (anno >= 1800 && anno <= 1899)
            {
                M = 23;
                N = 4;
            }
            else if (anno >= 1900 && anno <= 2099)
            {
                M = 24;
                N = 5;
            }
            else if (anno >= 2100 && anno <= 2199)
            {
                M = 24;
                N = 6;
            }
            else if (anno >= 2200 && anno <= 2299)
            {
                M = 25;
                N = 0;
            }

            int a, b, c, d, e, dia, mes;

            //Cálculo de residuos
            a = anno % 19;
            b = anno % 4;
            c = anno % 7;
            d = (19 * a + M) % 30;
            e = (2 * b + 4 * c + 6 * d + N) % 7;

            // Decidir entre los 2 casos:
            if (d + e < 10)
            {
                dia = d + e + 22;
                mes = MARCH;
            }
            else
            {
                dia = d + e - 9;
                mes = APRIL;
            }

            // Excepciones especiales (según artículo)
            if (dia == 26 && mes == APRIL)
                dia = 19;
            if (dia == 25 && mes == APRIL && d == 28 && e == 6 && a > 10)
                dia = 18;

            var result = new DateTime(anno, mes, dia);
            return result;
        }
        public static DateTime ViernesSanto(int anio)
        {
            var result = DomingoResurreccion(anio).AddDays(-2);
            return result;
        }
        public static List<DiaFeriado> GetDefaultDiasRD(int ano)
        {
            var vs = ViernesSanto(ano); var ss = vs.AddDays(1);
            var cc = vs.AddDays(62);
            var result = new List<DiaFeriado>(){
                                                new DiaFeriado(){Dia=new DateTime(ano, 01, 01), Descripcion="Día de Año Nuevo"},
                                                new DiaFeriado(){Dia=new DateTime(ano, 01, 06), Descripcion="Día de los Santos Reyes"},
                                                new DiaFeriado(){Dia=new DateTime(ano, 01, 21), Descripcion="Día de Nuestra Señora de La Altagracia"},
                                                new DiaFeriado(){Dia=new DateTime(ano, 01, 26), Descripcion="Día de nacimiento de Juan Pablo Duarte"},
                                                new DiaFeriado(){Dia=new DateTime(ano, 02, 27), Descripcion="Aniversario de la Independencia Nacional"},
                                                new DiaFeriado(){Dia=vs, Descripcion="Viernes Santo"},
                                                new DiaFeriado(){Dia=ss, Descripcion="Sábado Santo"},
                                                new DiaFeriado(){Dia=new DateTime(ano, 05, 01), Descripcion="Día Internacional del Trabajo"},
                                                new DiaFeriado(){Dia=cc, Descripcion="Corpus Christi"},
                                                new DiaFeriado(){Dia=new DateTime(ano, 08, 16), Descripcion="Aniversario de la Restauración del país"},
                                                new DiaFeriado(){Dia=new DateTime(ano, 09, 24), Descripcion="Día de Nuestra Señora de Las Mercedes"},
                                                new DiaFeriado(){Dia=new DateTime(ano, 11, 06), Descripcion="Día de la Constitución"},
                                                new DiaFeriado(){Dia=new DateTime(ano, 12, 24), Descripcion="Nochebuena. Laborable hasta el mediodía."},
                                                new DiaFeriado(){Dia=new DateTime(ano, 12, 25), Descripcion="Nacimiento de Jesús, Navidad"},
                                                new DiaFeriado(){Dia=new DateTime(ano, 12, 31), Descripcion="Nochevieja. Laborable hasta el mediodía."}
                                                };
            return result;
        }
    }
    public class DiaFeriadoNullable : DiaFeriado
    {
        new public DateTime? Dia { get; set; }
    }
}
