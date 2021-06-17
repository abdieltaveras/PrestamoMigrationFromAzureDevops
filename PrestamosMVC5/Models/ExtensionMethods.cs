using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestamosMVC5.Models
{
    public static class ExtensionMethods
    {
        public static string IndexChecked(this int value, int index)
        {
            var result = index == value ? "checked" : string.Empty;
            return result;
        }
    }
}