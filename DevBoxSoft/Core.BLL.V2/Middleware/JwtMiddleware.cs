using DevBox.Core.BLL.Identity.Interfaces;
using DevBox.Core.Classes.Identity;
using DevBox.Core.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DevBox.Core.BLL.Middleware
{

    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUsersManager usersManager, IHttpContextAccessor httpContextAccessor)
        {
            
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var userId = usersManager.ValidateJwtToken(token);

            var userId = TokenManager.GetPrincipal(token)?.Claims?.Where(m => m.Type == ClaimTypes.NameIdentifier)?.First()?.Value;
            if (userId != null)
            {
            var principal = TokenManager.GetPrincipal(token);
                var claimsIdentity = new ClaimsIdentity(principal.Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    // Asignar usuario al Context en caso de ser validado
                    var user = usersManager.GetUser(new Guid(userId));
                    context.Items["User"] = user;
                    //Asignar accesos
                    context.Items["UserActions"] = user.Actions;
                await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties
                {
                    IsPersistent = true
                });
            }

            await _next(context);
        }
    }
}

