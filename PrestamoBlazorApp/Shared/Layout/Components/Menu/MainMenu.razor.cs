using DevBox.Core.Access;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DevBox.Core.Identity;
using PrestamoBlazorApp.Providers;
using PrestamoBlazorApp.Services;

namespace PrestamoBlazorApp.Shared.Layout.Components.Menu
{
    public partial class MainMenu : ComponentBase
    {
        [CascadingParameter]
        private Task<AuthenticationState> authState { get; set; }
        [Inject] private TokenAuthenticationStateProvider _TokenState { get; set; }
        [Inject] private ActionsManagerService actionsManagerService { get; set; }
        [Inject] private UserManagerService userManagerService { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }

        ActionList actions { get; set; }

   
        protected override async Task OnInitializedAsync()
        {
            await loadActions();
            await ListActions();
        }
        private async Task loadActions()
        {
            var principal = (await authState).User;
            if (principal != null)
            {
                var usr = new User(principal);
                var isAuth = principal.Identity.IsAuthenticated;
                IEnumerable<CoreUser> users = new CoreUser[0];
                string message = string.Empty;
                try
                {
                    users = await userManagerService.GetUser(usr.ID);
                }
                catch (Exception e)
                {
                    message = e.Message;

                }
                var userFound = users.FirstOrDefault();
                actions = (users.FirstOrDefault()?.Actions ?? ActionList.Empty).Filter(ActionListFilters.Allowed);

                actionsManagerService.Actions = actions;
                //var a = MenuDictionary.MenuDictionaryData();

                //foreach (var item in a)
                //{
                //    if(item.Url.Replace("/","") == navigationManager.Uri.Replace(navigationManager.BaseUri,""))
                //    {
                //        foreach (var ac in  actions)
                //        {
                //            if(ac.Value == item.Value)
                //            {
                //                actionsManagerService.GetFormAction(ac);
                //                //navigationManager.NavigateTo(item.Url);
                //                StateHasChanged();
                //            }
                //        }
                //    }
                //}
            }
        }


        public void OnActionClick(DevBox.Core.Access.Action action)
        {
            Task.Run(async () => await _TokenState.LoggetOutWhenTokenExpiredAndnavigateTo("/home"));
            var url = actionsManagerService.GetFormAction(action);
            navigationManager.NavigateTo(url);
        }

        public void OnLogOutClick()
        {
            Task.Run(async () => await _TokenState.MarkUserAsLoggedOut());
            navigationManager.NavigateTo("/login",true);
        }

        // para indagar sobre el objeto que define las acciones
        private async Task ListActions()
        {
            var grps = actions.GroupBy(a => a.GroupName);
            foreach (var actionsGrp in grps)
            {
                var actionsInGrp = actionsGrp.ToArray();
                foreach (var action in actionsInGrp)
                {
                    var grpName = action.GroupName;

                    foreach (var subaction in action.SubActions)
                    {
                        var subActValue = subaction.Value;
                        var permisionLevel = subaction.PermissionLevel;
                    }
                }
            }
        }
    }
}
