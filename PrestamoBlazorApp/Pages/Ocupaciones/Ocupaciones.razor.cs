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


namespace PrestamoBlazorApp.Pages.Ocupaciones
{
    public abstract class BaseCatalogoComponent : BaseForCreateOrEdit
    {
        protected abstract CatalogoGetParams SetTableAndColumnName();
        protected abstract string CatalogoName();

        protected abstract void ShowAddEditor(Catalogo catalogo);

        protected abstract void ShowEditEditor(Catalogo catalogo);

        protected abstract void ShowDeleteEditor(Catalogo catalogo);
    }
    public partial class Ocupaciones : BaseCatalogoComponent
    {
        [Inject] protected IDialogService Dialog { get; set; }
        protected override CatalogoGetParams SetTableAndColumnName() => new CatalogoGetParams
        {
            NombreTabla = "tblOcupaciones",
            IdTabla = "idOcupacion",
        };
        protected override string CatalogoName() => "Ocupaciones";

        private void ShowEditor(Catalogo catalogo, bool usarFormularioParaEliminar=false)
        {
            var parameters = new DialogParameters();
            catalogo.NombreTabla = SetTableAndColumnName().NombreTabla;
            catalogo.IdTabla = SetTableAndColumnName().IdTabla;
            parameters.Add("Catalogo", catalogo);
            parameters.Add("UsarFormularioParaEliminar", usarFormularioParaEliminar);
            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };
            Dialog.Show<CatalogoEditor>("Editar", parameters, options);
        }
        protected override void ShowAddEditor(Catalogo catalogo)
        {
            ShowEditor(catalogo);
        }

        protected override void  ShowEditEditor(Catalogo catalogo)
        {
            ShowEditor(catalogo);
        }

        protected override void ShowDeleteEditor(Catalogo catalogo)
        {
            ShowEditor(catalogo,true);
        }
    }
}
