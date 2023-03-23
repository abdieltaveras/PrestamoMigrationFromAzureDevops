using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Shared;
using PrestamoBlazorApp.Shared.Components.Catalogos;
using MudBlazor;
using PrestamoBlazorApp.Shared.Components.Base;
namespace PrestamoBlazorApp.Pages.Catalogos
{
    public abstract class CatalogosViewBase : BaseCatalogoComponent
    {

        [Inject] protected CatalogosServicesFactoryManager CatalogosFactory { get; set; }
        [Inject] protected IDialogService Dialog { get; set; }

        //[Inject] protected OcupacionesServiceV2 OcupacionesService { get; set; }
        //protected override string CatalogoName() => "Ocupaciones";

        [Inject] protected CommonInjectionsService CommomInjectionsService { get; set; }

        protected virtual async Task ShowEditor(CatalogoInsUpd catalogo, bool usarFormularioParaEliminar = false, Func<Task> action = null)
        {
            
                var parameters = new DialogParameters();
                parameters.Add("Catalogo", catalogo);
                parameters.Add("UsarFormularioParaEliminar", usarFormularioParaEliminar);
                parameters.Add("UpdateList", action);
                parameters.Add("CatalogosService", GetService);
                var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };
                await Dialog.ShowAsync<CatalogoEditor>("Editar", parameters, options);
            
        }

        protected virtual CatalogosService GetService { get; }

        protected override async Task ShowAddEditor(CatalogoInsUpd catalogo, Func<Task> action)
        {
            await ShowEditor(catalogo, false, action);
        }

        protected override async Task ShowEditEditor(CatalogoInsUpd catalogo, Func<Task> action)
        {
            await ShowEditor(catalogo, false, action);

        }

        protected override async Task ShowDeleteEditor(CatalogoInsUpd catalogo, Func<Task> action)
        {
            await ShowEditor(catalogo, true, action);
        }

        protected async Task UpdateList() => await GetCatalogos(new BaseCatalogoGetParams());
        protected override async Task<IEnumerable<CatalogoInsUpd>> GetCatalogos(BaseCatalogoGetParams getParam)
        {
            var Catalogos = await GetService.Get(getParam);
            return Catalogos;
        }

    }

}
