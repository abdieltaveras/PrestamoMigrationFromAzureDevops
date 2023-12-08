using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    partial class AreasEducativas
    {
        string titulo => "Áreas Educativas";
        //[Inject] AreasEducativasService areasService { get; set; }
        protected override Task OnInitializedAsync()
        {
            //var areasServices = new AreasEducativasService();
            //var areas = areasService.GetAreaEducativas();
            return base.OnInitializedAsync();
        }
    }
}
