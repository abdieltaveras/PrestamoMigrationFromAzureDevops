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
        protected override string CatalogoName() => "Ocupaciones";

        private void ShowEditor(CatalogoInsUpd catalogo, bool usarFormularioParaEliminar=false)
        {
            var parameters = new DialogParameters();
            parameters.Add("Catalogo", catalogo);
            parameters.Add("UsarFormularioParaEliminar", usarFormularioParaEliminar);
            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };
            Dialog.Show<CatalogoEditor>("Editar", parameters, options);
        }
        protected override void ShowAddEditor(CatalogoInsUpd catalogo)
        {
            ShowEditor(catalogo);
        }

        protected override void  ShowEditEditor(CatalogoInsUpd catalogo)
        {
            ShowEditor(catalogo);
        }

        protected override void ShowDeleteEditor(CatalogoInsUpd catalogo)
        {
            ShowEditor(catalogo,true);
         
        }

        
        protected override async  Task<IEnumerable<CatalogoInsUpd>> GetCatalogos(BaseCatalogoGetParams getParam)
        {
            var Catalogos = await CatalogosService.Get2(getParam);
            return Catalogos;
        }

        
        
    }
}
