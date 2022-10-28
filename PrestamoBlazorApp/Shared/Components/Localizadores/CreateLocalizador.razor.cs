using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Localizadores
{
    public partial  class CreateLocalizador : BaseForCreateOrEdit
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        public Localizador Localizador { get; set; } = new Localizador();
        [Parameter]
        public int IdLocalizador { get; set; }
        [Inject]
        private LocalizadoresService LocalizadoresService { get; set; }
        IEnumerable<Localizador> localizadores { get; set; } = new List<Localizador>();

        protected override async Task OnInitializedAsync()
        {
            await CreateOrEdit();
        }

        async Task CreateOrEdit()
        {
            if (IdLocalizador > 0)
            {
                await BlockPage();
                localizadores = await LocalizadoresService.Get(new LocalizadorGetParams { IdLocalizador = IdLocalizador });
                Localizador = localizadores.FirstOrDefault();
                await UnBlockPage();
            }
            else
            {
                this.Localizador = new Localizador();
            }
            StateHasChanged();
        }
        private async Task Save() 
        {
            await Handle_SaveData(async () => await LocalizadoresService.Post(this.Localizador), null, null, false, mudDialogInstance: MudDialog);
        }
    }
}
