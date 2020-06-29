using PrestamoBLL;
using System;

namespace PrestamosMVC5.Models
{
    public class TestMask
    {
        public TestMask()
        {
        }

        public string Text { get; set; }
        public string TextAsNumber { get; set; }

        public string TextAsNumber2 { get; set; }
        [Min(minValue: 1, ErrorMessage ="no se acepta valores menor a 1")]
        public int Number { get; set; } = 1;
        public DateTime Fecha { get;  set; }
    }
}