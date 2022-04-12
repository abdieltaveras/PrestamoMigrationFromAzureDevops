﻿using PrestamoEntidades;
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
        [Parameter] public string CatalogoName { get; set; } = null;

        [Parameter] public Action<CatalogoInsUpd> ShowEditorForAddHandler { get; set; }
        [Parameter] public Action<CatalogoInsUpd> ShowEditorForEditHandler { get; set; }
        [Parameter] public Action<CatalogoInsUpd> ShowEditorForDeleteHandler { get; set; }

        [Parameter] public  Func<BaseCatalogoGetParams, Task<IEnumerable<CatalogoInsUpd>>> GetCatalogosHandler { get; set; }

        [Inject] CatalogosService CatalogosService { get; set; }
        private IEnumerable<CatalogoInsUpd> Catalogos { get; set; } = new List<CatalogoInsUpd>();
        private CatalogoInsUpd SelectedItem { get; set; } = null;
        private HashSet<CatalogoInsUpd> selectedItems = new HashSet<CatalogoInsUpd>();
        private string SearchValue { get; set; }

        private CommonActionsForCatalogo GetCommonActions() => new CommonActionsForCatalogo(ShowEditorForAddHandler, ShowEditorForEditHandler, ShowEditorForDeleteHandler);

        private IEnumerable<ButtonForToolBar<CatalogoInsUpd>> Buttons() => Factory.StandarCrudToolBarButtons(GetCommonActions());

        async Task PrintListado(int reportType)
        {
            await BlockPage();
            var catalogoReportParam = new CatalogoReportGetParams { ReportType = reportType };

            var result = await CatalogosService.ReportListado(jsRuntime, catalogoReportParam);
            await UnBlockPage();
        }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            this.Catalogos = await GetCatalogosHandler.Invoke(new BaseCatalogoGetParams());
        }
        protected bool FilterFunc(object obj) => Factory.FilterFuncForCatalogo(obj, SearchValue);
    }
}