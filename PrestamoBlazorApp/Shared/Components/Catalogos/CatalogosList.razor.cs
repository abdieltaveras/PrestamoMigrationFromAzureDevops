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

        [Parameter] public Action<Catalogo> ShowEditorForAdd { get; set; }
        [Parameter] public Action<Catalogo> ShowEditorForEdit { get; set; }
        [Parameter] public Action<Catalogo> ShowEditorForDelete { get; set; }
        [Inject] protected CatalogosService CatalogosService { get; set; }
        private IEnumerable<Catalogo> Catalogos { get; set; } = new List<Catalogo>();
        private Catalogo SelectedItem { get; set; } = null;
        private HashSet<Catalogo> selectedItems = new HashSet<Catalogo>();
        private string SearchValue { get; set; }
        protected bool ValidCatalogoSpecification => (CatalogoSpecification != null && !CatalogoSpecification.NombreTabla.IsNullOrEmpty() && !CatalogoSpecification.IdTabla.IsNullOrEmpty() && (!CatalogoName.IsNullOrEmpty()));

        private CommonActionsForCatalogo GetCommonActions() =>  new CommonActionsForCatalogo(ShowEditorForAdd,ShowEditorForEdit, ShowEditorForDelete);

        private IEnumerable<ButtonForToolBar<Catalogo>> Buttons() => Factory.StandarCrudToolBarButtons(GetCommonActions());

        async Task PrintListado(int reportType)
        {
            await BlockPage();
            CatalogoSpecification.reportType = reportType;
            var result = await CatalogosService.ReportListado(jsRuntime, CatalogoSpecification);
            await UnBlockPage();
        }
        protected override async Task OnInitializedAsync()
        {
            if (CatalogoSpecification != null)
            {
                await base.OnInitializedAsync();
                Catalogos = await CatalogosService.Get(CatalogoSpecification);
            }
            
        }
        protected bool FilterFunc(object obj) => Factory.FilterFuncForCatalogo(obj, SearchValue);
    }
}