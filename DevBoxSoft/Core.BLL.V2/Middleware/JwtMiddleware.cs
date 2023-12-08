using DevBox.Core.BLL.Identity.Interfaces;
using DevBox.Core.Classes.Identity;
using DevBox.Core.Identity;
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

        public async Task Invoke(HttpContext context, IUsersManager usersManager)
        {
            
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var userId = usersManager.ValidateJwtToken(token);

                var userId = TokenManager.GetPrincipal(token)?.Claims?.Where(m => m.Type == ClaimTypes.NameIdentifier)?.First()?.Value;

                if (userId != null)
                {
                    // Asignar usuario al Context en caso de ser validado
                    var user = usersManager.GetUser(new Guid(userId));
                    context.Items["User"] = user;
                    //Asignar accesos
                    context.Items["UserActions"] = user.Actions;
                }

            await _next(context);
        }
    }
}

