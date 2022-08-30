﻿using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrestamoBlazorApp.Services;
using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Shared;
using MudBlazor;
using PrestamoBlazorApp.Shared.Components.Reports;
using PrestamoBlazorApp.Shared.Components.Forms;

namespace PrestamoBlazorApp.Pages.Clientes
{
    public partial class Clientes : BaseForList
    {
        [Inject]
        IDialogService DialogService { get; set; }

        [Inject]
        ClientesService clientesService { get; set; }
        ClienteGetParams searchClientes { get; set; } = new ClienteGetParams();
        int totalClientes { get; set; }
        IEnumerable<Cliente> clientes;
        List<SelectClass> lstItemsToSearch { get; set; } = new List<SelectClass>();
        //private string _SearchDataBase { get; set; }

        //private string SearchDataBase { get { return _SearchDataBase; } set { _SearchDataBase = value; searchClientesDatabase(value).GetAwaiter(); } }

        private Cliente SelectedItem1 = null;
        private bool FilterFunc1(Cliente element) => FilterFunc(element, SearchStringTable);
        private bool ShowDialogCreate { get; set; } = false;
        private DialogOptions dialogOptions = new() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseOnEscapeKey = true };
        protected override async Task OnInitializedAsync()
        {
            await Handle_GetDataForList(GetClientes);
            FillOptions();
            await base.OnInitializedAsync();
        }
        private async Task GetClientes()
        {
            clientes = new List<Cliente>();
            this.searchClientes.CantidadRegistrosASeleccionar = 50;
            clientes = await clientesService.GetClientesAsync(this.searchClientes, false);
            totalClientes = clientes.Count();
            StateHasChanged();
        }
        private async Task searchClientesDatabase(string search)
        {
            LoadingTable = true;
            if (search.Length > 2)
            {
                clientes = new List<Cliente>();
                clientes = await clientesService.SearchClientes(search, false);
                totalClientes = clientes.Count();
            }
            else
            {
                this.searchClientes.CantidadRegistrosASeleccionar = 50;
                clientes = await clientesService.GetClientesAsync(this.searchClientes, false);
                totalClientes = clientes.Count();
            }
            LoadingTable = false;
        }
        async void PrintFicha(int idcliente, int reportType)
        {
            await BlockPage();
            var result = await clientesService.ReportFicha(jsRuntime, idcliente, reportType);
            await UnBlockPage();
        }

        private async Task ShowReportGenerator()
        {
            var parameters = new DialogParameters();
            //parameters.Add("IdLocalidad", id);
            var dialog = DialogService.Show<SearchReportGeneric>("", parameters, dialogOptions);
            var result = await dialog.Result;
            if (result.Data != null)
            {
                //if (result.Data.ToString() == "1")
                //{
                    
                //    StateHasChanged();
                //}
            }
        }
        private async Task SelectedOptionToSearch(SelectClass selected)
        {
            var value = selected.Value.ToString();
            var text = selected.Text.ToString();
        }
        private void FillOptions()
        {
            lstItemsToSearch.Add(new SelectClass { Value = "valor1", Text = "texto1" });
            lstItemsToSearch.Add(new SelectClass { Value = "valor2", Text = "texto2" });
            lstItemsToSearch.Add(new SelectClass { Value = "valor3", Text = "texto3" });

        }
        private bool FilterFunc(Cliente element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.NombreCompleto.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Codigo != null)
            {
                if (element.Codigo.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            if (element.TelefonoCasa != null)
            {
                if (element.TelefonoCasa.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            if (element.TelefonoMovil != null)
            {
                if (element.TelefonoMovil.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
            }


            if (element.NoIdentificacion != null)
            {
                if (element.NoIdentificacion.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}
