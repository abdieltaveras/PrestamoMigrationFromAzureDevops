using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace PrestamoBlazorApp.Pages.Equipos
{
    public partial class Equipos
    {
        ColorGetParams SearchMarca { get; set; } = new ColorGetParams();
        [Inject]
        IJSRuntime jsRuntime { get; set; }

        JsInteropUtils JsInteropUtils { get; set; } = new JsInteropUtils();
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
        protected override async Task OnInitializedAsync()
        {
            equipos = await EquiposService.Get(new EquiposGetParam());
        }
        async Task Get()
        {
            loading = true;
            var param = new EquiposGetParam { IdNegocio = 1 };
            equipos = await EquiposService.Get(param);
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
        void CreateOrEdit(int IdEquipo = -1)
        {
            if (IdEquipo > 0)
            {
                this.Equipo = equipos.Where(m => m.IdEquipo == IdEquipo).FirstOrDefault();
            }
            else
            {
                this.Equipo = new Equipo();
            }
            JsInteropUtils.ShowModal(jsRuntime, "#ModalCreateOrEdit");
        }
        void RaiseInvalidSubmit()
        {
            
        }
    }
}
