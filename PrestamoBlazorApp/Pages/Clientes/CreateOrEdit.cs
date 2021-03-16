using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using PrestamoBlazorApp.Data;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared;
using PrestamoBLL.Entidades;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PrestamoBlazorApp.Pages.Clientes
{
    public partial class CreateOrEdit
    {
        [Inject]
        OcupacionesService ocupacionesService { get; set; }

        string searchSector = string.Empty;
        string selectedLocalidad = "Ninguna";
        [Parameter]
        public int idCliente { get; set; }
        public Cliente cliente { get; set; }
        EventConsole console;
        string TextoForActivo { get; set; } = "Si";
        List<EnumModel> TiposIdentificacionPersonaList { get; set; }

        [Inject]
        ClientesService clientesService { get; set; }

        private IEnumerable<EnumModel> EstadosCiviles = EnumToList.GetEnumEstadosCiviles();
        private IEnumerable<EnumModel> TiposIdentificacion = EnumToList.GetEnumTiposIdentificacionPersona();

        private IEnumerable<Ocupacion> Ocupaciones { get; set; } = new List<Ocupacion>();

        private async Task<IEnumerable<Ocupacion>> GetOcupaciones()
        {
            var result = await ocupacionesService.GetOcupacionesAsync();
            return result;
        }

        bool disableCodigo { get; set; } = true;
        protected override async Task OnInitializedAsync()
        {
            
            await base.OnInitializedAsync();
            this.cliente = new Cliente();
            this.cliente.Codigo = "Nuevo";
            Ocupaciones = await GetOcupaciones();
            //TiposIdentificacionPersonaList = EnumToAList.GetEnumTiposIdentificacionPersona();
        }
        

        private bool loading { get; set; }
        //async Task SaveCliente()
        async Task SaveCliente()
        {
            loading = true;
            await clientesService.SaveCliente(this.cliente);
            loading = false;
        }


        void OnChange(object value, string name)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;
            var selectedValue = Convert.ToInt32(str);
            //console.Log($"{name} value changed to {str}");
        }

        void OnInputFileChange(InputFileChangeEventArgs e)
        {
            var imageFiles = e.GetMultipleFiles();

        }
        
        private void SetImages(IList<string> images)
        {
            this.cliente.ImagesForCliente = images;
        }
        
        int zoom = 10;
        bool showMadridMarker;
        

        void OnMapClick(GoogleMapClickEventArgs args)
        {
            console.Log($"Map clicked at Lat: {args.Position.Lat}, Lng: {args.Position.Lng}");
        }

        void OnMarkerClick(RadzenGoogleMapMarker marker)
        {
            console.Log($"Map {marker.Title} marker clicked. Marker position -> Lat: {marker.Position.Lat}, Lng: {marker.Position.Lng}");
        }

        public void Test(bool test)
        {
            Console.WriteLine(test);
        }


        ConfirmBase DeleteConfirmation { get; set; } 

        protected void Delete_Click()
        {
            DeleteConfirmation.Show();
        }

        protected void ConfirmDelete_Click(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                console.Log("borrando data");
            }
        }

    }

}

