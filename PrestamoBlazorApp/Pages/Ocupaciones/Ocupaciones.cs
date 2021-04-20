using PrestamoBLL.Entidades;
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
    public partial class Ocupaciones : BaseForCreateOrEdit
    {
        [Inject]
        IJSRuntime jsRuntime { get; set; }
        [Inject]
        OcupacionesService OcupacionesService { get; set; }
        IEnumerable<Ocupacion> ocupaciones { get; set; } = new List<Ocupacion>();
        [Parameter]
        public Ocupacion Ocupacion { get; set; } 
        bool loading = false;
        void Clear() => ocupaciones = null;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.Ocupacion = new Ocupacion();
        }
        protected override async Task OnInitializedAsync()
        {
            ocupaciones = await OcupacionesService.Get();
        }
        async Task GetOcupaciones()
        {
            loading = true;
            ocupaciones = await OcupacionesService.Get();
            loading = false;
        }

        async Task GetAll()
        {
            loading = true;
            ocupaciones = await OcupacionesService.GetAll();
            loading = false;
        }

        async Task SaveOcupacion()
        {
            await OcupacionesService.SaveOcupacion(this.Ocupacion);
            await OnGuardarNotification();
        }
        void CreateOrEdit(int idOcupacion = -1)
        {
            if (idOcupacion > 0)
            {
                this.Ocupacion = ocupaciones.Where(m => m.IdOcupacion == idOcupacion).FirstOrDefault();
            }
            else
            {
                this.Ocupacion = new Ocupacion();
            }
            JsInteropUtils.ShowModal(jsRuntime, "#ModalCreateOrEdit");
        }


        void RaiseInvalidSubmit()
        {
            
        }
    }
}
