using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Services;
using PrestamoBLL.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PrestamoBlazorApp.Pages
{
    public partial class ClientesAddOrEdit
    {
        [Parameter]
        public int idCliente { get; set; }
        public Cliente cliente { get; set; }

        string TextoForActivo { get; set; } = "Si";
        List<EnumModel> TiposIdentificacionPersonaList { get; set; }

        ClientesService clientesService { get; set; }

        bool disableCodigo { get; set; } = true;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.cliente = new Cliente();
            this.cliente.Codigo = "Nuevo";
            //TiposIdentificacionPersonaList = EnumToAList.GetEnumTiposIdentificacionPersona();
        }

        private bool loading { get; set; }
        //async Task SaveCliente()
        void SaveCliente()
        {
            loading = true;
            //wait clientesService.SaveCliente(this.cliente);
            loading = false;
        }


        void OnChange(object value, string name)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;
            var selectedValue = Convert.ToInt32(str);
            Console.WriteLine($"{name} value changed to {str}");
        }


        
    }
    public class EnumModel
    {
        public int IdValue { get; set; }

        public string Text { get; set; }
    }

    static class EnumToAList
    {

        public static List<EnumModel> GetEnumTiposIdentificacionPersona()
        {
            var result = ((TiposIdentificacionPersona[])Enum.GetValues(typeof(TiposIdentificacionPersona))).Select(c => new EnumModel() { IdValue = (int)c, Text = c.ToString() }).ToList();
            return result;
        }
        public static List<EnumModel> GetEnumEstadosCiviles()
        {
            var result = ((EstadosCiviles[])Enum.GetValues(typeof(EstadosCiviles))).Select(c => new EnumModel() { IdValue = (int)c, Text = c.ToString() }).ToList();
            return result;
        }
    }

    
}
