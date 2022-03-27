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
using PrestamoBlazorApp.Shared.Components.Base;



namespace PrestamoBlazorApp.Shared.Components.Catalogos
{

    

    public partial class CatalogosListV3 : CommonBase
    {
        [Parameter] public CatalogoGetParams CatalogoSpecification { get; set; } = null;
        [Parameter] public string CatalogoName { get; set; } = null;
        [Inject] protected IDialogService Dialog { get; set; }
        [Inject] protected CatalogosService CatalogosService { get; set; }
        [Inject] protected NavigationManager NavMa { get; set; }
        protected IEnumerable<Catalogo> Catalogos { get; set; } = new List<Catalogo>();

        private Catalogo selectedItem { get; set; } = null;

        private HashSet<Catalogo> selectedItems = new HashSet<Catalogo>();
        private string SearchValue { get; set; }
        protected bool ValidCatalogoSpecification => (CatalogoSpecification != null && !CatalogoSpecification.NombreTabla.IsNullOrEmpty() && !CatalogoSpecification.IdTabla.IsNullOrEmpty() && (!CatalogoName.IsNullOrEmpty()));

        public List<DataGridViewToolbarButton> buttons => new List<DataGridViewToolbarButton>()
        {
        new DataGridViewToolbarButton(){ Color= MudBlazor.Color.Success, Icon=Icons.Filled.AddCircle, Text="Nuevo", OnClick=btnAddClick, IsEnabled=btnAddEnabled},
        new DataGridViewToolbarButton(){ Color= MudBlazor.Color.Secondary, Icon=Icons.Filled.Edit, Text="Modificar", OnClick=btnEdtClick, IsEnabled=btnEdtEnabled},
        new DataGridViewToolbarButton(){ Color= MudBlazor.Color.Error, Icon=Icons.Filled.Remove, Text="Eliminar", OnClick=btnDelClick, IsEnabled=btnDelEnabled},
        new DataGridViewToolbarButton(){ Color= MudBlazor.Color.Primary, Icon=Icons.Filled.VpnKey, Text="Reporte", OnClick=btnReportClick, IsEnabled=btnReportEnabled},
        };

        bool btnAddEnabled(object obj) => true;
        bool btnEdtEnabled(object obj) => ObjectToCatalog(obj) != null;
        bool btnDelEnabled(object obj) => ObjectToCatalog(obj) != null;
        bool btnReportEnabled(object obj) => ObjectToCatalog(obj) != null;



        Catalogo ObjectToCatalog(object obj) => (Catalogo)obj;
        protected async void btnReportClick(object obj)
        {
            await NotifyNotImplementedAction();
        }

        void btnAddClick(object obj) => showEditor(new Catalogo());

        void btnEdtClick(object obj)
        {
            
            if (obj != null)
            {
                showEditor(ObjectToCatalog(obj));
            }
        }

        void btnDelClick(object obj) { }


        protected void showEditor(Catalogo catalogo)
        {
            var parameters = new DialogParameters();
            catalogo.IdTabla = CatalogoSpecification.IdTabla;
            catalogo.NombreTabla = CatalogoSpecification.NombreTabla;
            parameters.Add("Catalogo", catalogo);
            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };
            Dialog.Show<CatalogoEditor>("Editar", parameters, options);
        }


        public string searchString { get; set; }
        protected override async Task OnInitializedAsync()
        {
            if (CatalogoSpecification != null)
            {
                await base.OnInitializedAsync();
                Catalogos = await CatalogosService.Get(CatalogoSpecification);
            }
        }
        protected bool FilterFunc(object obj)
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

        protected void HandleSearchValueChanged(string value)
        {
            this.searchString = value;
        }

    }


}
