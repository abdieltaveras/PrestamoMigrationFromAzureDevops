using DevBox.Core.Classes.Configuration;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIClient.Pages.Components;
using UIClient.Services;

namespace UIClient.Pages.Configuration
{
    public partial class DiaFeriadoEditor : BasePage
    {
        [Parameter] public DiaFeriadoNullable selectedDay { get; set; } = new DiaFeriadoNullable() { Dia = DateTime.Today };
        [Inject] private DiasFeriadosService diasFeriadosService { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        string DATE_FORMAT => System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
        bool validForm;
        string[] errors = { };
        MudForm form;
        async Task delDay()
        {
            await Task.Delay(500);
            if (!validForm) { return; }
            await diasFeriadosService.SaveDiaFeriadoAsync(selectedDay.Dia.Value, "");
            await SendAsync("Del Record", selectedDay);
        }
        async Task saveDay()
        {
            await Task.Delay(500);
            if (!validForm) { return; }
            await diasFeriadosService.SaveDiaFeriadoAsync(selectedDay.Dia.Value, selectedDay.Descripcion);
            await SendAsync("Ha ocurrido una actualización", selectedDay);
        }
        private async Task SendAsync(string message, object target)
        {
            NotificationsService.Notify(message, "*", "*", "*", mustReload: true);
        }
        private void CloseDlg()
        {
            MudDialog.Cancel();
        }
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            validForm = selectedDay.Dia.HasValue && !string.IsNullOrEmpty(selectedDay.Descripcion);
        }
    }
}
