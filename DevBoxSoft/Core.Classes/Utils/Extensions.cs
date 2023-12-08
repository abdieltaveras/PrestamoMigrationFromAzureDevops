using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace DevBox.Core.Classes.Utils
{
    public static class Extensions
    {
        public static string Encrypt(this string str) => Encrypter.Encrypt(str);
        public static string Decrypt(this string str) => str.IsEmpty() ? "" : Encrypter.Decrypt(str);
        public static string ToJSON(this object obj) => JsonConvert.SerializeObject(obj);
        public static Dictionary<string, object> ToDictionary(this object obj)
        {
            var json = obj.ToJSON();
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            return dictionary;
        }
        public static bool InSet<@T>(this @T value, params @T[] set) => set.ToList().Contains(value);
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer = null)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            return _(); IEnumerable<TSource> _()
            {
                var knownKeys = new HashSet<TKey>(comparer);
                foreach (var element in source)
                {
                    if (knownKeys.Add(keySelector(element)))
                        yield return element;
                }
            }
        }
        public static bool IsEmpty(this string str) => string.IsNullOrEmpty(str);
        public static string ToCSV<T>(this IEnumerable<T> enumerable, Func<T, string> fnStr, string separator = ",")
        {
            var list = enumerable.Select(e => fnStr(e));
            var result = list.Aggregate("", (current, next) => $"{current}{separator}{next}");
            return result;
        }
        public static bool Between(this DateTime? dt, DateTime? from, DateTime? to)
        {
            if ((dt != null && dt.HasValue) && from.HasValue & to.HasValue)
            {
                return Between(dt.Value, from.Value, to.Value);
            }
            return false;
        }
        public static bool Between(this DateTime? dt, DateTime from, DateTime to)
        {
            return (dt != null && dt.HasValue) ? Between(dt.Value, from, to) : false;
        }
        public static bool Between(this DateTime dt, DateTime from, DateTime to)
        {
            return dt >= from && dt <= to;
        }
        public static bool Between(this decimal n, decimal from, decimal to)
        {
            return n >= from && n <= to;
        }
        public static bool Between(this int n, int from, int to)
        {
            return n >= from && n <= to;
        }
        public static bool Between(this decimal? n, decimal from, decimal to)
        {
            return (n != null && n.HasValue) ? Between(n.Value, from, to) : false;
        }
        public static string UrlEncode(this object obj)
        {
            if (obj == null) { return ""; }
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }
        public static @type[] Join<@type>(this @type[] array1, @type[] array2)
        {
            @type[] newArray = new @type[array1.Length + array2.Length];
            Array.Copy(array1, newArray, array1.Length);
            Array.Copy(array2, 0, newArray, array1.Length, array2.Length);
            return newArray;
        }
        public static DateTime PrincipioQuincena(this DateTime dt)
        {
            var day = (dt.Day < 16) ? 1 : 16;
            return new DateTime(dt.Year, dt.Month, day, 0, 0, 0);
        }
        public static DateTime FinQuincena(this DateTime dt)
        {
            var day = (dt.Day < 16) ? 15 : dt.MonthEnd().Day;
            return new DateTime(dt.Year, dt.Month, day, 0, 0, 0);
        }
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
        public static void InsertOrReplace<T>(this IList<T> _list, T Item)
        {
            var idx = _list.IndexOf(Item);
            if (idx < 0)
            {
                _list.Add(Item);
            }
            else
            {
                _list[idx] = Item;
            }
        }
        public static void ProcessMatches(this string text, string pattern, Action<int, string> onMatch)
        {
            var matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);
            foreach (Match match in matches)
            {
                foreach (Group group in match.Groups)
                {
                    onMatch(group.Index, group.Value);
                }
            }
        }
        public static string SafeSubstring(this string input, int startIndex, int length)
        {
            var result = ((startIndex + length) > input.SafeLenght()) ? "" : input.Substring(startIndex, length);
            return result;
        }
        public static int SafeLenght(this string input)
        {
            return (input ?? "").Length;
        }
        public static string SafeTrim(this string input)
        {
            return (input ?? "").Trim();
        }
        public static bool IsNullOrEmpty(this string input)
        {
            return string.IsNullOrEmpty(input);
        }
        public static bool IsADate(this string input, CultureInfo culture = null)
        {
            if (input.IsNullOrEmpty()) { return false; }
            if (input.Length < 5) { return false; }
            if (culture == null) { culture = new CultureInfo("en-US"); }
            var result = false;
            DateTime holder;
            result = DateTime.TryParseExact(input, "MM/dd/yyyy", culture, DateTimeStyles.None, out holder);
            return result;
        }
        public static bool IsMatch(this string input, string pattern)
        {
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            var result = rgx.IsMatch(input);
            return result;
        }
        public static string ReplaceByPattern(this string input, string pattern, string replacement)
        {
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            string result = rgx.Replace(input, replacement);
            return result;
        }
        public static bool AllItemsAreEqual<T>(this IEnumerable<T> list1, IEnumerable<T> list2, IEqualityComparer<T> comparer = null)
        {
            var cnt = (comparer == null) ? new Dictionary<T, int>() : new Dictionary<T, int>(comparer);
            foreach (T s in list1)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]++;
                }
                else
                {
                    cnt.Add(s, 1);
                }
            }
            foreach (T s in list2)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]--;
                }
                else
                {
                    return false;
                }
            }
            return cnt.Values.All(c => c == 0);
        }
        public static string ToTitleCase(this string @str)
        {
            if (string.IsNullOrEmpty(@str)) { return @str; }
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(@str);
        }
        public static bool IsNullOrDBNull(this object @obj)
        {
            return ((@obj == null) || (@obj == DBNull.Value));
        }
        public static T GetSingleItem<T>(this IEnumerable<T> @enum, Predicate<T> pred)
        {
            return (@enum != null) ? @enum.Where(i => pred(i)).SingleOrDefault() : default(T);
        }

        public static T GetFirstOrDefaultItem<T>(this IEnumerable<T> @enum, Predicate<T> pred)
        {
            return (@enum != null) ? @enum.Where(i => pred(i)).FirstOrDefault() : default(T);
        }

        public static T GetFirstItem<T>(this IEnumerable<T> @enum, Predicate<T> pred)
        {
            return (@enum != null) ? @enum.Where(i => pred(i)).FirstOrDefault() : default(T);
        }

        public static IEnumerable<T> GetAllExcept<T>(this IEnumerable<T> @enum, params T[] unwantedItems)
        {
            return (@enum != null) ? @enum.Except(unwantedItems) : null;
        }

        public static int inSet(this Enum value, params Enum[] set)
        {
            return Array.IndexOf(set, value);
        }

        public static string StripPunctuation(this string s)
        {
            var sb = new StringBuilder();
            foreach (char c in s)
            {
                if (!char.IsPunctuation(c))
                    sb.Append(c);
            }
            return sb.ToString();
        }
        public static int ToInteger(this double num, bool absolute)
        {
            return absolute ? (int)Math.Abs(Math.Truncate(num)) : (int)Math.Truncate(num);
        }
        public static int ToInteger(this double num)
        {
            return (int)(Math.Truncate(num));
        }

        public static bool isInThePast(this DateTime date, bool ignoreTime = false)
        {
            return ignoreTime ? (DateTime.Now.Date.CompareTo(date.Date) > 0)
                              : (DateTime.Now.CompareTo(date) > 0);
        }
        public static bool isInTheFuture(this DateTime date, bool ignoreTime = false)
        {
            return ignoreTime ? (DateTime.Now.Date.CompareTo(date.Date) < 0)
                              : (DateTime.Now.CompareTo(date) < 0);
        }
        public static string DateDiff(this DateTime date, string lang = "sp")
        {
            var currDt = DateTime.Now;
            var diff = currDt.Subtract(date);
            string result = date.ToShortDateString();

            var futureDt = (currDt.CompareTo(date) < 0);
            var spanish = (lang == "sp");

            var timePrefix = spanish ? (futureDt ? "En" : "Hace") : (futureDt ? "In" : "");
            var timeSuffix = spanish ? (futureDt ? "" : "") : (futureDt ? "" : "ago");
            if (diff.TotalMinutes.ToInteger(true) <= 0)
            {
                result = spanish ? string.Format("{1} {0} segundos {2}", diff.TotalSeconds.ToInteger(true), timePrefix, timeSuffix)
                               :
                               string.Format("{1} {0} seconds {2}", diff.TotalSeconds.ToInteger(true), timePrefix, timeSuffix);
            }
            else
            {
                if (diff.TotalHours.ToInteger(true) <= 0)
                {
                    result = spanish ? string.Format("{1} {0}  minutos {2}", diff.TotalMinutes.ToInteger(true), timePrefix, timeSuffix)
                                  :
                                    string.Format("{1} {0}  minutes {2}", diff.TotalMinutes.ToInteger(true), timePrefix, timeSuffix);
                }
                else
                {
                    if (diff.TotalDays.ToInteger(true) <= 0)
                    {
                        result = spanish ? string.Format("{1} {0}  horas {2}", diff.TotalHours.ToInteger(true), timePrefix, timeSuffix)
                                      :
                                        string.Format("{1} {0}  hours {2}", diff.TotalHours.ToInteger(true), timePrefix, timeSuffix);
                    }
                    else
                    {
                        if (diff.TotalDays.ToInteger(true) == 1)
                        {
                            result = spanish ? futureDt ? "Mañana" : "Ayer"
                                         : futureDt ? "Tomorrow" : "Yesterday";
                        }

                        else
                        {
                            if (diff.TotalDays.ToInteger(true) <= 365)
                            {
                                result = spanish ? string.Format("{1} {0} dias {2}", diff.TotalDays.ToInteger(true), timePrefix, timeSuffix)
                                              : string.Format("{1} {0} days {2}", diff.TotalDays.ToInteger(true), timePrefix, timeSuffix);
                            }
                            else
                            {
                                var years = (diff.TotalDays.ToInteger(true) % 365);
                                result = spanish ? string.Format("{1} {0} años {2}", years, timePrefix, timeSuffix)
                                              : string.Format("{1} {0} years {2}", years, timePrefix, timeSuffix);
                            }
                        }
                    }
                }
            }
            return result.Trim();
        }
        public static List<string> ToStringList(this IEnumerable list)
        {
            List<string> result = new List<string>();
            foreach (var element in list)
            {
                result.Add(element.ToString());
            }
            return result;
        }
        public static DateTime LastWorkingDay(this DateTime dt)
        {
            dt = dt.AddDays(-1);
            while ((dt.DayOfWeek == DayOfWeek.Saturday) || (dt.DayOfWeek == DayOfWeek.Sunday))
            {
                dt = dt.AddDays(-1);
            }
            return dt;
        }
        public static DateTime SetHora(this DateTime dt, string hora)
        {
            dt = new DateTime(dt.Year, dt.Month, dt.Day, int.Parse(hora.Split(':')[0]), int.Parse(hora.Split(':')[1]), 00);
            return dt;
        }
        static public DateTime ParseTime(string hora)
        {
            return new DateTime(1900, 01, 01, int.Parse(hora.Split(':')[0]), int.Parse(hora.Split(':')[1]), 00);
        }
        public static DateTime GetNext(this DateTime fromDt, DayOfWeek dow)
        {
            while (fromDt.DayOfWeek != dow)
            {
                fromDt = fromDt.AddDays(1);
            }
            return fromDt;
        }
        public static DateTime MonthEnd(this DateTime date)
        {
            DateTime result = new DateTime(date.Year, date.Month, 01);
            result = result.AddMonths(1).Subtract(new TimeSpan(0, 0, 1));
            return result;
        }
        public static DateTime MonthBegin(this DateTime date)
        {
            DateTime result = new DateTime(date.Year, date.Month, 01);
            return result;
        }
        public static DateTime NextMonday(this DateTime date)
        {
            DateTime result = date.AddDays(1);
            while (result.DayOfWeek != DayOfWeek.Monday)
            {
                result = result.AddDays(1);
            }
            return result;
        }
        public static DateTime LastMonday(this DateTime date)
        {
            DateTime result = date.AddDays(-1);
            while (result.DayOfWeek != DayOfWeek.Monday)
            {
                result = result.AddDays(-1);
            }
            return result;
        }
        /// <summary>
        /// Last minute and last second of the day
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime EndOfDay(this DateTime date)
        {
            return date.Date.AddDays(1).AddSeconds(-1);
        }
        /// <summary>
        /// Number of week on the year
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int WeekNumber(this DateTime date)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            return cal.GetWeekOfYear(date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
        }
        public delegate void Func<TArg0>(TArg0 element);

        /// <summary>
        /// Executes an Update statement block on all elements in an IEnumerable T sequence.
        /// </summary>
        /// <typeparam name="TSource">The source element type.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="update">The update statement to execute for each element.</param>
        /// <returns>The numer of records affected.</returns>
        public static int Update<TSource>(this IEnumerable<TSource> source, Func<TSource> update)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (update == null) throw new ArgumentNullException("update");
            if (typeof(TSource).IsValueType)
            {
                throw new NotSupportedException("value type elements are not supported by update.");
            }
            int count = 0;
            foreach (TSource element in source)
            {
                update(element);
                count++;
            }
            return count;
        }
        public static string ConcatToStr<TSource>(this IEnumerable<TSource> list, string sep)
        {
            StringBuilder result = new StringBuilder();
            int n = 0;
            foreach (var item in list)
            {
                result.Append(item.ToString());
                if (list.Count() > 1)
                {
                    result.Append(sep);
                }
                n++;
            }
            return result.ToString();
        }
        public static bool ContainsX(this string str, string value)
        {
            return CultureInfo.CurrentCulture.CompareInfo.IndexOf(str, value, CompareOptions.IgnoreCase
                                                                            | CompareOptions.IgnoreNonSpace) > -1;
        }
        public static string ToCSV(this IDictionary<string, string> dicc)
        {
            var result = new StringBuilder();
            foreach (var kv in dicc)
            {
                result.Append($"{kv.Key}:{kv.Value}"); result.Append(",");
            }
            result.Remove(result.Length - 1, 1);
            return result.ToString();
        }
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }
        public static string ToCSV(this IEnumerable<string> strs, string separator = ",")
        {
            var result = new StringBuilder();
            foreach (var s in strs)
            {
                result.Append(s); result.Append(separator);
            }
            if (result.Length > 0)
            {
                result.Remove(result.Length - 1, 1);
            }
            return result.ToString();
        }
        public static bool ContainsAll(this string str, params string[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (!str.ContainsX(values[i].Trim()))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool ContainsAny(this string str, params string[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (str.ContainsX(values[i].Trim()))
                {
                    return true;
                }
            }
            return false;
        }
        public static string RemoveTilde(this string s)
        {
            return s.Replace('á', 'a')
                  .Replace('é', 'e')
                  .Replace('í', 'i')
                  .Replace('ó', 'o')
                  .Replace('ú', 'u')
                  .Replace('Á', 'A')
                  .Replace('É', 'E')
                  .Replace('Í', 'I')
                  .Replace('Ó', 'O')
                  .Replace('Ú', 'U');
        }
        /// <summary>
        /// Retorna el numero en letras
        /// </summary>
        /// <param name="value"></param>
        /// <param name="currency">Verdadero cuando quiere la palabra "pesos" al final</param>
        /// <returns></returns>
        public static string ToLetras(this double value, bool currency)
        {
            return NumeroLetras(Convert.ToDecimal(value), currency);
        }
        /// <summary>
        /// Retorna el numero en letras
        /// </summary>
        /// <param name="value"></param>
        /// <param name="currency">Verdadero cuando quiere la palabra "pesos" al final</param>
        /// <returns></returns>
        public static string ToLetras(this int value, bool currency)
        {
            return NumeroLetras(Convert.ToDecimal(value), currency);
        }
        /// <summary>
        /// Retorna el numero en letras
        /// </summary>
        /// <param name="value"></param>
        /// <param name="currency">Verdadero cuando quiere la palabra "pesos" al final</param>
        /// <returns></returns>
        public static string ToLetras(this decimal value, bool currency)
        {
            return NumeroLetras(value, currency);
        }
        private static string NumeroLetras(decimal value, bool currency)
        {
            string Num2Text = "";
            value = Math.Abs(value);
            decimal decimales = Math.Round(value - Math.Truncate(value), 2);
            while (decimales != Math.Truncate(decimales))
            {
                decimales = decimales * 10;
            }
            value = Math.Truncate(value);

            if (value == 0) Num2Text = "CERO";

            else if (value == 1) Num2Text = "UNO";

            else if (value == 2) Num2Text = "DOS";

            else if (value == 3) Num2Text = "TRES";

            else if (value == 4) Num2Text = "CUATRO";

            else if (value == 5) Num2Text = "CINCO";

            else if (value == 6) Num2Text = "SEIS";

            else if (value == 7) Num2Text = "SIETE";

            else if (value == 8) Num2Text = "OCHO";

            else if (value == 9) Num2Text = "NUEVE";

            else if (value == 10) Num2Text = "DIEZ";

            else if (value == 11) Num2Text = "ONCE";

            else if (value == 12) Num2Text = "DOCE";

            else if (value == 13) Num2Text = "TRECE";

            else if (value == 14) Num2Text = "CATORCE";

            else if (value == 15) Num2Text = "QUINCE";

            else if (value < 20) Num2Text = "DIECI" + NumeroLetras(value - 10, false);

            else if (value == 20) Num2Text = "VEINTE";

            else if (value < 30) Num2Text = "VEINTI" + NumeroLetras(value - 20, false);

            else if (value == 30) Num2Text = "TREINTA";

            else if (value == 40) Num2Text = "CUARENTA";

            else if (value == 50) Num2Text = "CINCUENTA";

            else if (value == 60) Num2Text = "SESENTA";

            else if (value == 70) Num2Text = "SETENTA";

            else if (value == 80) Num2Text = "OCHENTA";

            else if (value == 90) Num2Text = "NOVENTA";

            else if (value < 100) Num2Text = NumeroLetras(Math.Truncate(value / 10) * 10, false) + " Y " + NumeroLetras(value % 10, false);

            else if (value == 100) Num2Text = "CIEN";

            else if (value < 200) Num2Text = "CIENTO " + NumeroLetras(value - 100, false);

            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = NumeroLetras(Math.Truncate(value / 100), false) + "CIENTOS";

            else if (value == 500) Num2Text = "QUINIENTOS";

            else if (value == 700) Num2Text = "SETECIENTOS";

            else if (value == 900) Num2Text = "NOVECIENTOS";

            else if (value < 1000) Num2Text = NumeroLetras(Math.Truncate(value / 100) * 100, false) + " " + NumeroLetras(value % 100, false);

            else if (value == 1000) Num2Text = "MIL";

            else if (value < 2000) Num2Text = "MIL " + NumeroLetras(value % 1000, false);

            else if (value < 1000000)
            {

                Num2Text = NumeroLetras(Math.Truncate(value / 1000), false) + " MIL";

                if ((value % 1000) > 0) Num2Text = Num2Text + " " + NumeroLetras(value % 1000, false);

            }

            else if (value == 1000000) Num2Text = "UN MILLON";

            else if (value < 2000000) Num2Text = "UN MILLON " + NumeroLetras(value % 1000000, false);

            else if (value < 1000000000000)
            {

                Num2Text = NumeroLetras(Math.Truncate(value / 1000000), false) + " MILLONES ";

                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + NumeroLetras(value - Math.Truncate(value / 1000000) * 1000000, false);

            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";

            else if (value < 2000000000000) Num2Text = "UN BILLON " + NumeroLetras(value - Math.Truncate(value / 1000000000000) * 1000000000000, false);

            else
            {

                Num2Text = NumeroLetras(Math.Truncate(value / 1000000000000), false) + " BILLONES";

                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + NumeroLetras(value - Math.Truncate(value / 1000000000000) * 1000000000000, false);

            }
            if (currency)
            {
                Num2Text = string.Concat(Num2Text, " Pesos");
            }
            if (decimales > 0)
            {
                string decStr = NumeroLetras(decimales, false);
                Num2Text = string.Concat(Num2Text, " con ", decStr, currency ? " ctvs." : "");
            }
            return Num2Text;

        }
        public static T Clone<T>(this T source)
        {
            if (Object.ReferenceEquals(source, null) == true)
                return default(T);

            T copy;
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));

            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, source);
                ms.Position = 0;
                copy = (T)serializer.ReadObject(ms);
            }

            return copy;
        }
        static GregorianCalendar _gc = new GregorianCalendar();
        public static int GetWeekOfMonth(this DateTime time)
        {
            DateTime first = new DateTime(time.Year, time.Month, 1);
            return time.GetWeekOfYear() - first.GetWeekOfYear() + 1;
        }

        static int GetWeekOfYear(this DateTime time)
        {
            return _gc.GetWeekOfYear(time, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }
    }
}
