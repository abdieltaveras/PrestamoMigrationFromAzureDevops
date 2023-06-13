using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Shared;
using MudBlazor;

namespace PrestamoBlazorApp.Pages.Clientes
{
    public partial class ImageDialog : BaseForCreateOrEdit
    {
        [Inject]
        IDialogService DialogService { get; set; }

        [Inject]
        ClientesService clientesService { get; set; }
    }
}
