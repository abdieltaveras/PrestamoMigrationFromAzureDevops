using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DevBox.Core.Classes.Utils;

namespace DevBox.Core.DAL.SQLServer
{
    public class Database
    {
        private static readonly object mutex = new object();

        private string connStr { get; set; }
        [ThreadStatic]
        static private volatile Database _DataServer = null;
        static bool USE_AZURE_DB => Convert.ToBoolean(ConfigurationManager.AppSettings["USE_AZURE_DB"] ?? "false");

        static public Database DataServer
        {
            get
            {
                if (_DataServer == null)
                {
                    lock (mutex)
                    {
                        if (_DataServer == null)
                        {
                            string connStr = "DataServer";
                            connStr = GetConnectionString(connStr);
                            _DataServer = new Database(connStr);
                        }
                    }

                }

                return _DataServer;
            }
        }

        private static string GetConnectionString(string connStr)
        {
            try
            {
                connStr = ConfigurationManager.ConnectionStrings[connStr].ConnectionString;
            }
            catch (Exception)
            {
                ThowErrorIfNullConnstr(connStr);
            }

            return connStr;
        }

        private static void ThowErrorIfNullConnstr(string connStr)
        {
            if (connStr == null)
            {
                throw new NullReferenceException($"La cadena de conexion {connStr} no pudo ser encontrada o extraida");
            }
        }

        static public Database AdHoc(string server)
        {
            //string connStr = ConfigurationManager.ConnectionStrings[server].ConnectionString;
            string connStr = GetConnectionString(server);
            return new Database(connStr);
        }
        private Database(string connStr)
        {
            this.connStr = connStr;
        }

        public DataSet ExecSelSPDS(string SProc, SearchRec search)
        {
            return ExecSelSPDS(SProc, search.ToSqlParams());
        }
        public DataSet ExecSelSPDS(string SProc, params SqlParameter[] Params)
        {
            using (SqlConnection conne = new SqlConnection(connStr))
            {
                SqlCommand comm = new SqlCommand(SProc, conne);
                comm.CommandType = CommandType.StoredProcedure;
                if (Params != null)
                {
                    comm.Parameters.AddRange(Params);
                }
                SqlDataAdapter da = new SqlDataAdapter(comm);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        public List<@type> ExecQuery<@type>(string Query, params SqlParameter[] Params) where @type : class
        {
            using (SqlConnection conne = new SqlConnection(connStr))
            {
                conne.Open();
                var comm = new SqlCommand(Query, conne);
                if (Params != null)
                {
                    comm.Parameters.AddRange(Params);
                }
                comm.Prepare();
                var dr = comm.ExecuteReader(CommandBehavior.CloseConnection);
                var result = dr.ToList<@type>();
                return result;
            }
        }
        public DataTable ExecSelSP<@type>(string SProc, ref @type paramObj)
        {
            SqlParameter[] Params = SearchRec.ToSqlParams(paramObj);
            DataSet ds = ExecSelSPDS(SProc, Params);
            foreach (var para in Params)
            {
                if (para.Direction == ParameterDirection.Output)
                {
                    paramObj.SetFieldValue(para.ParameterName.Replace("@", ""), para.Value);
                }
            }
            return (ds.Tables.Count > 0) ? ds.Tables[0] : null;
        }
        delegate void ExecSPAsyncDel(string Text);
        public void ExecuteSelSPAsync(string sp, IAsyncResult result, params SqlParameter[] Params)
        {
            using (SqlConnection conne = new SqlConnection(connStr))
            {
                SqlCommand comm = new SqlCommand(sp, conne);
                comm.CommandType = CommandType.StoredProcedure;
                if (Params != null)
                {
                    comm.Parameters.AddRange(Params);
                }
            }
        }
        public DataTable ExecSelSP(string SProc, SearchRec search)
        {
            return ExecSelSP(SProc, search.ToSqlParams());
        }
        public DataTable ExecSelSP(string SProc, params SqlParameter[] Params)
        {
            DataSet ds = ExecSelSPDS(SProc, Params);
            return (ds.Tables.Count > 0) ? ds.Tables[0] : null;
        }
        public List<@type> ExecReaderSelSP<@type>(string SProc, Dictionary<string, Func<@type, object>> convertMap, params SqlParameter[] Params) where @type : class
        {
            using (var result = ExecReaderSelSP(SProc, Params))
            {
                return result.ToList(convertMap);
            }
        }
        public List<@type> ExecReaderSelSP<@type>(string SProc, params SqlParameter[] Params) where @type : class
        {
            using (var result = ExecReaderSelSP(SProc, Params))
            {
                return result.ToList<@type>();
            }
        }
        public void BulkInsert(string tableName, DataTable data, int timeOut = 120, int batchSize = 1000)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                // Open the connection to SQL Data Warehouse
                connection.Open();
                // Create a Bulk Copy class
                var bulkCopy = new SqlBulkCopy(connection);
                bulkCopy.BatchSize = batchSize;
                // Define the target table
                bulkCopy.DestinationTableName = tableName;
                // Set the timeout.
                bulkCopy.BulkCopyTimeout = timeOut;
                // Write the rows to the table
                bulkCopy.WriteToServer(data);
            }
        }
        public SqlDataReader ExecReaderSelSP(string SProc, params SqlParameter[] Params)
        {
            SqlConnection conne = new SqlConnection(connStr);
            SqlCommand comm = new SqlCommand(SProc, conne);
            comm.CommandType = CommandType.StoredProcedure;
            if (Params != null)
            {
                comm.Parameters.AddRange(Params);
            }
            conne.Open();
            return comm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public int ExecNonQuerySP(string SProc, params SqlParameter[] Params)
        {
            using (SqlConnection conne = new SqlConnection(connStr))
            {
                SqlCommand comm = new SqlCommand(SProc, conne);
                comm.CommandType = CommandType.StoredProcedure;
                if (Params != null)
                {
                    comm.Parameters.AddRange(Params);
                }
                conne.Open();
                return comm.ExecuteNonQuery();
            }
        }

        public object ExecEscalarCommand(string command)
        {
            using (SqlConnection conne = new SqlConnection(connStr))
            {
                SqlCommand comm = new SqlCommand(command, conne);
                comm.CommandType = CommandType.Text;
                conne.Open();
                return comm.ExecuteScalar();
            }
        }
    }

