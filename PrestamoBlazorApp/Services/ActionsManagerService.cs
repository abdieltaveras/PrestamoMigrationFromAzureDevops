using Blazored.LocalStorage;
using DevBox.Core.Access;
using DevBox.Core.Classes.UI;
using DevBox.Core.Identity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using MudBlazor;
using PrestamoBlazorApp.Providers;
using PrestamoBlazorApp.Shared.Layout.Components.Menu;
using PrestamoBlazorApp.Shared.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using UIClient.Services;
using ActionMenu = DevBox.Core.Access.Action;


namespace PrestamoBlazorApp.Services
{
    public class ActionsManagerService : ServiceBase
    {
        [Inject] AuthenticationStateProvider _authenticationStateProvider { get; set; }
        //[Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private TokenAuthenticationStateProvider _TokenState { get; set; }
        [Inject] private ActionsManagerService actionsManagerService { get; set; }
        [Inject] private UserManagerService _UserManagerService { get; set; }
        [Inject]
        protected NavigationManager NavManager { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] CustomService CustomServ { get; set; }
        public ActionList Actions { get; set; }

        public ActionMenu CurrentAction { get; private set; }
        RootMenuAction customMap = new RootMenuAction();

        public string GetFormAction(ActionMenu action)
        {
            var value = action.Value;
            var id = action.ID;
            var form = getFormByValue(action.Value);
            CurrentAction = action;
            return form;
        }
        public void SetCurrentAction(ActionMenu action)
        {
            CurrentAction = action;
        }
        //CustomMenuAction GetMenuSubAction(string value)
        //{
        //    CustomMenuAction resp = new CustomMenuAction();
        //    if (customMap.Actions != null)
        //    {
        //        var act = customMap.Actions.Where(m => m.Value == value).ToList();
        //        if (act.Count() > 0)
        //        {
        //            return act.FirstOrDefault();
        //        }
        //    }
        //    return resp;
        //}
        //private string getFormByValue(string value) => forms.ContainsKey(value) ? $"/{forms[value]}" : value;
        private string getFormByValue(string value) => MenuDictionary.GetMenuDictionary().ContainsKey(value) ? $"{MenuDictionary.GetMenuDictionary()[value].Url}" : "#";

        
        //Dictionary<string, string> forms = new Dictionary<string, string>
        //{
        //   {"8d8fd452-0ad9-44b2-b8be-f49a7c36ae00","AreasEducativas"},
        //   {"2688fb13-293d-461f-9024-0589eb956b55","Aulas"},
        //   {"453e3779-855f-47cd-bfba-918c159d31ab","CatálogoCuentas"},
        //};

        public ActionsManagerService (IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService, 
            AuthenticationStateProvider authenticationStateProvider, UserManagerService userManagerService, NavigationManager navigationManager) : base(clientFactory, configuration, localStorageService)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _UserManagerService = userManagerService;
            NavManager = navigationManager;
            //var task = Task.Run(async () => await AuthorizeViewActions());
            // var task = AuthorizeViewActions();
            //task.Wait();
        }

        //protected ActionsManagerService(string actionsValues, IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService, NotificationService notificationService) : base(actionsValues, clientFactory, configuration, localStorageService, notificationService)
        //{
        //}

