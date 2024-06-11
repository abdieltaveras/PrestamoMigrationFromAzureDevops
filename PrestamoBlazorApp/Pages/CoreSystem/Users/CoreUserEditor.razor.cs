using DevBox.Core.Identity;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared.Components.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PrestamoBlazorApp.Pages.CoreSystem.Users
{
    public partial class CoreUserEditor : BasePage
    {
        [Parameter] public CoreUser user{ get; set; }
        [Inject] private UserManagerService userManagerService { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        List<CoreUserGroup> usersGroups { get; set; } = new List<CoreUserGroup>();
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
            Task.Run(async () => usersGroups = (await userManagerService.GetUsersGroups()).ToList());            
        }
        async Task del()
        {
            await Task.Delay(500);
            if (!validForm) { return; }
            await userManagerService.DelUserAsync(user);
            await SendAsync("Del Record", user);
        }
        async Task save()
        {
            await Task.Delay(500);
            if (!validForm) { return; }
            user.CreatedBy = CurrentUser.UserName;
            await userManagerService.SaveUserAsync(user);
            await SendAsync("Ha ocurrido una actualización", this);
        }
        private async Task SendAsync(string message, object target)
        {
            NotificationsService.Notify(message, "*", "*", "*", mustReload: true);
        }
    }
}
