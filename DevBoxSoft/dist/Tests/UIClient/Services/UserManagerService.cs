using Blazored.LocalStorage;
using DevBox.Core.Access;
using DevBox.Core.Classes.Identity;
using DevBox.Core.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UIClient.Pages.CoreSystem;

namespace UIClient.Services
{
    public class UserManagerService : HttpServiceBase
    {
        public async Task<IEnumerable<CoreUser>> GetUsers(Guid? id = null)
        {
            var parameters = new { UserID = id?.ToString() ?? "" };
            var result = await GetAsync<CoreUser>("api/user", parameters);
            return result;
        }
        public async Task<IEnumerable<CoreUserGroup>> GetUsersGroups()
        {
            var result = await GetAsync<CoreUserGroup>("api/users/groups", new { });
            return result;
        }
        public async Task<LoginResult> AuthenticateUserAsync(string userName, string password)
        {
            var credentials = new LoginCredentials() { UserName = userName, Password = password };
            var loginResult = await PostAsync<LoginCredentials, LoginResult>("api/user/login", credentials, requiresAuth: false);
            if (loginResult == null)
            {
                loginResult = LoginResult.Unauthorized;
            }
            return loginResult;
        }

        internal async Task<string> DelUserAsync(CoreUser user)
        {
            var result = await DelAsync("api/users/user", new { GroupID = user.UserID });
            return result;
        }

        internal async Task<CoreUser> SaveUserAsync(CoreUser user)
        {
            var result = await PostAsync<CoreUser, CoreUser>("api/user", user);
            return result;
        }
        internal async Task<string> SaveActions(List<ActionPermission> updates)
        {
            var result = await PostAsync<List<ActionPermission>, string>("api/actions", updates);
            return result;
        }
        internal async Task<string> DelGroupAsync(CoreUserGroup userGroups)
        {
            var result = await DelAsync("api/users/groups", new { GroupID = userGroups.GroupID });
            return result;
        }

        internal async Task<CoreUserGroup> SaveUserGroupAsync(CoreUserGroup userGroups)
        {
            var result = await PostAsync<CoreUserGroup, CoreUserGroup>("api/users/groups", userGroups);
            return result;
        }

        public UserManagerService(IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService, NotificationService notificationService) : base("Grupos;Usuarios", clientFactory, configuration, localStorageService, notificationService)
        {

        }
    }
}
