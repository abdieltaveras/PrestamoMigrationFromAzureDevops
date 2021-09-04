using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using PcpUtilidades;

using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Services
{
    public class ReportesService : ServiceBase
    {
        readonly string apiUrl = "api/reportes";
        string apiReportUrl = "api/reportes";

        public ReportesService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory, configuration)
        {
        }

        public async Task<string> ReporteGenerico(IJSRuntime jSRuntime,string EndPoint, BaseReporteParams search)
        {
            var d = await ReportGenerate(jSRuntime, $"{apiReportUrl}/{EndPoint}", search);
            return d.StatusCode.ToString();
        }
    }
}
