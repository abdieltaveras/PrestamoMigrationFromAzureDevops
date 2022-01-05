using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Shared;

namespace PrestamoBlazorApp.Pages.Codeudores
{
    public partial class Codeudores : BaseForList
    {
        [Inject]
        CodeudoresService CodeudoresService { get; set; }
        CodeudorGetParams codeudorGetParams { get; set; } = new CodeudorGetParams();
        int totalClientes { get; set; }
        IEnumerable<Codeudor> codeudores;
        protected override async Task OnInitializedAsync()
        {
            await Handle_GetDataForList(GetCodeudores);
            await base.OnInitializedAsync();
        }
        private async Task GetCodeudores()
        {
            codeudores = new List<Codeudor>();
            this.codeudorGetParams.CantidadRegistrosASeleccionar = 50;
            codeudores = await CodeudoresService.GetCodeudoresAsync(this.codeudorGetParams, false);
            totalClientes = codeudores.Count();
        }
    }
}
