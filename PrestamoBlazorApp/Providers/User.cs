using System;
using System.Linq;
using System.Security.Claims;

namespace PrestamoBlazorApp.Providers
{
    internal class User
    {
        private ClaimsPrincipal principal;
        public ClaimsPrincipal Principal { get => principal; private set => principal = value; }
        public string UserName => principal?.Identity?.Name ?? "";
        public string FullName => principal?.FindFirst(PrestamoBlazorApp.Models.ClaimTypes.GivenName)?.Value ?? "";
        public string Email => principal?.FindFirst(PrestamoBlazorApp.Models.ClaimTypes.Email)?.Value ?? "";
        public Guid ID => Guid.Parse(principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString());
        public User(ClaimsPrincipal principal)
        {
            this.principal = principal;
        }
    }
}
