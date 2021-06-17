using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PcpUtilidades
{
    public static class ExtMethJ
    {
        public enum ConversionType { ToJsonFormat, ToXmlFormat };
        /// <summary>
        /// Convert/Deserialize the string in json format to the specified type
        /// </summary>
        /// <typeparam name="T"> the type name </typeparam>
        /// <param name="value"> the string in json</param>
        /// <param name="convType"> option to convert to json o xml vy defaul is json</param>/// 
        /// <returns></returns>

        public static T ToType<T>(this string value, ConversionType convType = ConversionType.ToJsonFormat) where T : new()
        {
            var objConversion = new T();
            if (value == null || value.ToString() == string.Empty) return objConversion;
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
}
