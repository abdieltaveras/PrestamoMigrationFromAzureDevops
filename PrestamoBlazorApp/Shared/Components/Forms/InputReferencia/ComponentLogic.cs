using PrestamoBlazorApp.Shared.Hooks;
using PrestamoEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared.Components.Forms.InputReferencia
{
    public partial class InputReferenciaV3
    {
    
        protected override async Task OnInitializedAsync()
        {
            if (Referencias == null)
            {
                Referencias = new List<Referencia>();
            }
            if (Referencias.Count() <= 0)
            {
                Referencias.Add(new Referencia());
            }
            await base.OnInitializedAsync();
        }
        private async Task AddNewReferencia()
        {
            new AddRemoveListItemHook<Referencia>(Referencias).AddItem(new Referencia());
            StateHasChanged();
        }
 
        private async Task RemoveReferencia(Referencia refe)
        {
            new AddRemoveListItemHook<Referencia>(Referencias).RemoveItem(refe);
            StateHasChanged();
        }
        //private void SetReferencias(List<Referencia> infoReferenciasObj)
        //{
        //    for (int i = 0; i < 5; i++)
        //    {
        //        var referencia = new Referencia { Tipo = (int)EnumTiposReferencia.Personal };

        //        if ((i + 1) <= infoReferenciasObj.Count())
        //        {
        //            Referencias = infoReferenciasObj;
        //        }
        //        Referencias.Add(referencia);
        //    }
        //}
    }
}
