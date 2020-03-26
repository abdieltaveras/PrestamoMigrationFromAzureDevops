using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
namespace PrestamosMVC5.SiteUtils
{
    public static class DateUtils
    {
        public static string Tommddyyyy(this DateTime value)
        {
            string dateString = value.ToString("d", CultureInfo.InvariantCulture);
            return dateString;
        }

    }
}