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
    public partial class CreateOrEdit : BaseForCreateOrEdit
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

        string clasificacionSelected = string.Empty;
        private bool IsShowInmobiliario { get; set; } //= false;
        private bool IsShowMobiliario { get; set; } //= false;
        //Parametros
        //[Parameter]
        public Garantia Garantia { get; set; }
        [Parameter]
        public int idgarantia { get; set; }

        GarantiaGetParams SearchGarantia { get; set; } = new GarantiaGetParams();
        private int _SelectedIdClasificacionGarantia { get; set; } = 1;

        private int SelectedIdClasificacionGarantia { get { return _SelectedIdClasificacionGarantia; } set { _SelectedIdClasificacionGarantia = value; selectedRadioClasificacion(new ChangeEventArgs { Value = 1 }); } }
        void Clear() => garantias = null;
        //ViewDatas
        private string SelectedLocalidad { get; set; }
        private int SelectedIdMarca { get; set; }
        public IEnumerable<Imagen> FotosGarantias { get; set; }
        //protected override void OnInitialized()
        //{
        //    base.OnInitialized();
        //    this.Garantia = new Garantia();
        //}
        protected override async Task OnInitializedAsync()
        {

            this.Garantia = new Garantia();
            tipogarantia = await GarantiasService.GetTipoGarantia(new TipoGarantiaGetParams());
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
                //garantias = await GarantiasService.GetWithPrestamo(new BuscarGarantiaParams { IdNegocio = 1, Search = "" });
                this.Garantia = new Garantia { IdClasificacion = (int)TiposClasificacionGarantia.Mobiliaria };
                var changeEvent = new ChangeEventArgs();
                changeEvent.Value = Garantia.IdClasificacion;
                selectedRadioClasificacion(changeEvent);
            }
            //StateHasChanged();

        }

        async Task SaveGarantia()
        {
            await BlockPage();
            this.Garantia.DetallesJSON = this.detalleGarantia;
            await GarantiasService.SaveGarantia(this.Garantia);
            await SweetMessageBox("Guardado Correctamente", "success", "/Garantias", 1500);
            await UnBlockPage();
            //await OnGuardarNotification();
            //NavManager.NavigateTo("/Garantias");

        }

        private void selectedRadioClasificacion(ChangeEventArgs args)
        {
            int selected = SelectedIdClasificacionGarantia; //Garantia.IdClasificacion; //args.Value.ToString();
            if (selected == 1)
            {
                IsShowInmobiliario = false;
                IsShowMobiliario = true;
            }
            else if (selected == 2)
            {

                IsShowInmobiliario = true;
                IsShowMobiliario = false;
            }
            //this.detalleGarantia = new DetalleGarantia();
            Garantia.IdClasificacion = selected; //Convert.ToInt32(args.Value.ToString());
            tipogarantia = tipogarantia.Where(m => m.IdClasificacion == Garantia.IdClasificacion);
            StateHasChanged();
            //if (clasificacionSelected == "1")
            //{
            //    IsShowInmobiliario = true;
            //    IsShowMobiliario = false;
            //}
            //else if (clasificacionSelected == "2")
            //{

            //    IsShowInmobiliario = false;
            //    IsShowMobiliario = true;
            //}
            ////this.detalleGarantia = new DetalleGarantia();
            //Garantia.IdClasificacion = Convert.ToInt32(args.Value.ToString());
            //tipogarantia = tipogarantia.Where(m => m.IdClasificacion == Garantia.IdClasificacion);
            //StateHasChanged();

        }

        private string setSelectedClasificacion(int id)
        {
            //var changeEvent = new ChangeEventArgs();
            //changeEvent.Value = id;
            //selectedRadioClasificacion(changeEvent);
            if (Garantia.IdClasificacion == id)
            {
                return "checked";
            }

            return "";
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
        //private void SetImages(IList<string> images)
        //{
        //    this.Garantia.ImagesForGaratia = images;
        //}

        private void SetImages(IList<string> imagen)
        {
            Garantia.ImagesForGaratia = imagen;
            //FilterImagesByGroup();
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

        private async Task Handle_LocalidadSelected(BuscarLocalidad localidad)
        {
            var localidadSeleccionada = localidad;
     
             SelectedLocalidad = localidad.ToString();
            //direccion.IdLocalidad = localidad.IdLocalidad;
            //direccion.SelectedLocalidad = localidad.ToString();
            StateHasChanged();
        }
    }
}
