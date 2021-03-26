using emtSoft.DAL;
using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace PrestamoBLL.Entidades
{
    public static class Utils
    {
        public static TDerived ToDerived<TBase, TDerived>(TBase tBase)
        where TDerived : TBase, new()
        {
            TDerived tDerived = new TDerived();
            foreach (PropertyInfo propBase in typeof(TBase).GetProperties())
            {
                PropertyInfo propDerived = typeof(TDerived).GetProperty(propBase.Name);
                propDerived.SetValue(tDerived, propBase.GetValue(tBase, null), null);
            }
            return tDerived;
        }

    }
    /// <summary>
    /// Extension methods to convert string to json or xml and viseversa
    /// </summary>
    //public static class ExtMethJsonAndXml
    //{
    //    public enum ConversionType { ToJsonFormat, ToXmlFormat };
    //    /// <summary>
    //    /// Convert/Deserialize the string in json format to the specified type
    //    /// </summary>
    //    /// <typeparam name="T"> the type name </typeparam>
    //    /// <param name="value"> the string in json</param>
    //    /// <param name="convType"> option to convert to json o xml vy defaul is json</param>/// 
    //    /// <returns></returns>

    //    public static T ToType<T>(this string value, ConversionType convType = ConversionType.ToJsonFormat)
    //    {
    //        var objConversion = default(T);
    //        if (convType == ConversionType.ToJsonFormat)
    //        {
    //            objConversion = JsonConvert.DeserializeObject<T>(value);
    //            //return objConversion;
    //        }
    //        if (convType == ConversionType.ToXmlFormat)
    //        {
    //            try
    //            {
    //                var stringReader = new System.IO.StringReader(value);
    //                var serializer = new XmlSerializer(typeof(T));
    //                objConversion = (T)serializer.Deserialize(stringReader);
    //            }
    //            catch
    //            {
    //                throw new Exception("no pude convertir el string en formato xml al tipo solicitado");
    //            }
    //        }
    //        return objConversion;
    //    }
    //    /// <summary>
    //    /// Convert the type value submited to a string json representation
    //    /// </summary>
    //    /// <typeparam name="T">the type name examples Cliente, Direccion, etc</typeparam>
    //    /// <param name="value"> the instance o the type</param>
    //    /// <returns></returns>

    //    public static string ToJson<T>(this T value)
    //    {
    //        var jsonConversion = JsonConvert.SerializeObject(value);
    //        return jsonConversion;
    //    }
    //    public static string ToXml<T>(this T value)
    //    {
    //        try
    //        {
    //            var stringwriter = new System.IO.StringWriter();
    //            var serializer = new XmlSerializer(typeof(T));
    //            serializer.Serialize(stringwriter, value);
    //            return stringwriter.ToString();
    //        }
    //        catch
    //        {
    //            throw new FormatException("el objeto string no corresponde al formato xml adecuado favor revisar");
    //        }
    //    }

    //}
    ///// <summary>
    ///// extgension methods for strings
    ///// </summary>
    //public static class StringMeth
    //{
    //    public static string ConvertNullStringToEmpty(this string value)
    //    {
    //        value = string.IsNullOrEmpty(value) ? string.Empty : value;
    //        return value;
    //    }
    //    public static string RemoveAllButNumber(this string texto)
    //    {
    //        var onlyNumbers = string.Empty;
    //        if (!string.IsNullOrEmpty(texto))
    //        {
    //            onlyNumbers = new string(texto.Where(c => char.IsNumber(c)).ToArray());
    //        }
    //        return onlyNumbers;
    //    }

    //}

    //public static class DataReaderMeth
    //{
    //    public static bool HasColumn(this IDataRecord r, string columnName)
    //    {
    //        try
    //        {
    //            return r.GetOrdinal(columnName) >= 0;
    //        }
    //        catch (IndexOutOfRangeException)
    //        {
    //            return false;
    //        }
    //    }

    //    public static void SetValue(this PropertyInfo pi, IDataReader dr)
    //    {
    //        if (pi.CanWrite)
    //        {
    //            string propName = pi.Name;
    //            var propValue = dr[propName];
    //            try
    //            {

    //                if (!propValue.IsNull())
    //                {
    //                    pi.SetFieldValue(propName, propValue);
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                pi.SetFieldValue(propName, null);
    //            }
    //        }
    //    }
    //    public static @type DataReaderToType<@type>(this IDataReader dr, out @type obj) where @type : class, new()
    //    {
    //        obj = new @type();
    //        foreach (var pi in obj.GetType().GetProperties())
    //        {
    //            if (pi.CanWrite)
    //            {
    //                string propName = pi.Name;
    //                if (dr.HasColumn(propName))
    //                {
    //                    var propValue = dr[propName];
    //                    pi.SetValue(obj, Convert.ChangeType(propValue, pi.PropertyType), null);
    //                }
    //            }
    //        }
    //        return obj;
    //    }
    //}

    //public static class _type
    //{
    //    public static void CopyPropertiesTo<T, TU>(this T source, TU dest)
    //    {
    //        var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
    //        var destProps = typeof(TU).GetProperties()
    //                .Where(x => x.CanWrite)
    //                .ToList();

    //        foreach (var sourceProp in sourceProps)
    //        {
    //            if (destProps.Any(x => x.Name == sourceProp.Name))
    //            {
    //                var p = destProps.First(x => x.Name == sourceProp.Name);
    //                if (p.CanWrite)
    //                { // check if the property can be set or no.
    //                    p.SetValue(dest, sourceProp.GetValue(source, null), null);
    //                }
    //            }

    //        }

    //    }
    //}

    //public static class EnumUtils
    //{
    //    public static string getItemName(this Enum _enum, int itemIndex)
    //    {
    //        //Enum.GetName(typeof(TiposIdentificacionCliente), (TiposIdentificacionCliente)IdTipoIdentificacion);
    //        Type genericEnumType = _enum.GetType();
    //        var result = genericEnumType.GetEnumNames()[itemIndex];
    //        return result;
    //    }
    //}
    //public static class SLFactory
    //{
    //    /// <summary>
    //    /// Permite obtr
    //    /// </summary>
    //    /// <typeparam name="T"></typeparam>
    //    /// <returns></returns>
    //    private static IEnumerable<SelectListItem> GetEnumSelectList<T>()
    //    {
    //        return (Enum.GetValues(typeof(T)).Cast<int>().Select(e => new SelectListItem()
    //        {
    //            Text = Enum.GetName(typeof(T), e).Replace("_", " "),
    //            Value = e.ToString(),
    //        })).ToList();
    //    }
    //    public static SelectList ForEnum<T>() => new SelectList(GetEnumSelectList<T>(), "Value", "Text");
    //    //public static SelectList ForEnumAddingStartingValue<T>() => new SelectList(GetEnumSelectListAddingFirstValue<T>("Elija"), "Value", "Text");
    //}

}