        public ActionPermissionLevel GetPermisionFor(string actionValue,  string subActionValue, string groupName)
        {
            var actionsFound = Actions.Where(act => act.GroupName == groupName && act.Value== actionValue);
            ActionPermissionLevel permissionLevel = ActionPermissionLevel.None;
            if (subActionValue != null)
            {
                var subActionFound = actionsFound.FirstOrDefault().SubActions.Where(suba => suba.Value == subActionValue).FirstOrDefault();
                permissionLevel = subActionFound != null ? subActionFound.PermissionLevel : ActionPermissionLevel.None;
            }
            else
            {
                var actionCheck = Actions.FirstOrDefault();
                permissionLevel = actionCheck  != null ? actionCheck.PermissionLevel : ActionPermissionLevel.None;
            }
            return permissionLevel;
        }
        //Dictionary<string, string> forms = new Dictionary<string, string>
        //{
        //    {"8d8fd452-0ad9-44b2-b8be-f49a7c36ae00","AreasEducativas"},
        //    {"2688fb13-293d-461f-9024-0589eb956b55","Aulas"},
        //    {"453e3779-855f-47cd-bfba-918c159d31ab","CatálogoCuentas"},
        //    {"44f855a3-78f7-4e2e-bca3-eb8e37f2fb60","Cursos"},
        //    {"88aaddf0-9b98-45ca-90cd-acc46152d407","DíasFeriados"},
        //    {"90b8ac00-efdb-421d-9c6f-cfcb994c48ed","ListadoArticulos"},
        //    {"9498ef8f-4436-47f4-842a-c1cb1152dce6","AsignacionesCarnets"},
        //    {"34c9bf6c-f7ff-4d08-8f5c-65735fc46646","Matriculación"},
        //    {"14f7880f-a1d5-418f-81a1-be9cd2b4d467","RegistroNotas"},
        //    {"3269d450-5690-4276-843f-b2ee3a77e013","ListadoEstudiantes"},
        //    {"77c8726e-83ba-463d-9a48-d17825190f00","Cargos"},
        //    {"c2afba87-2882-4a30-a57e-8b82b449f3b7","ImportsGrades"},
        //    {"1d3006c3-cfe7-408e-a81f-ba860adec118","InformeCargos"},
        //    {"10b451be-4bc8-4022-9d40-6df0ccb5d1a5","Grupos"},
        //    {"cf3693a0-0e52-4d0d-8799-847335897c3f","Usuarios"},
        //    {"ce5eda86-977c-4299-bf4b-df02c6c9b719","SysPolicies"},
        //    {"ee7daf58-9dfa-4304-a7f9-4a00403c0cde","ActionManager"}
        //};

        //public ActionsManagerService(IHttpClientFactory clientFactory, IConfiguration configuration, ILocalStorageService localStorageService, IJSRuntime jsRuntime,NotificationService notificationService) : base("", clientFactory, configuration, localStorageService,jsRuntime, notificationService)
        //{
        //}

        protected async Task AuthorizeViewActions()
        {

            var principal = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var usr = new User(principal.User);
            if (principal.User.Identity.IsAuthenticated)
            {

                IEnumerable<CoreUser> users = new CoreUser[0];
                string message = string.Empty;
                try
                {
                    users = await _UserManagerService.GetUser(usr.ID);
                }
                catch (Exception e)
                {
                    message = e.Message;

                }
                var userFound = users.FirstOrDefault();
                var actions = (users.FirstOrDefault()?.Actions ?? ActionList.Empty).Filter(ActionListFilters.Allowed);

                Actions = actions;
                var a = MenuDictionary.MenuDictionaryData();

                foreach (var item in a)
                {
                    if (item.Url.Replace("/", "") == NavManager.Uri.Replace(NavManager.BaseUri, ""))
                    {
                        foreach (var ac in actions)
                        {
                            if (ac.Value == item.Value)
                            {
                                SetCurrentAction(ac);
                                //navigationManager.NavigateTo(item.Url);
                                //StateHasChanged();
                            }
                        }
                    }
                }
            }
        }

        public bool CanView => Task.Run(async () => await HasPermissionFor("View")).Result;
        public bool CanEdit => Task.Run(async () => await HasPermissionFor("Edit")).Result;
        public bool CanDelete => Task.Run(async () => await HasPermissionFor("Delete")).Result;
        public bool CanVoid => Task.Run(async () => await HasPermissionFor("Void")).Result;
        public bool CanAdd => Task.Run(async () => await HasPermissionFor("Add")).Result;
        public async Task<bool> HasPermissionFor(string subActionValue)
        {
            if (CurrentAction==null) return false;

            var resultSubAction = this.CurrentAction.SubActions.FirstOrDefault(item => item.Value == subActionValue);

            if (resultSubAction == null) return false;

            var subActionPermission = resultSubAction.PermissionLevel;
            
            var allowResult = false;
            switch (subActionPermission)
            {
                case DevBox.Core.Access.ActionPermissionLevel.Allow:
                    allowResult = true;
                    break;
                case DevBox.Core.Access.ActionPermissionLevel.None:
                    break;
                case DevBox.Core.Access.ActionPermissionLevel.PermissionRequired:
                    await Task.Run(() => Snackbar.Add("Falta establecer proceso para conceder permiso", MudBlazor.Severity.Warning));
                    break;
                case DevBox.Core.Access.ActionPermissionLevel.Deny:
                    break;
                case DevBox.Core.Access.ActionPermissionLevel.Permitir_Autorizar:
                    await Task.Run(() => Snackbar.Add("Falta establecer proceso para conceder permiso", MudBlazor.Severity.Warning));
                    break;
                default:
                    break;
            }
            return allowResult;
        }

    }
}
