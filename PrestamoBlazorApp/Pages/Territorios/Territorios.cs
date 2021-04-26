using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Territorios
{
    public partial class Territorios : BaseForCreateOrEdit
    {
        [Inject]
        IJSRuntime jsRuntime { get; set; }

        JsInteropUtils JsInteropUtils { get; set; } = new JsInteropUtils();
        [Inject]
        TerritoriosService territoriosService { get; set; }
        IEnumerable<Territorio> territorios { get; set; } = new List<Territorio>();
        IEnumerable<Territorio> listadeterritorios { get; set; } = new List<Territorio>();
        [Parameter]
        public Territorio Territorio { get; set; }
        bool loading = false;
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

          
            await territoriosService.SaveTerritorio(this.Territorio);
            await OnGuardarNotification();
            NavManager.NavigateTo("/Territorios");

        }
        void RaiseInvalidSubmit()
        {

        }
    }
}
