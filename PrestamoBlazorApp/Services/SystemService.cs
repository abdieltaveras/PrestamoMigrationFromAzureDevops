using PrestamoBlazorApp.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;


namespace PrestamoBlazorApp.Services
{
    public class SystemService
    {
        private User currentUser;

        internal User AnonymousUser => new User(new ClaimsPrincipal(new ClaimsIdentity()));
        internal User CurrentUser { get => new User((ClaimsPrincipal)Thread.CurrentPrincipal) ?? AnonymousUser; private set => currentUser = value; }
    }
}
