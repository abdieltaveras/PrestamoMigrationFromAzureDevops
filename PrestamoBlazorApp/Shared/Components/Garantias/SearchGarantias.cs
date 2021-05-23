using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Services;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Garantias
{
    public partial class SearchGarantias: BaseForSearch
    {
        [Inject]
        GarantiasService GarantiasService { get; set; }
        [Parameter]
        public bool showUI { get; set; } = false;
        [Parameter]
        public EventCallback<int> OnGarantiaSelected { get; set; }
        IEnumerable<GarantiaConMarcaYModelo> Garantias { get; set; } = new List<GarantiaConMarcaYModelo>();
        int SelectedSearchOption { get; set; } = 1;
        string TextToSearch { get; set; } = string.Empty;
        private async Task OnTextSearchChange(ChangeEventArgs args)
        {
            TextToSearch = args.Value.ToString();
        }
        private async Task SearchGarantia()
        {
            if (TextToSearch.Length <= 2)
            {
                await NotifyMessageBox("Debe digitar minimo 2 digitos (letras y/o numeros) para realizar la busqueda");
                return;
            }
            OpcionesSearchGarantia opcion = (OpcionesSearchGarantia)SelectedSearchOption;
            Garantias = new List<GarantiaConMarcaYModelo>();
            switch (opcion)
            {
                case OpcionesSearchGarantia.TextoLibre:
                    Garantias = await GarantiasService.SearchGarantias(TextToSearch);
                    break;
                case OpcionesSearchGarantia.NoIdentificacion:
                    Garantias = await GarantiasService.GetGarantias(new GarantiaGetParams { NoIdentificacion = TextToSearch });
                    break;
                case OpcionesSearchGarantia.Placa:
                    Garantias = await GarantiasService.GetGarantias(new GarantiaGetParams { Placa = TextToSearch });
                    break;
                case OpcionesSearchGarantia.Matricula:
                    Garantias = await GarantiasService.GetGarantias(new GarantiaGetParams { Matricula = TextToSearch });
                    break;
                default:
                    break;
            }

        }

        private void OnSelectedOptionChange(ChangeEventArgs args)
        {
            SelectedSearchOption = Convert.ToInt32(args.Value);
        }

        private async Task SelectGarantia(int idGarantia)
        {
            await OnGarantiaSelected.InvokeAsync(idGarantia);
        }

        private void onShowUiChange()
        {
            showUI = !showUI;
        }

        enum OpcionesSearchGarantia { TextoLibre = 1, NoIdentificacion, Placa, Matricula }
    }
}
