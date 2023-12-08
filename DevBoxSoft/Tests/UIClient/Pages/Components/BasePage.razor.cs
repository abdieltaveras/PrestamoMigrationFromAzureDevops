using DevBox.Core.Access;
using DevBox.Core.Classes.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UIClient.Providers;
using UIClient.Services;

namespace UIClient.Pages.Components
{
    public partial class BasePage : ComponentBase
    {
        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; }
        [Inject] protected SystemService sysService { get; set; }
        [Inject] protected NotificationService NotificationsService { get; set; }
        [Inject] private UserManagerService userManagerService { get; set; }
        [Inject] protected NavigationManager NavManager { get; set; }
        internal User CurrentUser { get; private set; }
        protected Action action { get; set; }
        public string ActionValue { get; set; }

        protected async override Task OnInitializedAsync()
        {
            var authState = await authenticationStateTask;
            CurrentUser = new User(authState.User);

            var uri = NavManager.ToAbsoluteUri(NavManager.Uri);
            ActionValue = uri.LocalPath.Replace("/", "");
            if (!ActionValue.IsNullOrEmpty())
            {
                var user = await userManagerService.GetUser(CurrentUser.ID);
                action = (user.FirstOrDefault()?.Actions ?? ActionList.Empty)
                            .Filter(ActionListFilters.Allowed)[ActionValue];
                if (action == null)
                {
                    NavManager.NavigateTo("Unauthorized");
                }
            }

        }
    }
}
