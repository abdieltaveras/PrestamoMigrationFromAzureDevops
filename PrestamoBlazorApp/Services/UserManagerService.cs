﻿using Blazored.LocalStorage;
using DevBox.Core.Access;
using DevBox.Core.Classes.Identity;
using DevBox.Core.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.JSInterop;
using UIClient.Services;

namespace PrestamoBlazorApp.Services
{
    public class UserManagerService : ServiceBase
    {
        private string ApiUrl = "api/user";
        public UserManagerService(IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService) : base(clientFactory, configuration, localStorageService)
        {
        }

        //protected UserManagerService(string actionsValues, IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService, NotificationService notificationService) : base(actionsValues, clientFactory, configuration, localStorageService, notificationService)
        //{
        //}

        public async Task<IEnumerable<CoreUser>> GetUser(Guid id)
        {
            var parameters = new { UserID = id.ToString()};
            var result = await GetAsync<CoreUser>("api/user", parameters);
            return result;
        }
        public async Task<IEnumerable<CoreUser>> GetUsers()
        {
            var parameters = new { };
            var result = await GetAsync<CoreUser>("api/users", parameters);
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
            var loginResult = await PostAsync<LoginResult>("api/user/login", credentials);
            if (loginResult == null)
            {
                loginResult = LoginResult.Unauthorized;
            }
            return loginResult;
        }
        public async Task ChangeUserPassword(Guid id, string password)
        {            
            await PostAsync<object>("api/user/changepwd", new { id, password });
        }
        public async Task ResetUserPassword(string NationalID)
        {
            await PostAsync<object>("api/user/resetpassword", new { NationalID });
        }
        public async Task ForceResetUserPassword(Guid id)
        {
            await PostAsync<object>("api/user/forceresetpassword", new { id });
        }
        internal async Task<string> DelUserAsync(CoreUser user)
        {
            var result = await DelAsync("api/users/user", new { GroupID = user.UserID });
            return result;
        }

        internal async Task<CoreUser> SaveUserAsync(CoreUser user)
        {
            var result = await PostAsync<CoreUser>("api/user", user);
            return result;
        }
        internal async Task<string> SaveActions(List<ActionPermission> updates)
        {
            var result = await PostAsync<string>("api/Actions", updates);
            return result;
        }
        internal async Task<string> DelGroupAsync(CoreUserGroup userGroups)
        {
            var result = await DelAsync("api/users/groups", new { GroupID = userGroups.GroupID });
            return result;
        }

        internal async Task<CoreUserGroup> SaveUserGroupAsync(CoreUserGroup userGroups)
        {
            var result = await PostAsync<CoreUserGroup>("api/users/groups", userGroups);
            return result;
        }

        //public UserManagerService(IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService, IJSRuntime jsRuntime, NotificationService notificationService) : 
        //    base("Grupos;Usuarios", clientFactory, configuration, localStorageService, jsRuntime, notificationService)
        //{

        //}
    }
}
