using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Shared.Components.Localizadores;
namespace PrestamoBlazorApp.Pages.Localizadores
{
    public partial class Localizadores : BaseForList
    {
        [Inject]
        IDialogService DialogService { get; set; }
        private DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseOnEscapeKey = true };
        public Localizador Localizador { get; set; } = new Localizador();
        public int IdLocalizador { get; set; }
        [Inject]
        private LocalizadoresService LocalizadoresService { get; set; }
        IEnumerable<Localizador> localizadores { get; set; } = new List<Localizador>();

        protected override async Task OnInitializedAsync()
        {
            await GetLocalizadores();
        }

        async Task GetLocalizadores()
        {
            localizadores = await LocalizadoresService.Get(new LocalizadorGetParams ());
        }
        //private bool FilterFunc(TasaInteres element, string searchString)
        //{
        //    if (string.IsNullOrWhiteSpace(searchString))
        //        return true;
        //    if (element.Nombre.Contains(searchString, StringComparison.OrdinalIgnoreCase))
        //        return true;
        //    if (element.Codigo != null)
        //    {
        //        if (element.Codigo.Contains(searchString, StringComparison.OrdinalIgnoreCase))
        //            return true;
        //    }
        //    return false;
        //}
        private async Task ShowDialog(int id = -1)
        {
            var parameters = new DialogParameters();
            parameters.Add("IdLocalizador", id);
            var dialog = DialogService.Show<CreateLocalizador>("", parameters, dialogOptions);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await GetLocalizadores();
                StateHasChanged();
            }
        }
    }
}
