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

    public partial class CatalogosList : CommonBase
    {
        [Parameter] public CatalogoGetParams CatalogoSpecification { get; set; } = null;
        [Parameter] public string CatalogoName { get; set; } = null;
        [Parameter] public Action<Catalogo> ShowEditor { get; set; }
        [Inject] protected CatalogosService CatalogosService { get; set; }
        protected IEnumerable<Catalogo> Catalogos { get; set; } = new List<Catalogo>();
        private Catalogo SelectedItem { get; set; } = null;
        private HashSet<Catalogo> selectedItems = new HashSet<Catalogo>();
        private string SearchValue { get; set; }
        protected bool ValidCatalogoSpecification => (CatalogoSpecification != null && !CatalogoSpecification.NombreTabla.IsNullOrEmpty() && !CatalogoSpecification.IdTabla.IsNullOrEmpty() && (!CatalogoName.IsNullOrEmpty()));

        private Catalogo ObjectToCatalog(object obj) => (Catalogo)obj;
        private IEnumerable<ToolbarButtonForMud<Catalogo>> Buttons(ICrudStandardButtonsAndActions<Catalogo> view) => Factory.StandarCrudToolBarButtons(CommonActions);

        async void PrintListado(int reportType)
        {
            await BlockPage();
            CatalogoSpecification.reportType = reportType;
            var result = await CatalogosService.ReportListado(jsRuntime, CatalogoSpecification);
            await UnBlockPage();
        }

        CommonActionsForCatalogo CommonActions { get; set; } = new CommonActionsForCatalogo();
        protected override async Task OnInitializedAsync()
        {
            if (CatalogoSpecification != null)
            {
                await base.OnInitializedAsync();
                Catalogos = await CatalogosService.Get(CatalogoSpecification);
            }
            CommonActions = new CommonActionsForCatalogo(ShowEditor, jsRuntime);
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

    public class CommonActionsForCatalogo : ICrudStandardButtonsAndActions<Catalogo>
    {
        Action<Catalogo> ShowEditor { get; }
        IJSRuntime JsRuntime { get; }

        public CommonActionsForCatalogo()
        {

        }
        public CommonActionsForCatalogo(Action<Catalogo> showEditor, IJSRuntime jsRuntime)
        {
            ShowEditor = showEditor;
            JsRuntime = jsRuntime;
        }
        public bool BtnAddEnabled(Catalogo obj) => true;
        public bool BtnEdtEnabled(Catalogo obj) => obj != null;
        public bool BtnDelEnabled(Catalogo obj) => obj != null;
        public bool BtnReportEnabled(Catalogo obj) => obj != null;

        public bool BtnAddShow() => true;
        public bool BtnEdtShow() => true;

        public bool BtnDelShow() => true;


        public void BtnAddClick(Catalogo obj)
        {
            ShowEditor(new Catalogo());
        }

        public void BtnEdtClick(Catalogo obj)
        {

            if (obj != null)
            {
                ShowEditor(obj);
            }
        }

        public void BtnDelClick(Catalogo obj)
        {
            Task.Run(async () =>
                        await JsInteropUtils.SweetMessageBox(JsRuntime, "Accion no implementada aun", "info"));
        }

    }

}