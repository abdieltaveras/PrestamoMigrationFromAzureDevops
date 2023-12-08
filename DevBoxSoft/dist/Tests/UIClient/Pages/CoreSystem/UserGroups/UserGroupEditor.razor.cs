using DevBox.Core.Identity;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIClient.Pages.Components;
using UIClient.Services;

namespace UIClient.Pages.CoreSystem.UserGroups
{
    public partial class UserGroupEditor : BasePage
    {
        [Parameter] public CoreUserGroup usersGroup { get; set; }
        [Inject] private UserManagerService userManagerService { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        bool validForm;
        string[] errors = { };
        MudForm form;
        private void CloseDlg()
        {
            MudDialog.Cancel();
        }
        protected override void OnParametersSet()
        {
            base.OnParametersSet();            
        }
        async Task del()
        {
            await Task.Delay(500);
            if (!validForm) { return; }
            await userManagerService.DelGroupAsync(usersGroup);
            await SendAsync("Del Record", usersGroup);
        }
        async Task save()
        {
            await Task.Delay(500);
            if (!validForm) { return; }
            await userManagerService.SaveUserGroupAsync(usersGroup);
            await SendAsync("Ha ocurrido una actualización", usersGroup);
        }
        private async Task SendAsync(string message, object target)
        {
            NotificationsService.Notify(message, "*", "*", "*", mustReload: true);
        }
    }
}
