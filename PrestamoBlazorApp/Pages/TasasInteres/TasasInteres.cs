using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.TasasInteres
{
    public partial class TasasInteres : BaseForCreateOrEdit
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }
        [Inject]
        TasasInteresService TasasInteresService { get; set; }
        IEnumerable<TasaInteres> tasasinteres { get; set; } = new List<TasaInteres>();
        [Parameter]
        public TasaInteres TasaInteres { get; set; } = new TasaInteres();

        protected override void OnInitialized()
        {
            base.OnInitialized();
          
            this.TasaInteres = new TasaInteres();
        }
        protected override async Task OnInitializedAsync()
        {
            await GetData();
        }
        async Task CreateOrEdit(int idTasaInteres = -1)
        {
            await BlockPage();
            if (idTasaInteres > 0)
            {
                var tasainteres = await TasasInteresService.Get(new TasaInteresGetParams { idTasaInteres = idTasaInteres });
                this.TasaInteres = tasainteres.FirstOrDefault();
            }
            else
            {
                this.TasaInteres = new TasaInteres();
            }
            await UnBlockPage();
            await JsInteropUtils.ShowModal(jsRuntime, "#MyModal");
        }
        async Task SaveTasaInteres()
        {
            await BlockPage();
            await TasasInteresService.SaveTasaInteres(this.TasaInteres);
            await SweetMessageBox("Guardado Correctamente", "success", "");
            await JsInteropUtils.CloseModal(jsRuntime, "#MyModal");
            await GetData();
            await UnBlockPage();
        }


        async Task GetData()
        {
            tasasinteres = await TasasInteresService.Get(new TasaInteresGetParams());
        }
    }
}
