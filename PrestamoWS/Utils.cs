using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoWS
{
    public class Utils :Controller
    {
        //
        public FileContentResult CatalogoReportList<@Type, @Base>(IEnumerable<Type> DataInList, string ReportUrl,  int reportType = 1, IEnumerable<Base> DatosBase = null, string DataSetName = "DataSet1", Dictionary<string, string> parameter = null)
        {
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Stream sl = new FileStream(ReportUrl, FileMode.Open, FileAccess.Read);
            string path = ReportUrl;
            LocalReport localReport = new LocalReport();
            localReport.LoadReportDefinition(sl);

            if (parameter!=null)
            {
                foreach (var item in parameter)
                {
                    localReport.SetParameters(new[] { new ReportParameter(item.Key, item.Value) });
                }
            }
            
            if (DatosBase != null)
            {
                localReport.DataSources.Add(new ReportDataSource("Base", DatosBase));
            }

            if (DataInList == null)
            {
                localReport.DataSources.Add(new ReportDataSource(DataSetName, DataInList));
            }
            else
            {
                localReport.DataSources.Add(new ReportDataSource(DataSetName, DataInList));
            }

            if (reportType == 1)
            {
                int ext = (int)(DateTime.Now.Ticks >> 10);
                var result = localReport.Render("PDF");
                return File(result, "application/pdf");
            }
            else if (reportType == 2)
            {
                int ext = (int)(DateTime.Now.Ticks >> 10);
                var result = localReport.Render("EXCEL");
                return File(result, "application/vnd.ms-excel");
            }
            return null;
        }


        //public IActionResult CatalogoReportList<@Type,@Base>(IEnumerable<Type> Datos, string ReportUrl, int reportType = 1, IEnumerable<Base> DatosBase = null) 
        //{

        //    string mimeType = "";
        //    int extension = 1;
        //    string path = ReportUrl;
        //    Dictionary<string, string> parameter = new Dictionary<string, string>();
        //    //parameter.Add("param","RDLC REPORT");
        //    LocalReport localReport = new LocalReport(path);
        //    localReport.AddDataSource(dataSetName: "DataSet1", dataSource: Datos);
        //    if (DatosBase!= null)
        //    {
        //        localReport.AddDataSource(dataSetName: "Base", dataSource: DatosBase);
        //    }

        //    if (reportType == 1)
        //    {
        //        var result = localReport.Execute(RenderType.Pdf, extension, parameter, mimeType);
        //        return File(result.MainStream, contentType: "application/pdf");
        //    }
        //    else if (reportType == 2)
        //    {
        //        var result = localReport.Execute(RenderType.Excel, extension, parameter, mimeType);
        //        //vnd.ms-excel
        //        return File(result.MainStream, contentType: "application/vnd.ms-excel");
        //        //return File(result.MainStream, contentType: "application/msexcel");
        //    }
        //    return null;
        //}

        public FileContentResult ReportGenerator<@Type, @Base>(IEnumerable<Type> DataInList, string ReportUrl, int reportType = 1, IEnumerable<Base> DatosBase = null, string DataSetName = "DataSet1", Dictionary<string, string> parameter = null)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Stream sl = new FileStream(ReportUrl, FileMode.Open, FileAccess.Read);
            string path = ReportUrl;
            LocalReport localReport = new LocalReport();
            localReport.LoadReportDefinition(sl);

            if (parameter != null)
            {
                foreach (var item in parameter)
                {
                    localReport.SetParameters(new[] { new ReportParameter(item.Key, item.Value) });
                }
            }

            if (DatosBase != null)
            {
                localReport.DataSources.Add(new ReportDataSource("Base", DatosBase));
            }

            if (DataInList == null)
            {
                localReport.DataSources.Add(new ReportDataSource(DataSetName, DataInList));
            }
            else
            {
                localReport.DataSources.Add(new ReportDataSource(DataSetName, DataInList));
            }

            if (reportType == 1)
            {
                int ext = (int)(DateTime.Now.Ticks >> 10);
                var result = localReport.Render("PDF");
                return File(result, "application/pdf");
            }
            else if (reportType == 2)
            {
                int ext = (int)(DateTime.Now.Ticks >> 10);
                var result = localReport.Render("EXCEL");
                return File(result, "application/vnd.ms-excel");
            }
            return null;
        }
        //public IActionResult ReportGenerator<@Type, @Base>(IEnumerable<Type> Datos, string ReportUrl, int reportType = 1, IEnumerable<Base> DatosBase = null)
        //{

        //    string mimeType = "";
        //    int extension = 1;
        //    string path = ReportUrl;
        //    Dictionary<string, string> parameter = new Dictionary<string, string>();
        //    //parameter.Add("param","RDLC REPORT");
        //    LocalReport localReport = new LocalReport(path);
        //    localReport.AddDataSource(dataSetName: "DataSet1", dataSource: Datos);
        //    if (DatosBase != null)
        //    {
        //        localReport.AddDataSource(dataSetName: "Base", dataSource: DatosBase);
        //    }

        //    if (reportType == 1)
        //    {
        //        var result = localReport.Execute(RenderType.Pdf, extension, parameter, mimeType);
        //        return File(result.MainStream, contentType: "application/pdf");
        //    }
        //    else if (reportType == 2)
        //    {
        //        var result = localReport.Execute(RenderType.Excel, extension, parameter, mimeType);
        //        //vnd.ms-excel
        //        return File(result.MainStream, contentType: "application/vnd.ms-excel");
        //        //return File(result.MainStream, contentType: "application/msexcel");
        //    }
        //    return null;
        //}
    }
}
