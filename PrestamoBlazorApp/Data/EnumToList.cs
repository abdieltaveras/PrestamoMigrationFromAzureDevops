using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Data
{

    public class EnumModel
    {
        public int Value { get; set; }

        public string Text { get; set; }
    }

    public static class EnumToList
    {

        public static List<EnumModel> GetEnumTiposIdentificacionPersona()
        {
            var result = ((TiposIdentificacionPersona[])Enum.GetValues(typeof(TiposIdentificacionPersona))).Select(c => new EnumModel() { Value = (int)c, Text = c.ToString() }).ToList();
            return result;
        }

        public static List<EnumModel> GetEnumTiposReferencias()
        {
            var result = ((EnumTiposReferencia[])Enum.GetValues(typeof(EnumTiposReferencia))).Select(c => new EnumModel() { Value = (int)c, Text = c.ToString() }).ToList();
            return result;
        }
        public static List<EnumModel> GetEnumEstadosCiviles()
        {
            var result = ((EstadosCiviles[])Enum.GetValues(typeof(EstadosCiviles))).Select(c => new EnumModel() { Value = (int)c, Text = c.ToString() }).ToList();
            return result;
        }
    }

}
