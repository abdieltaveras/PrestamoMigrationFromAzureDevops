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
namespace PrestamoBlazorApp.Pages.Ocupaciones
{
    public partial class Ocupaciones : BaseCatalogoComponent
    {
        [Inject] protected IDialogService Dialog { get; set; }
        
        [Inject] protected CatalogosService CatalogosService { get; set; }
        
        
        private async Task ShowEditor(CatalogoInsUpd catalogo, bool usarFormularioParaEliminar=false, Func<Task> action=null)
        {
            var parameters = new DialogParameters(); 
            parameters.Add("Catalogo", catalogo);
            parameters.Add("UsarFormularioParaEliminar", usarFormularioParaEliminar);
            parameters.Add("UpdateList", action);
            var options = new DialogOptions() {  CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };
            //var dlg = new CatalogoEditor(() => GetCatalogos(new BaseCatalogoGetParams());
            Dialog.Show<CatalogoEditor>("Editar", parameters, options);
            //Task showEditor = Task.Run(() => Dialog.Show<CatalogoEditor>("Editar", parameters, options));
            //await GetCatalogos(new BaseCatalogoGetParams());       
            //await showEditor.ContinueWith(antecedent =>  GetCatalogos(new BaseCatalogoGetParams()));

        }
        protected override async Task ShowAddEditor(CatalogoInsUpd catalogo, Func<Task> action)
        {
            await ShowEditor(catalogo, false, action);
        }

        protected override async Task   ShowEditEditor(CatalogoInsUpd catalogo, Func<Task> action)
        {
            await ShowEditor(catalogo, false, action);
            
        }

        protected override async Task ShowDeleteEditor(CatalogoInsUpd catalogo, Func<Task> action)
        {
            await ShowEditor(catalogo, true, action);
        }

        protected async Task UpdateList() => await GetCatalogos(new BaseCatalogoGetParams());
        protected override async  Task<IEnumerable<CatalogoInsUpd>> GetCatalogos(BaseCatalogoGetParams getParam)
        {
            var Catalogos = await CatalogosService.Get2(getParam);
            return Catalogos;
        }
        
    }
}
