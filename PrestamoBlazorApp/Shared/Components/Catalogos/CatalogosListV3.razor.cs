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



    public partial class CatalogosListV3 : CommonBase, ICrudStandardButtonsAndActions
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

        private Catalogo ObjectToCatalog(object obj) => (Catalogo)obj;

        private List<ToolbarButtonForMud> buttons => Buttons(this);
        private List<ToolbarButtonForMud> Buttons(ICrudStandardButtonsAndActions view)
        {
            var buttons = new List<ToolbarButtonForMud>();
            buttons.AddRange(Factory.StandarCrudToolBarButtons(this));
            buttons.Add(
            new ToolbarButtonForMud() { Color = MudBlazor.Color.Primary, Icon = Icons.Filled.VpnKey, Text = "Reporte", OnClick = BtnReportClick, IsEnabled = BtnReportEnabled, Show = true });
            return buttons;
        }

        public bool BtnAddEnabled(object obj) => true;
        public bool BtnEdtEnabled(object obj) => ObjectToCatalog(obj) != null;
        public bool BtnDelEnabled(object obj) => ObjectToCatalog(obj) != null;
        public bool BtnReportEnabled(object obj) => ObjectToCatalog(obj) != null;

        public bool BtnAddShow() => true;
        public bool BtnEdtShow() => true;

        public bool BtnDelShow() => true;

        protected async void BtnReportClick(object obj)
        {
            await NotifyNotImplementedAction();
        }

        public void BtnAddClick(object obj) => showEditor(new Catalogo());

        public void BtnEdtClick(object obj)
        {

            if (obj != null)
            {
                showEditor(ObjectToCatalog(obj));
            }
        }

        public void BtnDelClick(object obj) { }


        protected void showEditor(Catalogo catalogo)
        {
            var parameters = new DialogParameters();
            catalogo.IdTabla = CatalogoSpecification.IdTabla;
            catalogo.NombreTabla = CatalogoSpecification.NombreTabla;
            parameters.Add("Catalogo", catalogo);
            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };
            Dialog.Show<CatalogoEditor>("Editar", parameters, options);
        }


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
            if (string.IsNullOrWhiteSpace(SearchValue))
                return true;
            if (element.Nombre.Contains(SearchValue, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Codigo != null)
            {
                if (element.Codigo.Contains(SearchValue, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }

    public static class Factory
    {

        public static IEnumerable<ToolbarButtonForMud> StandarCrudToolBarButtons(ICrudStandardButtonsAndActions view)
        {
            var buttons = new List<ToolbarButtonForMud>()
            {
            new ToolbarButtonForMud() { Color = MudBlazor.Color.Success, Icon = Icons.Filled.AddCircle, Text = "Nuevo", OnClick = view.BtnAddClick, IsEnabled = view.BtnAddEnabled, Show = view.BtnAddShow() },
            new ToolbarButtonForMud() { Color = MudBlazor.Color.Secondary, Icon = Icons.Filled.Edit, Text = "Modificar", OnClick = view.BtnEdtClick, IsEnabled = view.BtnEdtEnabled, Show = view.BtnEdtShow() },
            new ToolbarButtonForMud() { Color = MudBlazor.Color.Error, Icon = Icons.Filled.Delete, Text = "Eliminar", OnClick = view.BtnDelClick, IsEnabled = view.BtnDelEnabled, Show = view.BtnDelShow() }
            };
            return buttons;
        }
    }
}