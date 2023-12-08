using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DevBox.Core.Classes.Utils
{
    public static class DalExtensions
    {
        public static Dictionary<string, int> GetAllNames(this IDataRecord record)
        {
            var result = new Dictionary<string, int>();
            for (int i = 0; i < record.FieldCount; i++)
            {
                result.Add(record.GetName(i).ToLower(), i);
            }
            return result;
        }        
        public static bool IsNull<T>(this T source)
        {
            return (source == null) || (source is DBNull);
        }
        public static T DeserializeFromXml<T>(string xml)
        {
            T result;
            var ser = new XmlSerializer(typeof(T));
            using (var tr = new StringReader(xml))
            {
                result = (T)ser.Deserialize(tr);
            }
            return result;
        }
        public static XElement SerializeToXml<T>(T obj)
        {
            string result;
            XmlSerializer Serializer = new XmlSerializer(typeof(T));
            using (StringWriter stringWriter = new StringWriter())
            {
                Serializer.Serialize(stringWriter, obj);
                result = stringWriter.ToString();
                stringWriter.Close();
            }
            return XElement.Parse(result);
        }
        public static object GetFieldValue<T>(this T obj, string FieldName)
        {
            Type t = obj?.GetType();
            PropertyInfo[] properties = t?.GetProperties();
            if (properties!=null)
            {
                foreach (PropertyInfo pi in properties)
                {
                    if (pi.Name.Equals(FieldName))
                    {
                        return pi.GetValue(obj, null);
                    }
                }
            }            
            return null;
        }
        public static void SetFieldValue<T>(this T obj, string FieldName, object value)
        {
            Type t = obj.GetType();
            PropertyInfo[] properties = t.GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                if (pi.Name.Equals(FieldName))
                {
                    if (pi.CanWrite)
                    {
                        try
                        {
                            pi.SetValue(obj, value, null);
                        }
                        catch (Exception e)
                        {
                            pi.SetValue(obj, null, null);
                        }
                    }
                }

            }
        }
        public static DataTable ToDataTable<@Type>(this IEnumerable<@Type> list, Dictionary<string, Func<object, object>> convertMap = null, List<string> skipList = null)
        {
            DataTable result = new DataTable();
            PropertyInfo[] properties = typeof(@Type).GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                if (skipList != null)
                {
                    if (skipList.Contains(pi.Name))
                    {
                        continue;
                    }
                }
                var newCol = new DataColumn(pi.Name);
                result.Columns.Add(newCol);
            }
            foreach (var item in list)
            {
                var newRow = result.NewRow();
                foreach (PropertyInfo pi in properties)
                {
                    if (skipList != null)
                    {
                        if (skipList.Contains(pi.Name))
                        {
                            continue;
                        }
                    }
                    if (convertMap != null)
                    {
                        if (convertMap.ContainsKey(pi.Name))
                        {
                            newRow[pi.Name] = convertMap[pi.Name](pi.GetValue(item, null));
                            continue;
                        }
                    }
                    newRow[pi.Name] = pi.GetValue(item, null);
                }
                result.Rows.Add(newRow);
            }
            return result;
        }
        public static List<@Type> ToList<@Type>(this IDataReader dr, Dictionary<string, Func<@Type, object>> convertMap = null) where @Type : class
        {
            var result = new List<@Type>();
            PropertyInfo[] properties = typeof(@Type).GetProperties();
            var hasDefaultConstructor = (typeof(@Type).GetConstructor(System.Type.EmptyTypes) != null);
            while (dr.Read())
            {
                var obj = (!hasDefaultConstructor) ? default(@Type) : (@Type)Activator.CreateInstance(typeof(@Type));
                if (!hasDefaultConstructor)
                {
                    obj = ((dr[0] == null) || (dr[0] == DBNull.Value)) ? default(@Type) : (@Type)Convert.ChangeType(dr[0], typeof(@Type));
                }
                else
                {
                    var dataFields = dr.GetAllNames();
                    foreach (PropertyInfo pi in properties)
                    {
                        if (pi.CanWrite)
                        {
                            string propName = pi.Name;
                            if (!dataFields.ContainsKey(propName.ToLower())) { continue; }
                            var propValue = dr[propName];
                            try
                            {
                                if (!convertMap.IsNull())
                                {
                                    if (convertMap.ContainsKey(propName))
                                    {
                                        continue;
                                    }
                                }
                                if (!propValue.IsNull())
                                {
                                    var propertyType = pi.PropertyType;
                                    if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                                    {
                                        propertyType = propertyType.GetGenericArguments()[0];
                                    }
                                    pi.SetValue(obj, Convert.ChangeType(propValue, propertyType), null);
                                }
                                else
                                {
                                    bool canBeNull = !pi.PropertyType.IsValueType || (Nullable.GetUnderlyingType(pi.PropertyType) != null);
                                    if (canBeNull)
                                    {
                                        pi.SetValue(obj, null, null);
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                pi.SetValue(obj, null, null);
                            }
                        }
                    }
                }
                if (convertMap != null)
                {
                    foreach (var entry in convertMap)
                    {
                        var campo = entry.Key;
                        var objValue = convertMap[campo](obj);
                        obj.SetFieldValue(campo, objValue);
                    }
                }
                result.Add(obj);
            }
            return result;
        }
        public static void GetFromDataRow<@Type>(this @Type obj, DataRow dr)
        {
            PropertyInfo[] properties = typeof(@Type).GetProperties();
            List<string> cols = new List<string>();
            foreach (DataColumn col in dr.Table.Columns)
            {
                cols.Add(col.ColumnName.ToUpper());
            }
            foreach (PropertyInfo pi in properties)
            {
                if (cols.Contains(pi.Name.ToUpper()))
                {
                    if (pi.CanWrite)
                    {
                        try
                        {
                            pi.SetValue(obj, dr[pi.Name], null);
                        }
                        catch
                        {
                            pi.SetValue(obj, null, null);
                        }
                    }
                }

            }
        }
    }
}
