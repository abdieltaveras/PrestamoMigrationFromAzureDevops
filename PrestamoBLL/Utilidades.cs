using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Drawing;
using emtSoft.DAL;
using Newtonsoft.Json;
using System.Reflection;
using System.Web.Mvc;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PrestamoBLL
{
    public static class Utils
    {
        private static readonly object PrestamoDB;

        public static int GetIdFromDataTable(DataTable objectContainingId)
        {
            return Convert.ToInt32(objectContainingId.Rows[0][0]);
        }
        public static string SaveFiles(string path, HttpPostedFileBase file, string fileName = "")
        {
            var files = new List<HttpPostedFileBase>();
            files.Add(file);
            var result = Utils.SaveFiles(path, files, fileName);
            return result.FirstOrDefault();

        }
        /// <summary>
        /// gets date from the sql server instance running
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDateFromSqlServer()
        {
            var result = BLLPrestamo.DBPrestamo.ExecQuery("select GetDate() as DT", "SysDate");
            var r = Convert.ToDateTime(result.Rows[0][0]);
            return r;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public static IEnumerable<string> SaveFiles(string path, IEnumerable<HttpPostedFileBase> files, string fileName = "")
        {
            List<string> fileNames = new List<string>();
            var index = 0;
            files.ToList().ForEach(
                    f =>
                    {
                        try
                        {
                            var filename = SaveFile(path, f, fileName);
                            fileNames.Add(filename);
                        }
                        catch (Exception e)
                        {
                            fileNames.Add($"Error {e.Message}");
                        }
                        index++;
                    }
                );
            return fileNames;
        }


        public static string SaveFile(string path, HttpPostedFileBase file, string fileName = "")
        {
            string _fileName = string.Empty;
            if (file == null) return _fileName;
            var extension = Path.GetExtension(file.FileName);
            if (string.IsNullOrEmpty(fileName))
            {
                _fileName = Guid.NewGuid().ToString() + extension;
            }
            else
            {
                _fileName = fileName + extension;
            }
            var fullpath = Path.Combine(path, _fileName);
            try
            {
                file.SaveAs(fullpath);
            }
            catch (Exception e)
            {
                throw e;
            }
            return _fileName;
        }

        /// <summary>
        /// Recibe un string64base y lo convierte en una imagen el string contiene el texto inicial "data:Image/jpeg;base64,"
        /// el metodo lo quita para crear la imagen de base64 string
        /// </summary>
        /// <param name="basestring"></param>
        /// <returns> devuelve un tuple donde item1 es el nombre del archivo y item2 es un objeto Image </returns>
        public static Tuple<string, Image> ConvertBase64ToImage(string basestring, string directorio, string nombreArchivoSinExtension)
        {
            string imageBase64 = Regex.Replace(basestring, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
            string extension = basestring.Substring(11, basestring.IndexOf(";")-11);
            //data:image/gif;base64,
            //this image is a single pixel (black)
            byte[] bytes = Convert.FromBase64String(imageBase64);
            Image image;
            var fullFileName = directorio+nombreArchivoSinExtension + "." + extension;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
                image.Save(fullFileName);
            }
            var result = new Tuple<string,Image>( nombreArchivoSinExtension + "." + extension, image);
            return result;
        }

        public static string ConvertFileToBase64(string fullFileName)
        {
            Byte[] bytes = File.ReadAllBytes(fullFileName);
            String file = Convert.ToBase64String(bytes);
            return "data:Image/jpeg;base64,"+file;
        }

        public static string SaveFile(string path, string base64Image)
        {
            string fileName = string.Empty;
            if (!string.IsNullOrEmpty(base64Image) && base64Image != Constant.NoImagen)
            {
                var extension = "jpeg";
                fileName = Guid.NewGuid().ToString() + "." + extension;
                var fullpath = Path.Combine(path, fileName);
                int lengthtoremove = "data:Image/jpeg;base64,".Length;
                string imgbasestring = base64Image.Remove(0, lengthtoremove);
                File.WriteAllBytes(fullpath, Convert.FromBase64String(imgbasestring));
                //var imagen = convertBase64ToImage(base64Image, fullpath);
            }
            return fileName;
        }

        /// <summary>
        /// define valores que se usaran como constantes para evitar errores de tipeados y otros
        /// </summary>

        public static class Constant
        {
            /// <summary>
            /// El valor para cuando se quiera establecer que no hay imagen 
            /// </summary>
            public static string NoImagen => "no imagen";
        }

        public static TDerived ToDerived<TBase, TDerived>(TBase tBase) where TDerived : TBase, new()
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

        public static T ToType<T>(this string value, ConversionType convType = ConversionType.ToJsonFormat) where T : new()
        {
            var objConversion = new T();
            if (value == null || value.ToString()==string.Empty) return objConversion;
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
    /// extension methods for strings
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

    public static class DataReaderMeth
    {
        public static bool HasColumn(this IDataRecord r, string columnName)
        {
            try
            {
                return r.GetOrdinal(columnName) >= 0;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        public static void SetValue(this PropertyInfo pi, IDataReader dr)
        {
            if (pi.CanWrite)
            {
                string propName = pi.Name;
                var propValue = dr[propName];
                try
                {

                    if (!propValue.IsNull())
                    {
                        pi.SetFieldValue(propName, propValue);
                    }
                }
                catch (Exception ex)
                {
                    pi.SetFieldValue(propName, null);
                }
            }
        }
        public static @type DataReaderToType<@type>(this IDataReader dr, out @type obj) where @type : class, new()
        {
            obj = new @type();
            foreach (var pi in obj.GetType().GetProperties())
            {
                if (pi.CanWrite)
                {
                    string propName = pi.Name;
                    if (dr.HasColumn(propName))
                    {
                        var propValue = dr[propName];
                        if (!propValue.IsNull())
                        {
                            pi.SetValue(obj, Convert.ChangeType(propValue, pi.PropertyType), null);
                        }
                    }
                }
            }
            return obj;
        }
    }

    public static class _type
    {
        public static void CopyPropertiesTo<TSource, Ttarget>(this TSource source, Ttarget dest)
        {
            var sourceProps = typeof(TSource).GetProperties().Where(x => x.CanRead).ToList();
            var targetProps = typeof(Ttarget).GetProperties()
                    .Where(x => x.CanWrite)
                    .ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (targetProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = targetProps.First(x => x.Name == sourceProp.Name);
                    if (p.CanWrite)
                    { // check if the property can be set or no.
                        p.SetValue(dest, sourceProp.GetValue(source, null), null);
                    }
                }

            }

        }
    }

    public static class EnumUtils
    {
        public static string getItemName(this Enum _enum, int itemIndex)
        {
            //Enum.GetName(typeof(TiposIdentificacionCliente), (TiposIdentificacionCliente)IdTipoIdentificacion);
            Type genericEnumType = _enum.GetType();
            var result = genericEnumType.GetEnumNames()[itemIndex];
            return result;
        }
    }
    public static class SLFactory
    {
        /// <summary>
        /// Permite obtr
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static IEnumerable<SelectListItem> GetEnumSelectList<T>()
        {
            return (Enum.GetValues(typeof(T)).Cast<int>().Select(e => new SelectListItem()
            {
                Text = Enum.GetName(typeof(T), e).Replace("_", " "),
                Value = e.ToString(),
            })).ToList();
        }
        public static SelectList ForEnum<T>() => new SelectList(GetEnumSelectList<T>(), "Value", "Text");
        //public static SelectList ForEnumAddingStartingValue<T>() => new SelectList(GetEnumSelectListAddingFirstValue<T>("Elija"), "Value", "Text");
    }

    public class MaxAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly int maxValue;

        public MaxAttribute(int maxValue)
        {
            this.maxValue = maxValue;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();

            rule.ErrorMessage = ErrorMessageString;
            //, maxValue;

            rule.ValidationType = "max";
            rule.ValidationParameters.Add("max", maxValue);
            yield return rule;
        }

        public override bool IsValid(object value)
        {
            return (int)value <= maxValue;
        }
    }
    public class MinAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly int minValue;

        public MinAttribute(int minValue)
        {
            this.minValue = minValue;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();

            rule.ErrorMessage = ErrorMessageString;
            //, maxValue;

            rule.ValidationType = "min";
            rule.ValidationParameters.Add("min", minValue);
            yield return rule;
        }

        public override bool IsValid(object value)
        {
            return (int)value <= minValue;
        }
    }



}
