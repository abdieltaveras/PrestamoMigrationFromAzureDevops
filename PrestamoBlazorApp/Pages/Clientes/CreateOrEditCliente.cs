using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Data;
using PrestamoBlazorApp.Models;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoBLL;
using PrestamoBLL.Entidades;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Pages.Clientes
{
    public partial class CreateOrEditCliente : BaseForCreateOrEdit
    {
        // servicios

        [Inject]
        OcupacionesService ocupacionesService { get; set; }
        [Inject]
        LocalidadesService localidadService { get; set; }
        [Inject]
        ClientesService clientesService { get; set; }

        // parametros
        [Parameter]
        public int idCliente { get; set; }

        // miembros
        string searchSector = string.Empty;

        public Cliente cliente { get; set; } = new Cliente();
        Conyuge conyuge { get; set; } = new Conyuge();

        DireccionModel direccion { get; set; } = new DireccionModel();

        InfoLaboral infoLaboral { get; set; } = new InfoLaboral();

        EventConsole console;
        string TextoForActivo { get; set; } = "Si";
        List<EnumModel> TiposIdentificacionPersonaList { get; set; }

        private IEnumerable<Ocupacion> Ocupaciones { get; set; } = new List<Ocupacion>();

        private async Task<IEnumerable<Ocupacion>> GetOcupaciones()
        {
            var result = await ocupacionesService.GetOcupacionesAsync();
            return result;
        }
        List<Referencia> referencias = new List<Referencia>();


        bool disableCodigo { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            Ocupaciones = await GetOcupaciones();
            prepareModel();
            await base.OnInitializedAsync();
        }

        private async void prepareModel()
        {


            if (idCliente != 0)
            {
                var clientes = await clientesService.GetClientesAsync(new ClienteGetParams { IdCliente = idCliente, ConvertJsonToObj = true });
                this.cliente = clientes.FirstOrDefault();
            }
            

            if (this.cliente == null || idCliente <= 0)
            {
                this.cliente = new Cliente
                {
                    Codigo = "Nuevo",
                    Nombres = "a1",
                    Apellidos = "a2",
                    Apodo = "a3",
                    IdTipoIdentificacion = 1,
                    NoIdentificacion = "000-0000000-1",
                    InfoConyugeObj = new Conyuge { Nombres = "b1", Apellidos = "b2", DireccionLugarTrabajo = "b3" },
                    InfoLaboralObj = new InfoLaboral { Direccion = "d1", Nombre = "d2" },
                    InfoDireccionObj = new Direccion
                    {
                        IdLocalidad = 5,
                        Calle = "Calle emilio prud home #5",
                        Latitud = 18.43056,
                        Longitud = -68.98449
                    }
                };
                
                
                //clinica coral Lat = 18.43190, Lng = -68.98503;
            }
            else
            {
                this.conyuge = cliente.InfoConyugeObj;
                this.infoLaboral = cliente.InfoLaboralObj;
                this.direccion = Utils.ToDerived<Direccion, DireccionModel>(cliente.InfoDireccionObj);
                var localidad = await localidadService.GetLocalidadesAsync(new LocalidadGetParams { IdLocalidad = this.direccion.IdLocalidad });
                this.direccion.selectedLocalidad = localidad.FirstOrDefault().Nombre;
            }
            SetReferencias(cliente.InfoReferenciasObj);
            StateHasChanged();
        }

        private void SetReferencias(List<Referencia> infoReferenciasObj)
        {
            for (int i = 0; i < 5; i++)
            {
                var referencia = new Referencia { Tipo = (int)EnumTiposReferencia.Personal };

                if ((i+1) <= infoReferenciasObj.Count())
                {
                    referencia = infoReferenciasObj[i];
                }
                referencias.Add(referencia);
            }
        }

        private bool loading { get; set; } = false;
        private bool hideSaveButton = false;
        
        //async Task SaveCliente()
        async Task SaveCliente()
        {
                hideSaveButton = true;
                //todo: validationresult https://www.c-sharpcorner.com/UploadFile/20c06b/using-data-annotations-to-validate-models-in-net/
                this.cliente.InfoConyugeObj = conyuge;
                this.cliente.InfoReferenciasObj = referencias;
                this.cliente.InfoDireccionObj = direccion;
                this.cliente.InfoLaboralObj = infoLaboral;
                await clientesService.SaveCliente(this.cliente);
                await OnGuardarNotification();
                NavManager.NavigateTo("/Clientes");
        }



        //void OnChange(object value, string name)
        //{
        //    var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;
        //    var selectedValue = Convert.ToInt32(str);
        //    //console.Log($"{name} value changed to {str}");
        //}

        //void OnInputFileChange(InputFileChangeEventArgs e)
        //{
        //    var imageFiles = e.GetMultipleFiles();
        //}

        private void SetImages(IList<string> images)
        {
            this.cliente.ImagesForCliente = images;
        }

        //protected void Handle_ConyugeChange(Conyuge conyuge)
        //{
        //    this.cliente.InfoConyugeObj = conyuge;
        //}


    }



}

