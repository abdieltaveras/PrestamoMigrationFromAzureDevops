using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PrestamoEntidades;
using PrestamoBlazorApp.Services;
namespace PrestamoBlazorApp.Shared.Components.Reports
{
    public partial class SearchReportGeneric : BaseForList
    {
        [Inject]
        public ReportesService reportesService { get; set; }
        [Parameter]
        public BaseReporteParams BaseReporteParams { get; set; } = new BaseReporteParams();
        [Parameter]
        public string EndPointReporte { get; set; }
        public DateTime FDesde { get; set; } = DateTime.Now;
        public DateTime FHasta { get; set; } = DateTime.Now;
        protected override async Task OnInitializedAsync()
        {
            // await Handle_GetDataForList(GetClientes);
          
            BaseReporteParams = new BaseReporteParams();
            await base.OnInitializedAsync();
        }
       private async void GenerarReporte()
       {
            await JsInteropUtils.CloseModal(jsRuntime, "#ModalGenerarReporte");
            await BlockPage();
            if(BaseReporteParams.Rango == "FechaIngreso" || BaseReporteParams.Rango == "FechaNacimiento")
            {
                BaseReporteParams.Desde = FDesde.ToString();
                BaseReporteParams.Hasta = FHasta.ToString();
            }
            var a =   await reportesService.ReporteGenerico(jsRuntime, EndPointReporte, BaseReporteParams);
            await UnBlockPage();
       }

       private async void VerGenerador() 
       {
            await JsInteropUtils.ShowModal(jsRuntime, "#ModalGenerarReporte");
       }
    }
}
