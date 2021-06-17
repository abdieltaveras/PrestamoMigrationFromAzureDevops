using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Periodos
{
    public partial class Periodos : BaseForCreateOrEdit
    {
        [Inject]
        PeriodosService PeriodosService { get; set; }
        IEnumerable<Periodo> Periodoss { get; set; } = new List<Periodo>();
        [Parameter]
        public Periodo Periodo { get; set; } = new Periodo();

        private int? _SelectedPeriodo = null;

        public int? SelectedPeriodo { get { return _SelectedPeriodo; } set { _SelectedPeriodo = value; Seleccionar(); } }
        public bool ChkEstatus { get; set; } = true;
        public bool ChkRequiereAutorizacion { get; set; }
        public PeriodoBase PeriodoBase { get; set; }
        private void Seleccionar()
        {
            //SelectedLocalidad = Convert.ToInt32(args.);
            var selected = Periodoss.Where(m => m.idPeriodo == SelectedPeriodo).FirstOrDefault();
            Periodo.idPeriodo = selected.idPeriodo;
           // Periodo.IdPeriodoBase;
            //OnLocalidadSelected.InvokeAsync(selected);

        }
        protected override void OnInitialized()
        {
            base.OnInitialized();

            this.Periodo = new Periodo();
        }
        protected override async Task OnInitializedAsync()
        {
            await GetData();
        }

        async Task CreateOrEdit(int idPeriodo = -1)
        {
            await BlockPage();
            if (idPeriodo > 0)
            {
                var periodo = await PeriodosService.Get(new PeriodoGetParams { idPeriodo = idPeriodo });
                this.Periodo = periodo.FirstOrDefault();
                this.SelectedPeriodo = Periodo.IdPeriodoBase;
                this.ChkEstatus = this.Periodo.Activo;
                this.ChkRequiereAutorizacion = this.Periodo.RequiereAutorizacion;
            }
            else
            {
                this.Periodo = new Periodo();
            }
            await UnBlockPage();
            await JsInteropUtils.ShowModal(jsRuntime, "#MyModal");
        }
        async Task SavePriodo()
        {
            Periodo.IdNegocio = 1;
            Periodo.IdLocalidadNegocio = 1;
            Periodo.Usuario = "Luis";
            this.Periodo.RequiereAutorizacion = ChkRequiereAutorizacion;
            this.Periodo.Activo = ChkEstatus;
            await BlockPage();
            await Handle_SaveData(async () => await PeriodosService.SavePeriodo(this.Periodo));
           // await PeriodosService.SavePeriodo(this.Periodo);
            await SweetMessageBox("Guardado Correctamente", "success", "");
            await JsInteropUtils.CloseModal(jsRuntime, "#MyModal");
            await GetData();
            await UnBlockPage();
        }
        async Task GetData()
        {
            Periodoss = await PeriodosService.Get(new PeriodoGetParams());
        }
    }
}
