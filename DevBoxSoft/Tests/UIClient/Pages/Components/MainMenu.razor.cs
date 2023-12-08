using DevBox.Core.Access;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIClient.Providers;
using UIClient.Services;

namespace UIClient.Pages.Components
{
    public partial class MainMenu
    {
        [CascadingParameter]
        private Task<AuthenticationState> authState { get; set; }

        [Inject] private ActionsManagerService actionsManagerService { get; set; }
        [Inject] private UserManagerService userManagerService { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }

        ActionList actions { get; set; }

        private async Task loadActions()
        {
            var principal = (await authState).User;
            if (principal != null)
            {
                var usr = new User(principal);
                var user = await userManagerService.GetUser(usr.ID);
                actions = (user.FirstOrDefault()?.Actions ?? ActionList.Empty).Filter(ActionListFilters.Allowed);
            }
        }

        public void OnActionClick(DevBox.Core.Access.Action action)
        {
            var form = actionsManagerService.GetFormAction(action);
            navigationManager.NavigateTo(form);
        }
        protected override async Task OnInitializedAsync()
        {
            await loadActions();
        }
    }
}
