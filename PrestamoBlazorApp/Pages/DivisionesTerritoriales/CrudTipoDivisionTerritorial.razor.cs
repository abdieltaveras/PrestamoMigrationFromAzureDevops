using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.DivisionesTerritoriales
{
    public partial class CrudTipoDivisionTerritorial: BaseForCreateOrEdit
    {

        [Parameter]
        public int idDivisionTerritorial { get; set; }


        [Parameter]
        public EventCallback<bool> HandleListUpdate { get; set; }

        [Inject]
        DivisionTerritorialService svrDivisionTerrirorial { get; set; }

        [CascadingParameter] MudDialogInstance MudDialog { get; set; }


        DivisionTerritorial _DivisionTerritorial { get; set; } = new DivisionTerritorial();

        bool test { get; set; } = true;
        async Task Cancel() => MudDialog.Close(DialogResult.Cancel());

        protected override async Task OnInitializedAsync()
        {
            if (idDivisionTerritorial > 0) { await GetData(); }
            await base.OnInitializedAsync();
        }

        

        private async Task GetData()
        {
            var result = await svrDivisionTerrirorial.GetDivisionesTerritoriales(new DivisionTerritorialGetParams { idDivisionTerritorial = idDivisionTerritorial });
            _DivisionTerritorial = result.FirstOrDefault();
        }
        async Task SaveData()
        {
            form.Validate();

            if (form.IsValid)
            {
                //var divisionTerritorial = new DivisionTerritorial { Nombre = NombreTipoDivisionTerritorial, IdDivisionTerritorial = idDivisionTerritorial, IdDivisionTerritorialPadre = null };

                var saveSucceed = await svrDivisionTerrirorial.SaveDivisionTerritorial(_DivisionTerritorial);

                if (saveSucceed == false)
                {
                    await NotifyMessageBySnackBar("No se pudo guardar los datos", Severity.Error);
                    return;
                }
                else
                {
                    await NotifyMessageBySnackBar("Datos guardados exitosamente", Severity.Success);
                    
                }
                await HandleListUpdate.InvokeAsync(true);
                MudDialog.Close(DialogResult.Ok(true));
            }
        }
        private int MudItemSize =>  (FormFieldErrors!=null  && IsFormShowErrors)  ? 7 : 12;

    }

}
