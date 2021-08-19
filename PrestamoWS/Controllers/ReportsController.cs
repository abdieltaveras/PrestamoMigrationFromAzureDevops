using AspNetCore.Reporting;
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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PrestamoWS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private Utils _utils { get; set; } = new Utils();
        public ReportsController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
        [HttpGet]
        public IActionResult CatalogoReportList([FromQuery] CatalogoGetParams getParams,int reportType = 1)
        {
            _utils = new Utils();
            reportType = getParams.reportType;
            var datos = BLLPrestamo.Instance.GetCatalogosNew<Catalogo>(getParams);
            string path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\Catalogo\\Listado.rdlc";
            return _utils.CatalogoReportList<Catalogo>(datos, path, reportType);
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
        }

        // GET: api/<ReportsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ReportsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ReportsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ReportsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReportsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