    public class SearchRec
    {
        private Dictionary<string, object> parameters;
        public SearchRec()
        {
            this.parameters = new Dictionary<string, object>();
        }
        public void AddParam(string name, object value)
        {
            this.parameters.Add(name, value);
        }
        public void AddParam(string name, object value, bool ignoreIfNull)
        {
            if (ignoreIfNull)
            {
                if (value == null)
                {
                    return;
                }
            }
            this.parameters.Add(name, value);
        }
        public void AddParam(string name, object value, bool ignoreIfNull, bool encriptar)
        {
            if (ignoreIfNull)
            {
                if (value == null)
                {
                    return;
                }
            }
            this.parameters.Add(name, value);
        }
        public override string ToString()
        {
            string result = string.Empty;
            List<string> fields = this.parameters.Keys.ToList<string>();
            foreach (string f in fields)
            {
                string value = Convert.ToString(this.parameters[f]).Trim();
                if (value != string.Empty)
                {
                    result = string.Concat(result, "@", f, " ='", value, "', ");
                }
            }
            result = result.Remove(result.LastIndexOf(","));
            return result;
        }
        public SqlParameter[] ToSqlParams()
        {
            List<SqlParameter> result = new List<SqlParameter>();
            foreach (var item in this.parameters)
            {
                if (item.Value == null)
                {
                    continue;
                }
                string campo = string.Concat("@", item.Key);
                object Value = item.Value;
                if (Value is System.Collections.ICollection)// tipo.GetProperty(info.Name).GetType() is IEnumerable)
                {
                    Value = getIEnumerableValues(Value as IEnumerable);
                }
                if (item.Value.GetType().Equals(typeof(DateTime)))
                {
                    bool badSqlDt = (Convert.ToDateTime(Value) < SqlDateTime.MinValue.Value)
                                    ||
                                 (Convert.ToDateTime(Value) > SqlDateTime.MaxValue.Value);
                    if (badSqlDt)
                    {
                        continue;
                    }
                }
                var p = new SqlParameter(campo, Value);
                if (item.Value.GetType().Equals(typeof(DataTable)))
                {
                    p.SqlDbType = SqlDbType.Structured;
                }
                result.Add(p);
            }
            return result.ToArray();
        }
        static public SqlParameter[] ToSqlParams(object obj)
        {
            List<SqlParameter> result = new List<SqlParameter>();

            Type tipo = obj.GetType();
            PropertyInfo[] infoCampos = tipo.GetProperties();
            foreach (var info in infoCampos)
            {
                bool ignorar = false;
                Attribute[] atributos = Attribute.GetCustomAttributes(tipo.GetProperty(info.Name));
                foreach (var att in atributos)
                {
                    if (att is IgnoreOnParams)
                    {
                        if ((att as IgnoreOnParams).Ignorar)
                        {
                            ignorar = true;
                            break;
                        }
                    }
                }
                if (ignorar)
                {
                    continue;
                }
                string campo = string.Concat("@", info.Name);
                var valor = tipo.GetProperty(info.Name).GetValue(obj, null);
                if (valor == null)
                {
                    continue;
                }
                if (info.PropertyType is System.Collections.ICollection)// tipo.GetProperty(info.Name).GetType() is IEnumerable)
                {
                    valor = getIEnumerableValues(valor as IEnumerable);
                }
                if (info.PropertyType.Equals(typeof(DateTime)))
                {
                    bool badSqlDt = (Convert.ToDateTime(valor) < SqlDateTime.MinValue.Value)
                                    ||
                                 (Convert.ToDateTime(valor) > SqlDateTime.MaxValue.Value);
                    if (badSqlDt)
                    {
                        continue;
                    }
                }
                var newParam = new SqlParameter(campo, valor);
                result.Add(newParam);
            }
            return result.ToArray();
        }

        private static string getIEnumerableValues(IEnumerable valor)
        {
            StringBuilder result = new StringBuilder();
            foreach (var item in valor)
            {
                result.Append(item.ToString());
                result.Append('█');
            }
            if (result.Length > 0) { result.Length--; }
            return result.ToString();
        }

        public void AddParam(SqlParameter[] paramss)
        {
            foreach (var item in paramss)
            {
                string nm = item.ParameterName;
                nm = nm.StartsWith("@") ? nm.Substring(1) : nm;
                this.parameters.Add(nm, item.Value);
            }
        }
    }
}
