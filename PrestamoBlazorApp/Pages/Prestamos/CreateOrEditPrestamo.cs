using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Prestamos
{
    public partial class CreateOrEditPrestamo : BaseForCreateOrEdit
    {
        Prestamo prestamo { get; set; } = new Prestamo();
        [Inject]
        PrestamosService prestamoService { get; set; }

        //el de
        //Moras
        //Clientes
        //Codeudores
        //Clasificacion
        //Intereses
        //Garantias
        //TipoAmortizaciones

        //        en la clasificacion que indique
        //si lleva o no garantia
        //si lleva o no Codeudor

//        igual tendremos tipos de vista de clientes
//Full view
//Basic View


        [Parameter]
        public int idPrestamo { get; set; } = -1;

        private string CodigoCliente { get; set; } = string.Empty;
        private string CodigoCodeudor { get; set; } = string.Empty;
        private string CodigoGarantia { get; set; } = string.Empty;
        private string CodigoPeriodo { get; set; } = string.Empty;
        private string CodigoInteres { get; set; } = string.Empty;
        private string CodigoMora { get; set; } = string.Empty;

        private bool LlevaGastoDeCierre { get; set; } = true;
        private bool SinVencimiento { get; set; } = true;

        string  MontoText { get; set; }
        [Inject]
        ClasificacionesService clasificacionesService { get; set; }

        [Inject]
        TiposMoraService tiposMorasService { get; set; }

        [Inject]
        TasasInteresService tasasInteresService { get; set; }
        [Inject]
        PeriodosService periodosService { get; set; }

        IEnumerable<Clasificacion> Clasificaciones { get; set; } = new List<Clasificacion>();
        IEnumerable<TipoMora> TiposMora { get; set; } = new List<TipoMora>();

        IEnumerable<TasaInteres> TasasDeInteres { get; set; } = new List<TasaInteres>();
        IEnumerable<Periodo> Periodos { get; set; } = new List<Periodo>();


        protected override async Task OnInitializedAsync()
        {
            Clasificaciones = await clasificacionesService.GetClasificacionesAsync(new ClasificacionesGetParams());
            TiposMora = await tiposMorasService.Get(new TipoMoraGetParams());
            TasasDeInteres = await tasasInteresService.Get(new TasaInteresGetParams());
            Periodos = await periodosService.Get(new PeriodoGetParams());
            prestamo.PrestamoNumero = "Nuevo";
            MontoText = "1250000.00";
            
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JsInteropUtils.SetInputMask(jsRuntime);
        }
        async Task SavePrestamo()
        {
            blockSaveButton = true;
            //todo: validationresult https://www.c-sharpcorner.com/UploadFile/20c06b/using-data-annotations-to-validate-models-in-net/
            
            try
            {
                //await prestamoService.SavePrestamo(this.prestamo);
                //await OnGuardarNotification(redirectTo: "/Prestamos");
            }
            catch (Exception e)
            {
                await JsInteropUtils.Notification(jsRuntime, $"Lo siento error al guardar los datos mensaje recibido {e.Message}");
            }

        }

        private decimal decimalValue = 1.1M;
        private NumberStyles style =
            NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign;
        private CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

        private string DecimalValue
        {
            get => decimalValue.ToString("0.000", culture);
            set
            {
                if (Decimal.TryParse(value, style, culture, out var number))
                {
                    decimalValue = Math.Round(number, 3);
                }
            }
        }
    }
}
