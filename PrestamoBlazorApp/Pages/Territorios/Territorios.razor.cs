using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Territorios
{
    public partial class Territorios : BaseForCreateOrEdit
    {
        
        JsInteropUtils JsInteropUtils { get; set; } = new JsInteropUtils();
        [Inject]
        TerritoriosService TerritoriosService { get; set; }
        IEnumerable<DivisionTerritorial> _Territorios { get; set; } = new List<DivisionTerritorial>();
        IEnumerable<DivisionTerritorial> Listadeterritorios { get; set; } = new List<DivisionTerritorial>();
        [Parameter]
        public DivisionTerritorial Territorio { get; set; }
        
        void Clear() => _Territorios = null;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.Territorio = new DivisionTerritorial();
            //this.Ocupacion = new Ocupacion();
        }
        protected override async Task OnInitializedAsync()
        {
             Listadeterritorios = await TerritoriosService.GetDivisionesTerritoriales();
            // await JsInteropUtils.Territorio(jsRuntime);
        }
        public async Task VerTerritorios(string id)
        {
            await JsInteropUtils.Territorio(jsRuntime,id);
        }
        async Task SaveTerritorio()
        {
            try
            {
                if (this.Territorio.IdDivisionTerritorialPadre <= 0 || this.Territorio.IdDivisionTerritorialPadre <= 0)
                {
                    await OnSaveNotification("Error Al Guardar, llene todos los campos");
                }
                else
                {
                    await BlockPage();
                    await TerritoriosService.SaveTerritorio(this.Territorio);
                    await UnBlockPage();
                    await SweetMessageBox("Datos Guardados", "success", "/Territorios");
                    //await OnGuardarNotification();
                    //NavManager.NavigateTo("/Territorios");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
          


        }
        void RaiseInvalidSubmit()
        {

        }
    }
}
