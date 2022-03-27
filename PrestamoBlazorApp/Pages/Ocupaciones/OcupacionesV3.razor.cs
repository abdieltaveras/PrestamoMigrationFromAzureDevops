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
    public partial class OcupacionesV3 : BaseForCreateOrEdit
    {
        private CatalogoGetParams CatalogoParams { get; set; } = new CatalogoGetParams { NombreTabla = "tblOcupaciones", IdTabla = "idocupacion", 
         };
        private string CatalogName { get; set; } = "Ocupaciones";
    }
}
