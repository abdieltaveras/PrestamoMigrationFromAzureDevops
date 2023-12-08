using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using UIClient.Providers;

namespace UIClient.Services
{
    public class SystemService
    {
        private User currentUser;

        internal User AnonymousUser => new User(new ClaimsPrincipal(new ClaimsIdentity()));
        internal User CurrentUser { get => new User((ClaimsPrincipal)Thread.CurrentPrincipal) ?? AnonymousUser; private set => currentUser = value; }
    }
}
