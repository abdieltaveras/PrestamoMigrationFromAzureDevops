using Microsoft.AspNetCore.Components;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Forms.InputReferencia
{
    public partial class InputReferenciaV3
    {

        [Parameter]
        public int Limit { get; set; } = 1;

        [Parameter]
        public List<Referencia> Referencias
        {
            get => _value;
            set
            {
                if (_value == value) return;

                _value = value;
                ReferenciasChanged.InvokeAsync(value);
            }
        }

        [Parameter]
        public EventCallback<List<Referencia>> ReferenciasChanged { get; set; }

        private List<Referencia> _value;
    }
}
