using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Services;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PrestamoBlazorApp.Shared.Components.EntidadEstatus
{
    public partial class AddEstatus
    {
        [Inject]
        EntidadEstatusService EntidadEstatusService { get; set; }

        private string _SelectedEstatus { get; set; }
        private string SelectedEstatus { get { return _SelectedEstatus; } set { _SelectedEstatus = value; OnSelect(value).GetAwaiter(); } }
        private List<string> lstEstatus { get; set; } = new List<string>();
        [Parameter]
        public EventCallback<string> OnSelectItem { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await GetStatus();
            await base.OnInitializedAsync();
        }

        private async Task OnSelect(string val)
        {
            await OnSelectItem.InvokeAsync(val);
        }
        private async Task GetStatus()
        {
            lstEstatus = new List<string>();
            var datos = await EntidadEstatusService.Get(new EntidadEstatusGetParams { Option = 1 });
            foreach (var item in datos)
            {
                lstEstatus.Add(item.Name);
            }
            StateHasChanged();
        }
        private async Task<IEnumerable<string>> EstatusListSearch(string value)
        {
            // await GetStatus(); no recargaremos a menos que sea necesario mediante un refresh
            //return lstEstatus;
            // In real life use an asynchronous function for fetching data from an api.
            //await Task.Delay(5);
            await Task.Delay(5);
            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value)) return lstEstatus; //return new String[0]; 
            return lstEstatus.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }

    }
}
