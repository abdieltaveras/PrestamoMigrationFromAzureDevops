using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using PrestamosMVC5.Models;
using PrestamosMVC5.Views.Reportes;
using System.Data;
using PrestamosMVC5.Controllers;
using emtSoft.DAL;
using System.Reflection;

namespace PrestamosMVC5.SiteUtils
{
    public class ReportViewerUtils
    {
        public static DataTable ToDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                tb.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        //public static ReportViewer ReporteCl(string ProcedureName, string PathApp, string TableName, object SqlParams,DataTable dt)
        public static ReportViewer ReporteCl(string PathApp, DataTable dt)
        {
            
            var connectionString = ConfigurationManager.ConnectionStrings["DataserverLuis2"].ConnectionString;
            SqlConnection SqlCon = new SqlConnection(connectionString);
            MyDataSet dts = new MyDataSet();
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(900);
            reportViewer.Height = Unit.Percentage(900);

            /*   Esto se utilizará cuando se quiera usar procedimientos almacenados directamente
             *   
                SqlCommand SqlCmd = new SqlCommand(ProcedureName,SqlCon);
                SqlCmd.CommandType = CommandType.StoredProcedure;
                var sqlParameters = SearchRec.ToSqlParams(SqlParams);
                foreach (var item in sqlParameters)
                {
                    SqlCmd.Parameters.AddWithValue(item.ParameterName, item.Value);
                }
                SqlDataAdapter SqlAda = new SqlDataAdapter(SqlCmd);
                SqlAda.Fill(dts, TableName);
            *
            */
            reportViewer.LocalReport.ReportPath = PathApp;
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

            return reportViewer;
        }
    }

}