using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace PrestamoBlazorApp.Pages.Garantias
{
    public partial class CreateOrEdit  : BaseForCreateOrEdit
    {
        // Servicios
        [Inject]
        GarantiasService GarantiasService { get; set; }
        //Listados

        IEnumerable<Garantia> garantias { get; set; } = new List<Garantia>();
        IEnumerable<TipoGarantia> tipogarantia { get; set; } = new List<TipoGarantia>();
        IEnumerable<Marca> marcas { get; set; } = new List<Marca>();
        IEnumerable<Modelo> modelos { get; set; } = new List<Modelo>();
        IEnumerable<Color> colores { get; set; } = new List<Color>();

        DetalleGarantia detalleGarantia = new DetalleGarantia();

        //Parametros
        [Parameter]
        public Garantia Garantia { get; set; }
        [Parameter]
        public int idgarantia { get; set; }
        
        GarantiaGetParams SearchGarantia { get; set; } = new GarantiaGetParams();
        void Clear() => garantias = null;
        //ViewDatas

        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.Garantia = new Garantia();
        }
        protected override async Task OnInitializedAsync()
        {
         

            tipogarantia = await GarantiasService.GetTipoGarantia(new TipoGetParams());
            modelos = await GarantiasService.GetModelosForGarantias(new ModeloGetParams { IdMarca = Garantia.IdMarca });
            //modelos = modelos.Where(m => m.IdMarca == Garantia.IdMarca);
            marcas = await GarantiasService.GetMarcasForGarantia(new MarcaGetParams());
            colores = await GarantiasService.GetColoresForGarantia(new ColorGetParams());
            if (idgarantia > 0)
            {
                var result = await GarantiasService.Get(new GarantiaGetParams { IdGarantia = this.idgarantia });
                var garantia = result.FirstOrDefault();
                this.Garantia = garantia;
                this.detalleGarantia = garantia.DetallesJSON;
                var changeEvent = new ChangeEventArgs();
                changeEvent.Value = garantia.IdClasificacion;
                selectedRadioClasificacion(changeEvent);
            }
            else
            {
                garantias = await GarantiasService.GetWithPrestamo(new BuscarGarantiaParams { IdNegocio = 1, Search = "" });
                this.Garantia = new Garantia { IdClasificacion = 2 };
                var changeEvent = new ChangeEventArgs();
                changeEvent.Value = Garantia.IdClasificacion;
                selectedRadioClasificacion(changeEvent);
            }
            StateHasChanged();
          
        }
       
        async Task SaveGarantia()
        {
            await BlockPage();
            this.Garantia.DetallesJSON = this.detalleGarantia;
            await GarantiasService.SaveGarantia(this.Garantia);
            await SweetMessageBox("Guardado Correctamente", "success", "/Garantias",1500);
            await UnBlockPage();
            //await OnGuardarNotification();
            //NavManager.NavigateTo("/Garantias");

        }
        //void CreateOrEdi(int idGarantia = -1)
        //{
        //    if (idGarantia > 0)
        //    {
        //        this.Garantia = garantias.Where(m => m.IdGarantia == idGarantia).FirstOrDefault();
        //    }
        //    else
        //    {
        //        this.Garantia = new Garantia();
        //    }
        //    JsInteropUtils.ShowModal(jsRuntime, "#ModalCreateOrEdit");
        //}
        private void SetImages(IList<string> images)
        {
            this.Garantia.ImagesForGaratia = images;
        }
        //private void QuitImage(IList<string> images)
        //{
        //    this.Garantia.ImagesForGaratia = images;
        //}
        //private void LoadImages(IList<string> images)
        //{
        //    this.Garantia.ImagesForGaratiaEntrantes = images;
        //}
        void RaiseInvalidSubmit()
        {
            
        }
    }
}
