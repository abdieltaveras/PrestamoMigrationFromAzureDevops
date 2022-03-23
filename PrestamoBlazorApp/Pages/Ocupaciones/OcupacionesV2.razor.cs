using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Shared;
namespace PrestamoBlazorApp.Pages.Ocupaciones
{
    public partial class OcupacionesV2 : BaseForCreateOrEdit
    {
        private CatalogoGetParams CatalogoOcupaciones { get; set; } = new CatalogoGetParams { NombreTabla = "tblOcupaciones", IdTabla = "idocupacion" };
    }
}
