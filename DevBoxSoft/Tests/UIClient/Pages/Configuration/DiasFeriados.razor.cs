using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using UIClient.Services;
using DevBox.Core.Classes.Configuration;
using UIClient.Pages.Components;
using MudBlazor;

namespace UIClient.Pages.Configuration
{
    public partial class DiasFeriados : BasePage
    {
        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private IJSRuntime JsRuntime { get; set; }
        [Inject] private DiasFeriadosService diasFeriadosService { get; set; }
        [Inject] private IDialogService Dialog { get; set; }
        enum ViewingStyle { list, calendar, Default = calendar }
        int _year { get; set; } = DateTime.Today.Year;
        public int Year { get { return _year; } set { _year = value; } }

        ViewingStyle viewingStyle { get; set; } = ViewingStyle.Default;
        List<DiaFeriado> dias = new List<DiaFeriado>();
        public Dictionary<DateTime, string> diasFeriados => dias.ToDictionary(d => d.Dia, d => d.Descripcion);
        public Dictionary<string, List<DiaFeriado>> diasFeriadosGrouped => dias.GroupBy(d => d.Dia.ToString("MMMM")).ToDictionary(g => g.Key, g => g.ToList());
        public DiaFeriadoNullable selectedDay { get; set; } = new DiaFeriadoNullable() { Dia = DateTime.Today };

        private string _hubUrl;
        //private HubConnection _hubConnection;
        Dictionary<string, object> messages = new Dictionary<string, object>();

        async Task newDay()
        {
            onDayClick(DateTime.Today, "");
        }
        async void addDefDays() => await saveDays(diasFeriadosService.GetDefDiasFeriados(Year));

        private async Task saveDays(List<DiaFeriado> dias)
        {
            var tasks = new List<Task>();
            dias.ForEach(d => tasks.Add(diasFeriadosService.SaveDiaFeriadoAsync(d.Dia, d.Descripcion)));
            await Task.WhenAll(tasks);
            NotificationsService.Notify($"Días feriados comunes por año {Year} han sido agregados. ", "*", "*", "*", mustReload: true);
        }

        async Task loadDias()
        {
            loading = true;
            var _dias = await diasFeriadosService.GetDiasFeriadosAsync(Year);
            dias = _dias.ToList();
            loading = false;
        }
        protected async override Task OnInitializedAsync()
        {
            try
            {
                //string baseUrl = navigationManager.BaseUri;
                //_hubUrl = baseUrl.TrimEnd('/') + SignalRHub.HubUrl;
                //_hubConnection = new HubConnectionBuilder()
                //    .WithUrl(_hubUrl)
                //    .Build();
                //_hubConnection.On<string, string, object>("Update", ReceiveUpdates);
                //await _hubConnection.StartAsync();
            }
            catch (Exception e)
            {

            }
            await base.OnInitializedAsync();
            await loadDias();

        }
        private async void NotifyUpdates(object entity, string message)
        {
            await SendAsync(message, entity);
        }
        private async Task DisconnectAsync()
        {
            //await _hubConnection.StopAsync();
            //await _hubConnection.DisposeAsync();
            //_hubConnection = null;
        }

        private async Task SendAsync(string message, object target)
        {
            NotificationsService.Notify(message, "*", "*", "*");
        }

        private async void ReceiveUpdates(string entity, string message, object target)
        {
            bool isMine = entity.Equals("diasferiados", StringComparison.OrdinalIgnoreCase);
            if (isMine)
            {
                this.messages.Add(message, target);
                if (message.Contains("actualización"))
                {
                    await loadDias();
                }
                // Inform blazor the UI needs updating
                StateHasChanged();
            }
        }
        Task Alert(object message) => Task.Run(async () => await JsRuntime.InvokeAsync<object>("Alert", new object[] { message }));
        Task ShowModal(string id) => Task.Run(async () => await JsRuntime.InvokeAsync<string>("ShowModal", new string[] { id }));
        Task Reload(bool force) => Task.Run(async () => await JsRuntime.InvokeAsync<bool>("Reload", force));
        bool loading = false;
        public async void onYearChange(int year)
        {
            Year = year;
            await loadDias();
        }

        public void onDayClick(DateTime date, string description)
        {
            selectedDay.Dia = date;
            selectedDay.Descripcion = description;
            StateHasChanged();
            var parameters = new DialogParameters();
            parameters.Add("selectedDay", selectedDay);
            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };
            Dialog.Show<DiaFeriadoEditor>("Editar", parameters, options);
        }
        void notifyError()
        {
            //NotificationsService.NotifyError("Errpipo", "*", "*", "*");
        }
        void notifyCritical()
        {
            //NotificationsService.NotifyError("Errpipo", "*", "*", "*", mustRestart: true, force: true);
        }
    }
}
