using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoBlazorApp.Shared
{
    public abstract class BaseForSearch : CommonBase
    {
        protected async void Handle_GetDataForSearch(Func<Task> _action)
        {
            try
            {
                //loading = true;
                //await BlockPage();
                await _action();
                StateHasChanged();
                //await UnBlockPage();
                //loading = false;
            }
            catch (Exception e)
            {
                await NotifyMessageBySnackBar("Ha ocurrido algun error " + e.Message,MudBlazor.Severity.Error);
            }
        }
    }
}
