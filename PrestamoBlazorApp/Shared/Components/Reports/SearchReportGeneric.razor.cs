using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PrestamoEntidades;
using PrestamoBlazorApp.Services;
using System.Reflection;

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
        public DateTime? FDesde { get; set; } = DateTime.Now;
        public DateTime? FHasta { get; set; } = DateTime.Now;
        List<PropertyInfo> SearchOptions = new List<PropertyInfo>();
        private string _SelectedOptionSearch { get; set; }
        private string SelectedOptionSearch { get { return _SelectedOptionSearch; } set { _SelectedOptionSearch = value; OnSelectOptionSearch(value).GetAwaiter(); } }
        private bool ShowDatePicker { get; set; } = false;
        protected override async Task OnInitializedAsync()
        {
            // await Handle_GetDataForList(GetClientes);
           
            foreach (PropertyInfo item in typeof(Cliente).GetProperties())
            {
                SearchOptions.Add(item);
            }
            BaseReporteParams = new BaseReporteParams();
            await base.OnInitializedAsync();
        }
       private async void GenerarReporte()
       {
            await JsInteropUtils.CloseModal(jsRuntime, "#ModalGenerarReporte");
            await BlockPage();
            BaseReporteParams.Opcion = 1;
            if (ShowDatePicker)
            {
                BaseReporteParams.FechaDesde = FDesde;
                BaseReporteParams.FechaHasta = FHasta;
                BaseReporteParams.Opcion = 2;
            }
            //if(BaseReporteParams.Rango == "FechaIngreso" || BaseReporteParams.Rango == "FechaNacimiento")
            //{
            //    BaseReporteParams.ODesde = FDesde.ToString();
            //    BaseReporteParams.OHasta = FHasta.ToString();
            //}
            var a =   await reportesService.ReporteGenerico(jsRuntime, EndPointReporte, BaseReporteParams);
            await UnBlockPage();
       }
        private async Task OnSelectOptionSearch(string option)
        {
            ShowDatePicker = false;
            BaseReporteParams.OrdenarPor = option;
            if (option == "FechaIngreso" || option == "FechaNacimiento")
            {
                ShowDatePicker = true;
            }
        }

       private async void VerGenerador() 
       {
            
            await JsInteropUtils.ShowModal(jsRuntime, "#ModalGenerarReporte");
       }

    }
}
