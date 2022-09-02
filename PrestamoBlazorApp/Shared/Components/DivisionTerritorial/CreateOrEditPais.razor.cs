using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PrestamoBlazorApp.Shared.Components.DivisionTerritorial
{
    public partial class CreateOrEditPais : CommonBase
    {
        PrestamoEntidades.DivisionTerritorial DivisionTerritorial { get; set; }
        [Inject]
        DivisionTerritorialService DivisionTerritorialService { get; set; }
        [Parameter]
        public int IdDivisionTerritorial { get; set; }
        protected override async Task OnInitializedAsync()
        {
            //await base.OnInitializedAsync();
        }

        async Task CreateOrEdit()
        {
            //await BlockPage();
            if (IdDivisionTerritorial > 0)
            {
                var division = await DivisionTerritorialService.GetDivisionesTerritoriales(new DivisionTerritorialGetParams { 
                 idDivisionTerritorial = IdDivisionTerritorial});
                this.DivisionTerritorial = division.FirstOrDefault();
            }
            else
            {
                this.DivisionTerritorial = new PrestamoEntidades.DivisionTerritorial();
            }
            //await UnBlockPage();

            //await JsInteropUtils.ShowModal(jsRuntime, "#MyModal");
        }
        async Task Save()
        {

            await BlockPage();
            await Handle_Funct(async () => await DivisionTerritorialService.SaveDivisionTerritorial(this.DivisionTerritorial));
            await SweetAlertSuccess("Datos Guardados Correctamente");
            await UnBlockPage();
        }

    }
}
