using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace PrestamoBlazorApp.Pages.Garantias
{
    public partial class CreateOrEdit
    {
        [Parameter]
        public int idgarantia { get; set; }
        [Inject]
        IJSRuntime jsRuntime { get; set; }
        JsInteropUtils JsInteropUtils { get; set; } = new JsInteropUtils();
        [Inject]
        GarantiasService GarantiasService { get; set; }
        IEnumerable<Garantia> garantias { get; set; } = new List<Garantia>();
        IEnumerable<Marca> marcas { get; set; } = new List<Marca>();
        IEnumerable<Modelo> modelos { get; set; } = new List<Modelo>();
        IEnumerable<Color> colores { get; set; } = new List<Color>();
        DetalleGarantia detalleGarantia = new DetalleGarantia();
        [Parameter]
        public Garantia Garantia { get; set; } 
        bool loading = false;
        GarantiaGetParams SearchGarantia { get; set; } = new GarantiaGetParams();
        void Clear() => garantias = null;
        //protected override void OnInitialized()
        //{
        //    base.OnInitialized();


        //}
        protected override async Task OnInitializedAsync()
        {
            this.Garantia = new Garantia();
            garantias = await GarantiasService.GetWithPrestamo(new BuscarGarantiaParams { IdNegocio = 1, Search = "-1"});
            modelos = await GarantiasService.GetModelosForGarantias(new ModeloGetParams());
            modelos = modelos.Where(m => m.IdMarca == Garantia.IdMarca);
            marcas = await GarantiasService.GetMarcasForGarantia();
            colores = await GarantiasService.GetColoresForGarantia();
            if (idgarantia>0)
            {
                var result = await GarantiasService.Get(new GarantiaGetParams { IdGarantia = this.idgarantia });
                var garantia = result.FirstOrDefault();
                this.Garantia = garantia;
                this.detalleGarantia = garantia.DetallesJSON;
            }

        }
        //public async void FillModelosCBB(int id)
        //{

        //}

        //async Task GetGarantias()
        //{
        //    loading = true;
        //    var param = new GarantiaGetParams { IdNegocio = 1,IdGarantia = 1 };
        //    garantias = await GarantiasService.Get(param);
        //    loading = false;
        //}
        
        //async Task GetAll()
        //{
        //    loading = true;
        //    Garantias = await GarantiasService.GetAll();
        //    loading = false;
        //}

        async Task SaveGarantia()
        {
            this.Garantia.IdTipoGarantia = 2;
            this.Garantia.DetallesJSON = this.detalleGarantia;
            await GarantiasService.SaveGarantia(this.Garantia);
        }
        void CreateOrEdi(int idGarantia = -1)
        {
            if (idGarantia > 0)
            {
                this.Garantia = garantias.Where(m => m.IdGarantia == idGarantia).FirstOrDefault();
            }
            else
            {
                this.Garantia = new Garantia();
            }
            JsInteropUtils.ShowModal(jsRuntime, "#ModalCreateOrEdit");
        }
        private void SetImages(IList<string> images)
        {
            this.Garantia.ImagesForGaratia = images;
        }
        void RaiseInvalidSubmit()
        {
            
        }
    }
}
