using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared.Components.Forms;
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
        EstatusService EntidadEstatusService { get; set; }
        private SelectClass _value2 { get; set; }
        private SelectClass value2 { get { return _value2; } set { _value2 = value; OnSelect(value).GetAwaiter(); } }
        private List<SelectClass> lstEstatus { get; set; } = new List<SelectClass>();
        [Parameter]
        public EventCallback<SelectClass> OnSelectItem { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetStatus();
            await base.OnInitializedAsync();
        }

        private async Task OnSelect(SelectClass val)
        {
            await OnSelectItem.InvokeAsync(val);
        }
        private async Task GetStatus()
        {
            lstEstatus = new List<SelectClass>();
            var datos = await EntidadEstatusService.Get(new EstatusGetParams());
            foreach (var item in datos)
            {
                lstEstatus.Add(new SelectClass { Value = item.IdEstatus, Text = item.Name.ToString() });
            }
            StateHasChanged();
        }
    }
}
