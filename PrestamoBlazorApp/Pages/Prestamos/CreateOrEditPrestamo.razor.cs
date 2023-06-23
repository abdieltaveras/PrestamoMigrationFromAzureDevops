﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using PcpUtilidades;
using PrestamoBlazorApp.Models;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoBlazorApp.Shared.Components.Clientes;
using PrestamoBlazorApp.Shared.Components.Garantias;
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
        [Inject]
        IDialogService DialogService { get; set; }

        [Inject]
        protected SetParametrosService setParametros { get; set; }
        private Prestamo prestamo { get; set; }

        private bool? _LlevaGastoCierre { get; set; }
        private bool? LlevaGastoCierre { get { return _LlevaGastoCierre;  } set { _LlevaGastoCierre = value; } }
        //private Prestamo prestamo { get; set; }
        //private bool ShowSearchGarantia { get; set; } = false;
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

        GarantiaConMarcaYModelo GarantiaSelected { get; set; } = new GarantiaConMarcaYModelo();
        Cliente ClienteSelected { get; set; } = new Cliente();
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

        Clasificacion ClasificacionSelected { get; set; } = new Clasificacion();
        private DateTime? FechaEmisionReal { get; set; }

        //private string MontoPrestado { get; set; }


        private async Task callCalcular()
        {
            await Calcular();
        }
        // private decimal _montoPrestado;
        // decimal MontoPrestado { get { return _montoPrestado; } set { this._montoPrestado = value; OnChangeMontoText(value); } }
        // string FormattedMontoPrestadoText { get; set; }
        IEnumerable<Clasificacion> Clasificaciones { get; set; } = new List<Clasificacion>();
        IEnumerable<TipoMora> TiposMora { get; set; } = new List<TipoMora>();

        IEnumerable<TasaInteres> TasasDeInteres { get; set; } = new List<TasaInteres>();
        IEnumerable<Periodo> Periodos { get; set; } = new List<Periodo>();

        protected override async Task OnInitializedAsync()
        {
            var result = execTimes;
            //this.prestamo = new Prestamo();
            //await PrepareData();
            GarantiaSelected = new GarantiaConMarcaYModelo();
            await Handle_GetData(InitPrestamo);
        }


        private int execTimes = 0;
        private async Task InitPrestamo()
        {
            execTimes++;
            this.prestamo = new Prestamo();
            await PrepareData();
            if (idPrestamo > 0)
            {
                var getResult = await prestamoService.GetConDetallesForUiAsync(idPrestamo);
                //var getResult2 = await prestamoService.GetByIdAsync(idPrestamo);
                if (getResult == null)
                {
                };
                prestamo = getResult.infoPrestamo;
                await OnClasificacionChange(new ChangeEventArgs { Value= prestamo.IdClasificacion });

                updateInfoCliente(getResult.infoCliente);
                updateInfoGarantia(getResult.infoGarantias);
            }
            else
            {
                await CreateNewPrestamo();
                //prestamo.SetServices(this.NotificadorDeMensaje, Clasificaciones, TiposMora, TasasDeInteres, Periodos);

                //await prestamoCalculo.UpdatePrestamoCalculo();
            }
            this.prestamo.ProyectarPrimeraYUltima = true;
            SetPeriodo();
            await SetTasaDeInteresDelPeriodo();
            SetFechaEmisionReal();
            await Calcular();
            // var result = prestamoService.CalcularCuotas(prestamo);

        }

        private void SetFechaEmisionReal()
        {
            this.FechaEmisionReal = prestamo.FechaEmisionReal; //prestamo.FechaEmisionReal.ToString("yyyy-MM-dd"); comentado por luis
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (prestamo.AcomodarFechaALasCuotas)
            {
            }
            if (firstRender)
            {

                if (idPrestamo <= 0)
                {
                    //await CreateNewPrestamo();
                }
            }
            //await JsInteropUtils.SetInputMaskByElemId(jsRuntime,"MontoPrestado","");
        }

        /// <summary>
        /// To get data from services for Clasificaciones, tiposMora, tasasDeInteres, etc.
        /// </summary>
        /// <returns></returns>
        private async Task PrepareData()
        {
            
            Clasificaciones = await clasificacionesService.Get(new ClasificacionesGetParams());
            TiposMora = await tiposMorasService.Get(new TipoMoraGetParams());
            TasasDeInteres = await tasasInteresService.Get(new TasaInteresGetParams());
            TasasDeInteres = TasasDeInteres.ToList().OrderBy(ti => ti.InteresMensual);
            Periodos = (await periodosService.Get(new PeriodoGetParams())).ToList();
        }

        private async Task CreateNewPrestamo()
        {
            this.prestamo.PrestamoNumero = "Nuevo";

            this.prestamo.IdClasificacion = Clasificaciones.FirstOrDefault().IdClasificacion;
            ClasificacionSelected = Clasificaciones.FirstOrDefault();
            this.prestamo.TipoAmortizacion =TiposAmortizacion.No_Amortizable_cuotas_fijas;
            this.prestamo.IdPeriodo = Periodos.FirstOrDefault().idPeriodo;

            this.prestamo.IdTipoMora = TiposMora.FirstOrDefault().IdTipoMora;
            this.prestamo.IdTasaInteres = TasasDeInteres.FirstOrDefault().idTasaInteres;
            SetPeriodo();
            await SetTasaDeInteresDelPeriodo();
            await setParametros.ForPrestamo(this.prestamo);
        }


        private async Task SetTasaDeInteresDelPeriodo()
        {
            var tasaInteresPorPeriodo = await tasasInteresService.GetTasaInteresPorPeriodo(prestamo.IdPeriodo, prestamo.IdTasaInteres);
            prestamo.TasaDeInteresDelPeriodo = tasaInteresPorPeriodo.InteresDelPeriodo;
        }

        public void SetPeriodo()
        {
            this.prestamo.Periodo = Periodos.Where(pr => pr.idPeriodo == prestamo.IdPeriodo).FirstOrDefault();
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
            IEnumerable<CxCCuota> cuotas = new List<CxCCuota>();
            try
            {
                cuotas = await prestamoService.GenerarCuotas(this.prestamo);
            }
            catch (Exception e)
            {
            }
            this.Cuotas.Clear();
            this.Cuotas = cuotas.ToList();
            //await JsInteropUtils.NotifyMessageBox(jsRuntime,"calculando cuotas"+cuotas.Count().ToString());

        }



        private void NotificadorDeMensaje(object sender, string e)
        {
        }

        private List<CxCCuota> Cuotas { get; set; } = new List<CxCCuota>();

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
            var MensajesValidacionesFallida = string.Join(", ", validacionesFallidas.Select((item, i) => (i + 1) + "-" + item.Message + Environment.NewLine));
            if (validacionesFallidas.Count() > 0)
            {
                return;
            }
            try
            {
                Prestamo pr = this.prestamo;
                await Handle_SaveData(async () => await prestamoService.SavePrestamo(pr));
            }
            catch (ValidationObjectException e)
            {
                //await JsInteropUtils.NotifyMessageBox(jsRuntime, $"Lo siento error al guardar los datos mensaje recibido {e.Message}");
            }

        }

        private async Task Test(MouseEventArgs mouseEventArgs)
        {
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
            //ShowSearchGarantia = true;
        }


        private async Task UpdateGarantiaSelected(int idGarantia)
        {
            //this.ShowSearchGarantia = false;
            //if (prestamo.IdGarantias.Exists(val => val == idGarantia))
            //{
            //    return;
            //}
            ////todo que la garantia valide si permite usarse en mas de 1 prestamo
            //// pero pense cambiar la forma, que dicho permiso se otorgue especificante par auna determinada peticion
            //// lo que deseo hacer en el sistema es que se genere un one time petition y diga peticion
            //// se le autorice al usuario para el proceso que desea y que se venza en cierto tiempo
            //// esto es una posiblidad luego de consumida ya expira
            //// dicha solicitud puede tener un boton que diga revisar estatus solicitud
            //// en espera, aceptada, rechazada, expirada

            //var prestamosVinculados = await GarantiasService.GetPrestamosVigentesForGarantia(idGarantia);
            //if (prestamosVinculados.Count() > 0)
            //{

            //    var prestamosForGarantia = string.Join(",", prestamosVinculados.Select(elem => elem.prestamoNumero));
            //    return;
            //}

            //this.IdGarantiaSelected = idGarantia;

            ////await NotifyMessageBox("garantia seleccionada " + idGarantia);
            //var Garantias = await GarantiasService.GetGarantias(new GarantiaGetParams { IdGarantia = idGarantia });
            //var garantia = Garantias.FirstOrDefault();
            //updateInfoGarantia(garantia);
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
            //var result = await JsInteropUtils.SweetConfirmWithIcon(jsRuntime, "Quitar Garantia", "Desea realmente quitar esta garantia");
            //if (result)
            //{
            //    prestamo.IdGarantias.RemoveAll(value => value == idGarantia);
            //    InfoGarantias.RemoveAll(gar => gar.IdGarantia == idGarantia);
            //}
        }

        int IdClienteSelected { get; set; }
        //bool ShowSearchCliente { get; set; }
        string InfoCliente { get; set; }
        [Inject]
        ClientesService clientesService { get; set; }


        private async Task ActivateSearchCliente()
        {
            InfoCliente = string.Empty;
            //ShowSearchCliente = true;
        }
        private async Task UpdateClienteSelected(int idCliente)
        {
            //this.ShowSearchCliente = false;
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

        //private void OnCloseSearchCliente() => ShowSearchCliente = false;
        //private void OnCloseSearchGarantia() => ShowSearchGarantia = false;

        private bool LlevaGarantia() => ClasificacionSelected.RequiereGarantia; 

        protected async Task OnMontoPrestadoChange(ChangeEventArgs args)
        {
            
            var value = Convert.ToDecimal(args.Value.ToString().RemoveAllButNumber());
            this.prestamo.MontoPrestado = value;
            await Calcular();
        }

        protected async Task OnLlevaGastoDeCierreChange(ChangeEventArgs args)
        {
            var value = Convert.ToBoolean(args.Value);
            prestamo.InteresGastoDeCierre = value ? 10 : 0;
            await Calcular();
        }

        protected async Task OnGastoDeCierreEsDeducibleChange(ChangeEventArgs args)
        {
            var value = Convert.ToBoolean(args.Value);
            prestamo.GastoDeCierreEsDeducible = value;
            await Calcular();
        }

        protected async Task SetFecha(DateTime fecha) 
        { 
        }
        protected async Task OnFechaEmisionRealChange(ChangeEventArgs args)
        {
            var value = DateTime.Now;
            var convertionSucceed = DateTime.TryParse(args.Value.ToString(), out value);
            if (value > DateTime.Now )
            {
                SetFechaEmisionReal();
            }
            if (convertionSucceed)
            {
                prestamo.FechaEmisionReal = value;
                await Calcular();
            }
            else
            {
                SetFechaEmisionReal();
            }

        }

        protected async Task OnClasificacionChange(ChangeEventArgs args)
        {
            var value = Convert.ToInt32(args.Value);
            prestamo.IdClasificacion = value;
            ClasificacionSelected = Clasificaciones.First(cl => cl.IdClasificacion == value);
            prestamo.LlevaGarantia = ClasificacionSelected.RequiereGarantia;
        }

        protected async Task OnFinanciarGastoDeCierreChange(ChangeEventArgs args)
        {
            var value = Convert.ToBoolean(args.Value);
            prestamo.FinanciarGastoDeCierre = value;
            await Calcular();
            StateHasChanged();
        }

        protected async Task OnInteresGastoDeCierreChange(ChangeEventArgs args)
        {
            var value = Convert.ToDecimal(args.Value);
            prestamo.InteresGastoDeCierre = value;
            if (value > 0)
            {
                LlevaGastoCierre = true;
            }
            await Calcular();
            StateHasChanged();
        }

        protected async Task OnCantidadPeriodoChange(ChangeEventArgs args)
        {
            var value = Convert.ToInt32(args.Value);
            prestamo.CantidadDePeriodos = value;
            await Calcular();
        }

        protected async Task OnPeriodoChange(ChangeEventArgs args)
        {
            var value = Convert.ToInt32(args.Value);
            prestamo.IdPeriodo = value;
            SetPeriodo();
            await SetTasaDeInteresDelPeriodo();
            await Calcular();
        }
        protected async Task OnTasaInteresChange(ChangeEventArgs args)
        {
            var value = Convert.ToInt32(args.Value);
            prestamo.IdTasaInteres = value;
            await SetTasaDeInteresDelPeriodo();
            await Calcular();
        }
        private async Task ShowSearchGarantia()
        {
            //string[] cols = { "Nombres", "Apellidos" };
            var parameters = new DialogParameters { };
            DialogOptions dialogOptions = new DialogOptions { MaxWidth = MaxWidth.Medium, FullWidth = true, CloseButton = true };
            var dialog = DialogService.Show<SearchGarantiaByProperty>("Seleccionar Garantia", parameters, dialogOptions);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {

                GarantiaSelected = (GarantiaConMarcaYModelo)result.Data;
            }
        }
        private async Task ShowSearchCliente()
        {
            string[] cols = { "Nombres", "Apellidos" };
            var parameters = new DialogParameters { ["Columns"] = cols };
            DialogOptions dialogOptions = new DialogOptions { MaxWidth = MaxWidth.Medium, FullWidth = true, CloseButton = true };
            var dialog = DialogService.Show<SearchClientesByProperty>("Seleccionar Cliente", parameters, dialogOptions);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                ClienteSelected = (Cliente)result.Data;
            }
        }
    }
    class infoGarantia
    {
        public int IdGarantia { get; set; }
        public string Text { get; set; }
        public override string ToString() => Text;

    }

}
