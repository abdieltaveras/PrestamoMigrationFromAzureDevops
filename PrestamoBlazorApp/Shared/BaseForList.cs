using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared
{
    public abstract class BaseForList : CommonBase
    {

        // agregar logica para los listados como es OnAgregar, OnDelete, etc

        protected async Task Handle_GetDataForList(Func<Task> _action, string redirectTo = "")
        {
            try
            {
                //loading = true;
                //await BlockPage();
                await _action();
                //StateHasChanged();
                //await UnBlockPage();
                //loading = false;
            }
            catch (Exception e)
            {
                var redirect = redirectTo == "" ? @"\" : redirectTo;
                await SweetMessageBox("Ha ocurrido algun error " + e.Message, icon: "info", redirect, 5000);
            }
        }

        public virtual void OnEditClick(string url)
        {
            NavManager.NavigateTo(url);
        }

    }
}
