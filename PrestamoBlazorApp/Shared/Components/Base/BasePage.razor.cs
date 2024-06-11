using DevBox.Core.Access;
using DevBox.Core.Classes.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Providers;

namespace PrestamoBlazorApp.Shared.Components.Base
{
    public partial class BasePage : ComponentBase
    {
        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; }
        [Inject] protected SystemService sysService { get; set; }
        [Inject] protected NotificationService NotificationsService { get; set; }
        [Inject] private UserManagerService userManagerService { get; set; }
        [Inject] protected NavigationManager NavManager { get; set; }
        [Inject] private IWebHostEnvironment Env { get; set; }
        internal User CurrentUser { get; private set; }
        protected Action Actions { get; set; }
        public string ActionValues { get; set; }

        protected async override Task OnInitializedAsync()
        {
            var authState = await authenticationStateTask;
            CurrentUser = new User(authState.User);

            if (CurrentUser == null)
            {
                var res = 5;
            }
            var uri = NavManager.ToAbsoluteUri(NavManager.Uri);
            ActionValues = uri.LocalPath.Replace("/", "");
            var urlValidations = IsDevelopmentEnvironment && ConfigurationManager.AppSettings["pcp8uiikisds"] == "false" ;
            if (urlValidations && !ActionValues.IsNullOrEmpty())
            {
                var user = await userManagerService.GetUser(CurrentUser.ID);
                Actions = (user.FirstOrDefault()?.Actions ?? ActionList.Empty)
                            .Filter(ActionListFilters.Allowed)[ActionValues];
                if (Actions == null)
                {
                    NavManager.NavigateTo("Unauthorized");
                }
            }
        }
        protected bool IsDevelopmentEnvironment => Env.IsDevelopment();
    }
}
