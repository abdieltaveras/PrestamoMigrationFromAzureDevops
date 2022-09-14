using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Shared.Components.Periodos;
namespace PrestamoBlazorApp.Pages.Periodos
{
    public partial class Periodos : BaseForCreateOrEdit
    {
        [Inject]
        IDialogService DialogService { get; set; }
        [Inject]
        PeriodosService PeriodosService { get; set; }

        
        IEnumerable<Periodo> Periodoss { get; set; } = new List<Periodo>();
        [Parameter]
        public Periodo Periodo { get; set; } = new Periodo();

        private int? _IdSelectedPeriodo =null;

        public int? IdSelectedPeriodo { get { return _IdSelectedPeriodo; } set { _IdSelectedPeriodo = value; Seleccionar(); } }
        private int _idSelectedPeriodoBase=1;

        public int IdSelectedPeriodoBase { get { return _idSelectedPeriodoBase; } set { _idSelectedPeriodoBase = value; SetNombrePeriodo(); } }
        public bool ChkEstatus { get; set; } = true;
        public bool ChkRequiereAutorizacion { get; set; }
        public PeriodoBase PeriodoBase { get; set; }

        private string SearchString1 = "";
        private Periodo SelectedItem1 = null;
        private bool FilterFunc1(Periodo element) => FilterFunc(element, SearchString1);

        private bool ShowDialogCreate { get; set; } = false;
        private DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseOnEscapeKey = true };
        private bool Dense = true, Hover = true, Bordered = false, Striped = false;
        void Seleccionar()
        {
            //SelectedLocalidad = Convert.ToInt32(args.);
            var selected = Periodoss.Where(m => m.idPeriodo == IdSelectedPeriodo).FirstOrDefault();
            Periodo.idPeriodo = selected.idPeriodo;
            IdSelectedPeriodoBase = (int)selected.PeriodoBase;
            
            
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
            SetNombrePeriodo();
        }

        async Task CreateOrEdit(int idPeriodo = -1)
        {
            await BlockPage();
            if (idPeriodo > 0)
            {
                var periodo = await PeriodosService.Get(new PeriodoGetParams { idPeriodo = idPeriodo });
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
            ShowDialog(idPeriodo);
            //await JsInteropUtils.ShowModal(jsRuntime, "#MyModal");
        }
        async Task Save()
        {
            Periodo.IdLocalidadNegocio = 1;
            Periodo.PeriodoBase = (PeriodoBase)IdSelectedPeriodoBase;
            this.Periodo.RequiereAutorizacion = ChkRequiereAutorizacion;
            this.Periodo.Activo = ChkEstatus;
            await BlockPage();
            await Handle_SaveData(async () => await PeriodosService.SavePeriodo(this.Periodo));
           // await PeriodosService.SavePeriodo(this.Periodo);
            await SweetMessageBox("Guardado Correctamente", "success", "");
            //await JsInteropUtils.CloseModal(jsRuntime, "#MyModal");
            ShowDialog();
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
        private bool FilterFunc(Periodo element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.Nombre.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Codigo != null)
            {
                if (element.Codigo.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        void ShowDialog(int idperiodo =-1)
        {
            var parameters = new DialogParameters();
            parameters.Add("IdPeriodo", idperiodo);
            DialogService.Show<CreatePeriodo>("",parameters, dialogOptions);
        }
    }
}
