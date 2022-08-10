using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared
{
    public abstract class BaseForList : CommonBase
    {
        public bool Dense = true, Hover = true, Bordered = false, Striped = false;
        public bool LoadingTable { get; set; } = false;
        public string SearchStringTable { get; set; }= "";

        // agregar logica para los listados como es OnAgregar, OnDelete, etc

        protected async Task Handle_GetDataForList(Func<Task> _action, string redirectTo = "")
        {
            await Handle_Funct( ()=> SetOverlay(true));

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
                if (e.Message.ToUpper().Contains("JAVASCRIPT INTEROP", StringComparison.InvariantCultureIgnoreCase) == false && e.Message.ToLower().Contains("cannot read properties of null", StringComparison.InvariantCultureIgnoreCase) == false)
                {
                    var redirect = redirectTo == "" ? @"\" : redirectTo;
                    await SweetMessageBox("Ha ocurrido algun error " + e.Message, icon: "info", redirect, 5000);
                }
            }
            await Handle_Funct(() => SetOverlay(false));

        }

        public virtual void OnEditClick(string url)
        {
            NavManager.NavigateTo(url);
        }

    }
}
