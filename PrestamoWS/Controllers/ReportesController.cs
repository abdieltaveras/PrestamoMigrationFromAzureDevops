using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PrestamoBLL;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrestamoWS.Reports;
using HESRAM.Utils;
using Newtonsoft.Json;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PrestamoWS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportesController : ControllerBasePrestamoWS
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private Utils _utils { get; set; } = new Utils();
        public ReportesController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }


        [HttpGet]
        public IActionResult CatalogoReportList([FromQuery] CatalogoReportGetParams getParams,int reportType = 1)
        { 

            List<Reports.Bases.BaseReporteMulti> baseReporte = new List<Reports.Bases.BaseReporteMulti>();
            Reports.Bases.BaseReporteMulti BasePrueba = new Reports.Bases.BaseReporteMulti { NombreEmpresa = "PC Prog",
                DireccionEmpresa="Calle Principal, Algun barrio de la romana, La Romana, Rep Dom",
                TituloReporte = "Catalogo",
                ImpresoPor="lheskey",
                RangoFiltro ="A-Z",
                OrdenadoPor="Nombre",
                OtrosDetalles="-"
            };
            baseReporte.Add(BasePrueba);
            //******************************************************//
            _utils = new Utils();
            reportType = getParams.ReportType;
            //todo: arreglar este y todos los procedimientos relacionados con catalogos
            var datos = BLLPrestamo.Instance.GetCatalogos<CatalogoInsUpd>(CatalogoName.Generico,     getParams.CatalogoGParams);
            string path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\Catalogo\\Listado.rdlc";
            return _utils.CatalogoReportList(datos, path, reportType,baseReporte);
            #region ComoUsar
            //DataTable dt = new DataTable();
            //string mimeType = "";
            //int extension = 1;
            //string path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\Catalogo\\Listado.rdlc";
            //Dictionary<string, string> parameter = new Dictionary<string, string>();
            ////parameter.Add("param","RDLC REPORT");
            //LocalReport localReport = new LocalReport(path);
            //localReport.AddDataSource(dataSetName: "DataSet1", dataSource: datos);
            //if (reportType == 1)
            //{
            //    var result = localReport.Execute(RenderType.Pdf, extension, parameter, mimeType);
            //    return File(result.MainStream, contentType: "application/pdf");
            //}
            //else if (reportType == 2)
            //{
            //    var result = localReport.Execute(RenderType.Excel, extension, parameter, mimeType);
            //    //vnd.ms-excel
            //    return File(result.MainStream, contentType: "application/vnd.ms-excel");
            //    //return File(result.MainStream, contentType: "application/msexcel");
            //}
            //return null;
            #endregion
        }
        [HttpGet]
        public IActionResult ClienteReportList([FromQuery] BaseReporteParams getParams)
        {
            List<Reports.Bases.BaseReporteMulti> baseReporte = new List<Reports.Bases.BaseReporteMulti>();
            Reports.Bases.BaseReporteMulti BasePrueba = new Reports.Bases.BaseReporteMulti
            {
                NombreEmpresa = "PC Prog",
                DireccionEmpresa = "Calle Principal, Algun barrio de la romana, La Romana, Rep Dom",
                TituloReporte = "Listado De Clientes",
                ImpresoPor = "lheskey",
                RangoFiltro = getParams.RDesde +"-"+ getParams.RHasta,
                OrdenadoPor = getParams.OrdenarPor,
                OtrosDetalles = "-"
            };
            baseReporte.Add(BasePrueba);
            //******************************************************//
            _utils = new Utils();
            var datos = BLLPrestamo.Instance.ReporteClientes(getParams);
            string path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\Clientes\\Listado.rdlc";
            var resultado = _utils.ReportGenerator(null, path, getParams.reportType, baseReporte, DataInList:datos);
            return resultado;
        }
  
    }
}
