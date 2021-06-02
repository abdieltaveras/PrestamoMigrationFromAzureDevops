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
            tasasinteres = await TasasInteresService.Get(new TasaInteresGetParams());
        }
        async Task CreateOrEdit(int idTasaInteres = -1)
        {
            await BlockPage();
            if (idTasaInteres > 0)
            {
                //var marca = await marcasService.Get(new MarcaGetParams { IdMarca = idMarca });
                //this.Marca = marca.FirstOrDefault();
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
            await UnBlockPage();
            await SweetMessageBox("Guardado Correctamente", "success", "");
            //await JsInteropUtils.Reload(jsRuntime, true);
        }
    }
}
