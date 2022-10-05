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
        private bool resetValueOnEmptyText;
        private bool coerceText;
        private bool coerceValue;
        private string _value2 { get; set; }
        private string value2 { get { return _value2; } set { _value2 = value; OnSelect(value).GetAwaiter(); } }
        private List<string> lstEstatus { get; set; } = new List<string>();
        [Parameter]
        public EventCallback<string> OnSelectItem { get; set; }

        //private string[] states =
        //{
        //    "Alabama", "Alaska", "American Samoa", "Arizona",
        //    "Arkansas", "California", "Colorado", "Connecticut",
        //    "Delaware", "District of Columbia", "Federated States of Micronesia",
        //    "Florida", "Georgia", "Guam", "Hawaii", "Idaho",
        //    "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky",
        //    "Louisiana", "Maine", "Marshall Islands", "Maryland",
        //    "Massachusetts", "Michigan", "Minnesota", "Mississippi",
        //    "Missouri", "Montana", "Nebraska", "Nevada",
        //    "New Hampshire", "New Jersey", "New Mexico", "New York",
        //    "North Carolina", "North Dakota", "Northern Mariana Islands", "Ohio",
        //    "Oklahoma", "Oregon", "Palau", "Pennsylvania", "Puerto Rico",
        //    "Rhode Island", "South Carolina", "South Dakota", "Tennessee",
        //    "Texas", "Utah", "Vermont", "Virgin Island", "Virginia",
        //    "Washington", "West Virginia", "Wisconsin", "Wyoming",
        //};

        protected override async Task OnInitializedAsync()
        {
            await GetStatus();
            //await base.OnInitializedAsync();
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
        private async Task<IEnumerable<string>> Search1(string value)
        {
            await GetStatus();
            //return lstEstatus;
            // In real life use an asynchronous function for fetching data from an api.
            //await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return lstEstatus;
            return lstEstatus.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
