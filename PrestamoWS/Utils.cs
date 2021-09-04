using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamoWS
{
    public class Utils :ControllerBase
    {
      
        //public IActionResult CatalogoReportList<@Type>(List<Type> Datos,string ReportUrl, int reportType = 1)
        //{

        //    string mimeType = "";
        //    int extension = 1;
        //    string path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\{ReportUrl}";
        //    Dictionary<string, string> parameter = new Dictionary<string, string>();
        //    //parameter.Add("param","RDLC REPORT");
        //    LocalReport localReport = new LocalReport(path);
        //    localReport.AddDataSource(dataSetName: "DataSet1", dataSource: Datos);
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
        public IActionResult CatalogoReportList<@Type>(IEnumerable<Type> Datos, string ReportUrl, int reportType = 1)
        {

            string mimeType = "";
            int extension = 1;
            string path = ReportUrl;
            Dictionary<string, string> parameter = new Dictionary<string, string>();
            //parameter.Add("param","RDLC REPORT");
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource(dataSetName: "DataSet1", dataSource: Datos);
            if (reportType == 1)
            {
                var result = localReport.Execute(RenderType.Pdf, extension, parameter, mimeType);
                return File(result.MainStream, contentType: "application/pdf");
            }
            else if (reportType == 2)
            {
                var result = localReport.Execute(RenderType.Excel, extension, parameter, mimeType);
                //vnd.ms-excel
                return File(result.MainStream, contentType: "application/vnd.ms-excel");
                //return File(result.MainStream, contentType: "application/msexcel");
            }
            return null;
        }
       
    }
}
