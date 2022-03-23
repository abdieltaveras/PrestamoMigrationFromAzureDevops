using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Shared;
using Newtonsoft.Json;
using MudBlazor;
using UIClient.Pages.Components;


namespace PrestamoBlazorApp.Shared.Components.Catalogos
{

    public partial class CatalogosList : CommonBase
    {
        [Parameter]
        public CatalogoGetParams CatalogoSpecification { get; set; } = null;
        [Inject] private IDialogService Dialog { get; set; }
        [Inject] private CatalogosService CatalogosService { get; set; }
        [Inject] private NavigationManager NavMa { get; set; } 
        private IEnumerable<Catalogo> Catalogos { get; set; } = new List<Catalogo>();

        private bool ValidCatalogoSpecification => (CatalogoSpecification!=null && !CatalogoSpecification.NombreTabla.IsNullOrEmpty() && !CatalogoSpecification.IdTabla.IsNullOrEmpty());

        private List<DataGridViewColumn> columns => new List<DataGridViewColumn>()
        {
        new DataGridViewColumn{ Header= "Codigo", ColumnName = "Codigo", ColumnType= DataGridViewColumnTypes.Text, ContentAligment= MudBlazor.Align.Left, HeaderAligment= MudBlazor.Align.Center},
        new DataGridViewColumn{ Header= "Nombre", ColumnName = "Nombre", ColumnType= DataGridViewColumnTypes.Text, ContentAligment= MudBlazor.Align.Left, HeaderAligment= MudBlazor.Align.Center},
        };

        

        public List<DataGridViewToolbarButton> buttons => new List<DataGridViewToolbarButton>()
        {
        new DataGridViewToolbarButton(){ Color= MudBlazor.Color.Primary, Icon=Icons.Filled.AddCircle, Text="Nuevo", OnClick=btnAddClick, IsEnabled=btnAddEnabled},
        new DataGridViewToolbarButton(){ Color= MudBlazor.Color.Secondary, Icon=Icons.Filled.Edit, Text="Modificar", OnClick=btnEdtClick, IsEnabled=btnEdtEnabled},
        new DataGridViewToolbarButton(){ Color= MudBlazor.Color.Tertiary, Icon=Icons.Filled.VpnKey, Text="Reporte", OnClick=btnReporte, IsEnabled=btnEdtEnabled},
        };

        
        private async void btnReporte(object obj)
        {
            await NotifyNotImplementedAction();
        }

        void btnAddClick(object obj) => showEditor(new Catalogo());
        void btnEdtClick(object obj)
        {
            var catalogo = (Catalogo)obj;
            if (catalogo != null)
            {
                showEditor(catalogo);
            }
        }
        private void showEditor(Catalogo user)
        {
            var parameters = new DialogParameters();
            parameters.Add("Catalogo", user);
            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };
            Dialog.Show<CatalogoEditor>("Editar", parameters, options);
        }
        bool btnAddEnabled(object obj) => true;
        bool btnEdtEnabled(object obj) => ((Catalogo)obj) != null;
        public string searchString { get; set; }
        protected override async Task OnInitializedAsync()
        {
            if (CatalogoSpecification != null)
            { 
                await base.OnInitializedAsync();
                Catalogos = await CatalogosService.Get(CatalogoSpecification);
            }
        }
        private bool FilterFunc(object obj)
        {
            var element = (Catalogo)obj;
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.Nombre.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Codigo != null)
            {
                if (element.Codigo.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        private bool FilterFunc1(Catalogo element) => FilterFunc2(element, this.searchString);

        private void HandleSearchValueChanged(string value)
        {
            this.searchString = value;
        }
        private bool FilterFunc2(Catalogo element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.Nombre.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Codigo != null)
            {
                if (element.Codigo.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
