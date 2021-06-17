using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Shared;

namespace PrestamoBlazorApp.Pages.Equipos
{
    public partial class Equipos : BaseForCreateOrEdit
    {
        ColorGetParams SearchMarca { get; set; } = new ColorGetParams();
        [Inject]

        JsInteropUtils JsInteropUtils { get; set; } = new JsInteropUtils();
        [Inject]
        EquiposService EquiposService { get; set; }
        IEnumerable<Equipo> equipos { get; set; } = new List<Equipo>();
        [Parameter]
        public Equipo Equipo { get; set; } 

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
            await BlockPage();
            await EquiposService.SaveEquipo(this.Equipo);
            await UnBlockPage();
            await SweetMessageBox("Guardado Correctamente", "success", "");
        }
        async Task CreateOrEdit(int IdEquipo = -1)
        {
            if (IdEquipo > 0)
            {
                var datos = await EquiposService.Get(new EquiposGetParam { IdEquipo = IdEquipo });
                this.Equipo = datos.FirstOrDefault();
            }
            else
            {
                this.Equipo = new Equipo();
            }
           await JsInteropUtils.ShowModal(jsRuntime, "#ModalCreateOrEdit");
        }
        void RaiseInvalidSubmit()
        {
            
        }
    }
}
