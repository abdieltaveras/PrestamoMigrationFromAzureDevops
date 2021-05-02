using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Prestamos
{
    public partial class CreateOrEditPrestamo : BaseForCreateOrEdit
    {
        Prestamo prestamo { get; set; } = new Prestamo();
        [Inject]
        PrestamosService prestamoService { get; set; }

        //        el de
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

        private string monto { get; set; } = "0.00";
        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
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
