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
        TerritoriosService territoriosService { get; set; }
        IEnumerable<Territorio> territorios { get; set; } = new List<Territorio>();
        IEnumerable<Territorio> listadeterritorios { get; set; } = new List<Territorio>();
        [Parameter]
        public Territorio Territorio { get; set; }
        
        void Clear() => territorios = null;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.Territorio = new Territorio();
            //this.Ocupacion = new Ocupacion();
        }
        protected override async Task OnInitializedAsync()
        {
             listadeterritorios = await territoriosService.GetDivisionesTerritoriales();
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
                if (this.Territorio.IdDivisionTerritorialPadre <= 0 || this.Territorio.IdLocalidadPadre <= 0)
                {
                    await OnGuardarNotification("Error Al Guardar, llene todos los campos");
                }
                else
                {
                    await BlockPage();
                    await territoriosService.SaveTerritorio(this.Territorio);
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
