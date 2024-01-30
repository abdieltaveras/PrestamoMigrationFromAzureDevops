using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
//using JWTTokenPOC.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Components;
using PrestamoBlazorApp.Services;
using PrestamoBlazorApp.Shared.Model;
using PrestamoBlazorApp.Shared.Layout.Components.Menu;
using Microsoft.AspNetCore.Components.Authorization;
using PrestamoBlazorApp.Providers;

namespace DispatchAPI.Authentication.Services
{
    public class MenuMiddleware
    {
        private readonly RequestDelegate _next;
        [Inject] private ActionsManagerService actionsManagerService { get; set; }
        [Inject] private UserManagerService userManagerService { get; set; }
        [Inject] private AuthenticationStateProvider _AuthenticationStateProvider { get; set; }

        //private readonly AppSettings _appSettings;
        public MenuMiddleware(RequestDelegate next)
        {
            _next = next;
            //_appSettings = appSettings.Value;
        }
        CustomMenuAction GetMenuSubAction(string value)
        {
            CustomMenuAction resp = new CustomMenuAction();
            //var menu = MenuDictionary.MenuDictionaryData();
            //var authstate = await _AuthenticationStateProvider.GetAuthenticationStateAsync();
            //var user = authstate.User;
            //var name = user.Identity.Name
            ////if (customMap.Actions != null)
            ////{
            //    var act = customMap.Actions.Where(m => m.Value == value).ToList();
            //    if (act.Count() > 0)
            //    {
            //        return act.FirstOrDefault();
            //    }
            //}
            return resp;
        }
        public async Task Invoke(HttpContext context, TokenAuthenticationStateProvider authenticationStateProvider)
        {
            CustomMenuAction resp = new CustomMenuAction();
            //var menu = MenuDictionary.MenuDictionaryData();
            //var state = await authenticationStateProvider.IsTokenExpired();
            //if (!state)
            //{
            //    var authstate = await authenticationStateProvider.GetAuthenticationStateAsync();
            //    var user = authstate.User;
            //    var name = user.Identity.Name;
            //    var a = actionsManagerService.GetFormAction(new DevBox.Core.Access.Action());
            //}



            //    //Validate the token
            //    AttachActionToContext();
            await _next(context);
        }
        public void OnActionClick(DevBox.Core.Access.Action action)
        {
            //Task.Run(async () => await _TokenState.LoggetOutWhenTokenExpiredAndnavigateTo("/home"));
            var url = actionsManagerService.GetFormAction(action);
            //navigationManager.NavigateTo(url);
        }
        private void AttachActionToContext()
        {
            try
            {

            }
            catch (Exception)
            {
  
            }
        }
    }
}
