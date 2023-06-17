using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Negocios
{
    public partial class CreateOrEditNegocios
    {
        [Parameter]
        public int IdNegocio { get; set; }
    }
}
