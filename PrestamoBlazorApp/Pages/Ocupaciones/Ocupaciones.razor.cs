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
    public partial class Ocupaciones : BaseForCreateOrEdit
    {
        [Inject] protected IDialogService Dialog { get; set; }
        private CatalogoGetParams TableAndColumnName { get; set; } = new CatalogoGetParams { NombreTabla = "tblOcupaciones", IdTabla = "idOcupacion", 
         };


        private string CatalogName { get; set; } = "Ocupaciones";

        protected void ShowCatalogoEditor(Catalogo catalogo)
        {
            var parameters = new DialogParameters();
            catalogo.NombreTabla = TableAndColumnName.NombreTabla;
            catalogo.IdTabla = TableAndColumnName.IdTabla;
            parameters.Add("Catalogo", catalogo );
            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };
            Dialog.Show<CatalogoEditor>("Editar", parameters, options);
        }
    }
}
