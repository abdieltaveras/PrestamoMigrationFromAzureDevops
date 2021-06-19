using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using PcpUtilidades;
using PrestamoBlazorApp.Models;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoEntidades;
using PrestamoValidaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Prestamos
{
    public partial class CreateOrEditPrestamo : BaseForCreateOrEdit
    {
        //private PrestamoConCalculosVM prestamo { get; set; } 

        private PrestamoConCalculosVM prestamo { get; set; }
        private bool ShowSearchGarantia { get; set; } = false;
        [Inject]
        PrestamosService prestamoService { get; set; }
        [Inject]
        ClasificacionesService clasificacionesService { get; set; }

        [Inject]
        TiposMoraService tiposMorasService { get; set; }

        [Inject]
        TasasInteresService tasasInteresService { get; set; }
        [Inject]
        PeriodosService periodosService { get; set; }


        [Inject]
        GarantiasService GarantiasService { get; set; }
        // en la clasificacion que indique
        // si lleva o no garantia
        // si lleva o no Codeudor

        // igual tendremos tipos de vista de clientes
        // Full view
        // Basic View
        [Parameter]
        public int idPrestamo { get; set; } = -1;
        private string CodigoCliente { get; set; } = string.Empty;
        private string CodigoCodeudor { get; set; } = string.Empty;
        private string CodigoGarantia { get; set; } = string.Empty;
        private string CodigoPeriodo { get; set; } = string.Empty;
        private string CodigoInteres { get; set; } = string.Empty;
        private string CodigoMora { get; set; } = string.Empty;
        private bool SinVencimiento { get; set; } = true;
        // private decimal _montoPrestado;
        // decimal MontoPrestado { get { return _montoPrestado; } set { this._montoPrestado = value; OnChangeMontoText(value); } }
        // string FormattedMontoPrestadoText { get; set; }
        

        IEnumerable<Clasificacion> Clasificaciones { get; set; } = new List<Clasificacion>();
        IEnumerable<TipoMora> TiposMora { get; set; } = new List<TipoMora>();

        IEnumerable<TasaInteres> TasasDeInteres { get; set; } = new List<TasaInteres>();
        List<Periodo> Periodos { get; set; } = new List<Periodo>();


        protected override async Task OnInitializedAsync()
        {
            var result = execTimes;
            await Handle_GetData(InitPrestamo);
            //await base.OnInitializedAsync();
        }

        private int execTimes = 0;
        private async Task InitPrestamo()
        {
            execTimes++;
            this.prestamo = new PrestamoConCalculosVM(prestamoService);
            this.loading = true;
            //PeriodoBase.Dia
            

            //this.prestamo.SetServices(this.NotificadorDeMensaje, Clasificaciones, TiposMora, TasasDeInteres, Periodos);

            if (idPrestamo > 0)
            {
                await PrepareData();
                var getResult = await prestamoService.GetConDetallesForUiAsync(idPrestamo);
                //var getResult2 = await prestamoService.GetByIdAsync(idPrestamo);
                if (getResult == null)
                {
                    await SweetMessageBox("Lo siento no encontramos prestamo para su peticion", redirectTo: "/prestamos");
                };
                prestamo = getResult.infoPrestamo.ToJson().ToType<PrestamoConCalculosVM>();
                updateInfoCliente(getResult.infoCliente);
                updateInfoGarantia(getResult.infoGarantias);
            }
            else
            {
                await CreateNewPrestamo();
                //prestamo.SetServices(this.NotificadorDeMensaje, Clasificaciones, TiposMora, TasasDeInteres, Periodos);

                //await prestamoCalculo.UpdatePrestamoCalculo();
            }


            // var result = prestamoService.CalcularCuotas(prestamo);

            this.loading = false;
        }

        private async Task PrepareData()
        {
            Clasificaciones = await clasificacionesService.Get(new ClasificacionesGetParams());
            TiposMora = await tiposMorasService.Get(new TipoMoraGetParams());
            TasasDeInteres = await tasasInteresService.Get(new TasaInteresGetParams());
            TasasDeInteres = TasasDeInteres.ToList().OrderBy(ti => ti.InteresMensual);
            Periodos = (await periodosService.Get(new PeriodoGetParams())).ToList();
        }

        private async Task  CreateNewPrestamo()
        {
            this.prestamo = new PrestamoConCalculosVM(prestamoService);
            await PrepareData();
            this.prestamo.PrestamoNumero = "Nuevo";
            this.prestamo.IdClasificacion = Clasificaciones.FirstOrDefault().IdClasificacion;
            this.prestamo.IdTipoAmortizacion = (int)TiposAmortizacion.No_Amortizable_cuotas_fijas;
            this.prestamo.IdPeriodo = Periodos.FirstOrDefault().idPeriodo;
            this.prestamo.Periodo = Periodos.FirstOrDefault();
            this.prestamo.IdTipoMora = TiposMora.FirstOrDefault().IdTipoMora;
            this.prestamo.IdTasaInteres = TasasDeInteres.FirstOrDefault().idTasaInteres;
            var tasaInteresPorPeriodo = await tasasInteresService.GetTasaInteresPorPeriodo(prestamo.IdPeriodo, prestamo.IdTasaInteres); 
            prestamo.TasaDeInteresDelPeriodo = tasaInteresPorPeriodo.InteresDelPeriodo;
            await setParametros.ForPrestamo(this.prestamo);
            //prestamo.ActivateCalculos();
            //await prestamo.Calcular();
            await Calcular();
            //if (prestamo.Cuotas.Count > 0)
            //{
            //    this.Cuotas.Clear();
            //    this.Cuotas.AddRange(prestamo.Cuotas);
            //}
        }


        public async Task Calcular()
        {
            await CalcularGastoDeCierre();
            await CalcularCuotas();
            this.prestamo.FechaVencimiento = this.Cuotas.Last().Fecha;
        }

        private async Task CalcularGastoDeCierre()
        {
            prestamo.MontoGastoDeCierre = prestamo.LlevaGastoDeCierre ? prestamo.MontoPrestado * (prestamo.InteresGastoDeCierre / 100) : 0;
        }
        private async Task CalcularCuotas()
        {
            IEnumerable<Cuota> cuotas = new List<Cuota>();
            try
            {
                var prestamo = this.prestamo.ToJson().ToType<Prestamo>();
                cuotas = await prestamoService.GenerarCuotas(prestamo);
            }
            catch (Exception e)
            {
                await SweetMessageBox(e.Message, delayMilliSeconds: 5000);
            }
            
            this.Cuotas.Clear();
            this.Cuotas = cuotas.ToList();
            //await JsInteropUtils.NotifyMessageBox(jsRuntime,"calculando cuotas"+cuotas.Count().ToString());

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (prestamo.AcomodarFechaALasCuotas)
            {
                await SweetMessageBox("Aun no permito trabajar con prestamos acomodando cuotas", redirectTo: "/prestamos");
            }
            if (firstRender)
            {
                render = true;
                //this.prestamo.ActivateCalculos();
                if (idPrestamo <= 0)
                {
                    await CreateNewPrestamo();
                }
                //await prestamo.Calcular();
                this.prestamo.ProyectarPrimeraYUltima = true;
                //await JsInteropUtils.SetInputMask(jsRuntime);
            }


        }


        private void NotificadorDeMensaje(object sender, string e)
        {
            NotifyMessageBox(e);
        }

        private List<Cuota> Cuotas { get; set; } = new List<Cuota>();

        private string InfoCuotas()
        {
            decimal montoCuota = 0;
            if (prestamo.TipoAmortizacion == TiposAmortizacion.No_Amortizable_cuotas_fijas)
            {
                var valorCta = Cuotas.Where(cta => cta.Numero == 1).FirstOrDefault().TotalOrig;
                montoCuota = Cuotas != null ? valorCta : 0;
            }
            var result = $"{prestamo.CantidadDePeriodos} - {prestamo.Periodo.Nombre} por valor de {montoCuota.ToString("C")}";
            return result;
        }
        


        async Task SavePrestamo()
        {
            //todo: validationresult https://www.c-sharpcorner.com/UploadFile/20c06b/using-data-annotations-to-validate-models-in-net/
            var result = Validaciones.ForPrestamo001().Validate(prestamo);
            var validacionesFallidas = result.Where(item => item.Success == false);
            var MensajesValidacionesFallida = string.Join(", ", validacionesFallidas.Select((item, i) =>  (i+1)+"-"+item.Message+Environment.NewLine));
            if (validacionesFallidas.Count() > 0)
            {
                await SweetMessageBox("Se han encontrado errores"+Environment.NewLine + MensajesValidacionesFallida, "error", "", 5000);
                return;
            }
            try
            {
                Prestamo pr = this.prestamo;
                await Handle_SaveData(async () => await prestamoService.SavePrestamo(pr));
            }
            catch (ValidationObjectException e)
            {
                await JsInteropUtils.NotifyMessageBox(jsRuntime, $"Lo siento error al guardar los datos mensaje recibido {e.Message}");
            }

        }
        
        private async Task Test(MouseEventArgs mouseEventArgs)
        {
            await NotifyMessageBox("ejecutando Test");
            //JsInteropUtils.NotifyMessageBox
            await JsInteropUtils.ConsoleLog(jsRuntime, prestamo);
            var pr = this.prestamo;
            Console.WriteLine(prestamo);
        }



        private async Task UpdateLlevaGastoDeCierre(ChangeEventArgs arg)
        {

            prestamo.InteresGastoDeCierre = prestamo.LlevaGastoDeCierre ? 0 : 10;
            //await prestamo.ExecCalcs();
        }




        int IdGarantiaSelected { get; set; }
        List<infoGarantia> InfoGarantias { get; set; } = new List<infoGarantia>();

        
        private async Task ActivateSearchGarantia()
        {
            ShowSearchGarantia = true;
        }

        bool render = false;
        protected override bool ShouldRender()
        {
            return render;
        }

        private async Task UpdateGarantiaSelected(int idGarantia)
        {
            this.ShowSearchGarantia = false;
            if (prestamo.IdGarantias.Exists(val => val == idGarantia))
            {
                await SweetMessageBox("Esta garantia ya fue añadida");
                return;
            }
            //todo que la garantia valide si permite usarse en mas de 1 prestamo
            // pero pense cambiar la forma, que dicho permiso se otorgue especificante par auna determinada peticion
            // lo que deseo hacer en el sistema es que se genere un one time petition y diga peticion
            // se le autorice al usuario para el proceso que desea y que se venza en cierto tiempo
            // esto es una posiblidad luego de consumida ya expira
            // dicha solicitud puede tener un boton que diga revisar estatus solicitud
            // en espera, aceptada, rechazada, expirada

            var prestamosVinculados = await GarantiasService.GetPrestamosVigentesForGarantia(idGarantia);
            if (prestamosVinculados.Count()>0)
            {
                
                var prestamosForGarantia = string.Join(",", prestamosVinculados.Select(elem => elem.prestamoNumero));
                await SweetMessageBox("garantia no aceptadad porque esta vinculada con otro(s) prestamo "+prestamosForGarantia,delayMilliSeconds:5000);
                return;
            }
            
            this.IdGarantiaSelected = idGarantia;
            
            //await NotifyMessageBox("garantia seleccionada " + idGarantia);
            var Garantias = await GarantiasService.GetGarantias(new GarantiaGetParams { IdGarantia = idGarantia });
            var garantia = Garantias.FirstOrDefault();
            updateInfoGarantia(garantia);
        }

        private void updateInfoGarantia(GarantiaConMarcaYModelo garantia)
        {
            CodigoGarantia = garantia.NoIdentificacion;

            this.prestamo.IdGarantias.Add(garantia.IdGarantia);
            var infoG = $"{garantia.NombreMarca} {garantia.NombreModelo} {garantia.DetallesJSON.Ano} {garantia.NombreColor}  placa {@garantia.DetallesJSON.Placa} matricula {@garantia.DetallesJSON.Matricula}";
            var infoGarantia = new infoGarantia { IdGarantia = garantia.IdGarantia, Text = infoG };
            InfoGarantias.Add(infoGarantia);
        }

        private void updateInfoGarantia(IEnumerable<InfoGarantiaDrCr> garantias)
        {
            prestamo.IdGarantias.Clear();
            foreach (var garantia in garantias)
            {
                CodigoGarantia = garantia.NumeracionGarantia;
                this.prestamo.IdGarantias.Add(garantia.IdGarantia);
                var infoG = $"{garantia.NombreMarca} {garantia.NombreModelo} {garantia.GetDetallesGarantia().Ano} {garantia.GetDetallesGarantia().Color}  placa {@garantia.GetDetallesGarantia().Placa} matricula {@garantia.GetDetallesGarantia().Matricula}";
                var infoGarantia = new infoGarantia { IdGarantia = garantia.IdGarantia, Text = infoG };
                InfoGarantias.Add(infoGarantia);
            }
        }

        private async Task RemoveGarantia(int idGarantia)
        {
            //var result = await JsInteropUtils.Confirm(jsRuntime, "Desea realmente quitar esta garantia");
            //var result2 = await JsInteropUtils.SweetConfirm(jsRuntime, "Quitar Garantia", "Desea realmente quitar esta garantia");
            //await NotifyMessageBox(result2.ToString());
            var result = await JsInteropUtils.SweetConfirmWithIcon(jsRuntime, "Quitar Garantia","Desea realmente quitar esta garantia");
            if (result)
            {
                prestamo.IdGarantias.RemoveAll(value => value == idGarantia);
                InfoGarantias.RemoveAll(gar => gar.IdGarantia == idGarantia);
            }
        }

        int IdClienteSelected { get; set; }
        bool ShowSearchCliente { get; set; }
        string InfoCliente { get; set; }
        [Inject]
        ClientesService clientesService { get; set; }

        private async Task ActivateSearchCliente()
        {
            InfoCliente = string.Empty;
            ShowSearchCliente = true;
        }
        private async Task UpdateClienteSelected(int idCliente)
        {
            this.ShowSearchCliente = false;
            this.IdClienteSelected = idCliente;
            //await NotifyMessageBox("garantia seleccionada " + idGarantia);
            var clientes = await clientesService.GetClientesAsync(new ClienteGetParams { IdCliente = idCliente });
            var cliente = clientes.FirstOrDefault();
            updateInfoCliente(cliente);
        }

        private void updateInfoCliente(Cliente cliente)
        {
            CodigoCliente = cliente.Codigo;
            this.prestamo.IdCliente = cliente.IdCliente;
            InfoCliente = $"{cliente.NoIdentificacion} {cliente.NombreCompleto } ";
        }

        private void updateInfoCliente(InfoClienteDrCr cliente)
        {
            CodigoCliente = cliente.CodigoCliente;
            this.prestamo.IdCliente = cliente.IdCliente;
            InfoCliente = $"{cliente.NumeracionDocumentoIdentidad} {cliente.NombreCompleto} ";
        }

        private async Task OnCloseSearchCliente() => ShowSearchCliente = false;
        private async Task OnCloseSearchGarantia() => ShowSearchGarantia = false;

        private bool LlevaGarantia() => false;
    }
    class infoGarantia
    {
        public int IdGarantia { get; set; }
        public string Text { get; set; }

        public override string ToString() => Text;
        
    }

}
