﻿using Newtonsoft.Json;
using System;
using System.Linq;
using System.Xml.Serialization;

namespace PrestamoBLL.Entidades
{
    /// <summary>
    /// Extension methods to convert string to json or xml and viseversa
    /// </summary>
    public static class ExtMethJsonAndXml
    {
        public enum ConversionType { ToJsonFormat, ToXmlFormat };
        /// <summary>
        /// Convert/Deserialize the string in json format to the specified type
        /// </summary>
        /// <typeparam name="T"> the type name </typeparam>
        /// <param name="value"> the string in json</param>
        /// <param name="convType"> option to convert to json o xml vy defaul is json</param>/// 
        /// <returns></returns>

        public static T ToType<T>(this string value, ConversionType convType = ConversionType.ToJsonFormat)
        {
            var objConversion = default(T);
            if (convType == ConversionType.ToJsonFormat)
            {
                objConversion = JsonConvert.DeserializeObject<T>(value);
                //return objConversion;
            }
            if (convType == ConversionType.ToXmlFormat)
            {
                try
                {
                    var stringReader = new System.IO.StringReader(value);
                    var serializer = new XmlSerializer(typeof(T));
                    objConversion = (T)serializer.Deserialize(stringReader);
                }
                catch
                {
                    throw new Exception("no pude convertir el string en formato xml al tipo solicitado");
                }
            }
            return objConversion;
        }
        /// <summary>
        /// Convert the type value submited to a string json representation
        /// </summary>
        /// <typeparam name="T">the type name examples Cliente, Direccion, etc</typeparam>
        /// <param name="value"> the instance o the type</param>
        /// <returns></returns>

        public static string ToJson<T>(this T value)
        {
            var jsonConversion = JsonConvert.SerializeObject(value);
            return jsonConversion;
        }
        public static string ToXml<T>(this T value)
        {
            try
            {
                var stringwriter = new System.IO.StringWriter();
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stringwriter, value);
                return stringwriter.ToString();
            }
            catch
            {
                throw new FormatException("el objeto string no corresponde al formato xml adecuado favor revisar");
            }
        }

    }
    /// <summary>
    /// extgension methods for strings
    /// </summary>
    public static class StringMeth
    {
        public static string ConvertNullStringToEmpty(this string value)
        {
            value = string.IsNullOrEmpty(value) ? string.Empty : value;
            return value;
        }
        public static string RemoveAllButNumber(this string texto)
        {
            var onlyNumbers = string.Empty;
            if (!string.IsNullOrEmpty(texto))
            {
                onlyNumbers = new string(texto.Where(c => char.IsNumber(c)).ToArray());
            }
            return onlyNumbers;
        }

    }

    public static class _type
    {
        public static void CopyPropertiesTo<T, TU>(this T source, TU dest)
        {
            var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
            var destProps = typeof(TU).GetProperties()
                    .Where(x => x.CanWrite)
                    .ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    if (p.CanWrite)
                    { // check if the property can be set or no.
                        p.SetValue(dest, sourceProp.GetValue(source, null), null);
                    }
                }

            }

        }
    }

}
