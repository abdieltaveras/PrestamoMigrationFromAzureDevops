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
using Microsoft.AspNetCore.Components.Authorization;
using DevBox.Core.Access;

namespace PrestamoBlazorApp.Shared.Components.Catalogos
{

    public partial class CatalogosList : CommonBase
    {
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] private ActionsManagerService _ActionsManagerService { get; set; }
        [Parameter] public string CatalogoName { get; set; } 
        [Parameter] public Func<CatalogoInsUpd, Func<Task>, Task> ShowEditorForAddHandler { get; set; }
        [Parameter] public Func<CatalogoInsUpd, Func<Task>, Task> ShowEditorForEditHandler { get; set; }
        [Parameter] public Func<CatalogoInsUpd, Func<Task>, Task> ShowEditorForDeleteHandler { get; set; }
        [Parameter] public  Func<BaseCatalogoGetParams, Task<IEnumerable<CatalogoInsUpd>>> GetCatalogosHandler { get; set; }
        private DevBox.Core.Access.Action CurrentAction { get; set; }
        
        private IEnumerable<CatalogoInsUpd> Catalogos { get; set; } = new List<CatalogoInsUpd>();
        private CatalogoInsUpd SelectedItem { get; set; } = null;
        private HashSet<CatalogoInsUpd> selectedItems = new HashSet<CatalogoInsUpd>();
        private string SearchValue { get; set; }
        

        private CommonActionsForCatalogo GetCommonActions() => new CommonActionsForCatalogo(ShowEditorForAddHandler, ShowEditorForEditHandler, ShowEditorForDeleteHandler, UpdateList, CurrentAction);

        private IEnumerable<ButtonForToolBar<CatalogoInsUpd>> Buttons() => Factory.StandarCrudToolBarButtons(GetCommonActions());

        async Task PrintListado(int reportType)
        {
            await BlockPage();
            var catalogoReportParam = new CatalogoReportGetParams { ReportType = reportType };

            //var result = await CatalogosService.ReportListado(jsRuntime, catalogoReportParam);
            await UnBlockPage();
        }
        protected override async Task OnInitializedAsync()
        {
            await AuthorizeViewActions();
            CurrentAction = _ActionsManagerService.CurrentAction;

            await base.OnInitializedAsync();
            await UpdateList();
        }

        private async Task UpdateList()
        {
            this.Catalogos = await GetCatalogosHandler.Invoke(new BaseCatalogoGetParams());
            StateHasChanged();
        }

        protected bool FilterFunc(object obj) => Factory.FilterFuncForCatalogo(obj, SearchValue);
    }
}