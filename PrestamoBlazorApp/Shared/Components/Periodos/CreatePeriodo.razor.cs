using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Periodos
{
    public partial class CreatePeriodo : BaseForCreateOrEdit
    {
        [Inject]
        PeriodosService PeriodosService { get; set; } 
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public int IdPeriodo { get; set; } = -1;
        //[Parameter]
        public Periodo Periodo { get; set; } = new Periodo();

        IEnumerable<Periodo> Periodoss { get; set; } = new List<Periodo>();
        private int? _IdSelectedPeriodo = null;

        public int? IdSelectedPeriodo { get { return _IdSelectedPeriodo; } set { _IdSelectedPeriodo = value; Seleccionar(); } }
        private int _idSelectedPeriodoBase = 1;

        public int IdSelectedPeriodoBase { get { return _idSelectedPeriodoBase; } set { _idSelectedPeriodoBase = value; SetNombrePeriodo(); } }
        public bool ChkEstatus { get; set; } = true;
        public bool ChkRequiereAutorizacion { get; set; }
        public PeriodoBase PeriodoBase { get; set; }

        private bool ShowDialogCreate { get; set; } = false;
        private DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseOnEscapeKey = true };
        void Seleccionar()
        {
            //SelectedLocalidad = Convert.ToInt32(args.);
            var selected = Periodoss.Where(m => m.idPeriodo == IdSelectedPeriodo).FirstOrDefault();
            Periodo.idPeriodo = selected.idPeriodo;
            IdSelectedPeriodoBase = (int)selected.PeriodoBase;


            // Periodo.IdPeriodoBase;
            //OnLocalidadSelected.InvokeAsync(selected);

        }
        protected override async Task OnInitializedAsync()
        {
            await GetData();
            await CreateOrEdit();
            //SetNombrePeriodo();
        }

        async Task CreateOrEdit()
        {
            await BlockPage();
            if (IdPeriodo > 0)
            {
                var periodo = await PeriodosService.Get(new PeriodoGetParams { idPeriodo = IdPeriodo });
                this.Periodo = periodo.FirstOrDefault();
                this.IdSelectedPeriodo = Periodo.idPeriodo;
                this.ChkEstatus = this.Periodo.Activo;
                this.ChkRequiereAutorizacion = this.Periodo.RequiereAutorizacion;
            }
            else
            {
                this.Periodo = new Periodo();
            }
            await UnBlockPage();

            //await JsInteropUtils.ShowModal(jsRuntime, "#MyModal");
        }
        async Task Save()  
        {
            Periodo.IdNegocio = 1;
            Periodo.IdLocalidadNegocio = 1;
            Periodo.PeriodoBase = (PeriodoBase)IdSelectedPeriodoBase;
            this.Periodo.RequiereAutorizacion = ChkRequiereAutorizacion;
            this.Periodo.Activo = ChkEstatus;
            await BlockPage();
            await Handle_SaveData(async () => await PeriodosService.SavePeriodo(this.Periodo));
            // await PeriodosService.SavePeriodo(this.Periodo);
            await SweetMessageBox("Guardado Correctamente", "success", "");
            await CloseModal(1);
            await GetData();
            await UnBlockPage();
        }

        async Task OnSelectedPeriodoChange()
        {

        }

        string NombrePeriodo { get; set; }
        void SetNombrePeriodo()
        {
            NombrePeriodo = "Ninguno";
            if (IdSelectedPeriodoBase > 0)
            {
                NombrePeriodo = Enum.GetName(typeof(PeriodoBase), IdSelectedPeriodoBase);
            }
        }

        async Task GetData()
        {
            Periodoss = await PeriodosService.Get(new PeriodoGetParams());
        }


        private async Task CloseModal(int result = -1)
        {
            MudDialog.Close(DialogResult.Ok(result));
        }
    }
}
