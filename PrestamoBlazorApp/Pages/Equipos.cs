using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;

namespace PrestamoBlazorApp.Pages
{
    public partial class Equipos
    {
        [Inject]
        EquiposService EquiposService { get; set; }
        IEnumerable<Equipo> equipos { get; set; } = new List<Equipo>();
        [Parameter]
        public Equipo Equipo { get; set; } 
        bool loading = false;
        void Clear() => equipos = null;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.Equipo = new Equipo();
        }

        async Task GetEquiposByParam()
        {
            loading = true;
            var param = new EquiposGetParam { IdNegocio = 1 };
            equipos = await EquiposService.GetEquiposAsync(param);
            loading = false;
        }

        //async Task GetAll()
        //{
        //    loading = true;
        //    Equipos = await EquiposService.GetAll();
        //    loading = false;
        //}

        async Task SaveEquipo()
        {
            await EquiposService.SaveEquipo(this.Equipo);
        }

        void RaiseInvalidSubmit()
        {
            
        }
    }
}
