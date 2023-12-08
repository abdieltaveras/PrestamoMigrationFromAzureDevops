using DevBox.Core.Access;
using DevBox.Core.Classes.Utils;
using DevBox.Core.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DevBox.Core.Classes.Identity
{
    public static class TokenManager
    {
        public static string GenerateToken(CoreUser user, int durationMinutes = 10)
        {
            var key = Encoding.UTF8.GetBytes(TokenAuthConfig.JwtKey);
            var securityKey = new SymmetricSecurityKey(key);
            var claims = GetClaims(user,durationMinutes:durationMinutes);
            var subject = new ClaimsIdentity(claims);
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Audience = TokenAuthConfig.Audience,
                Issuer = TokenAuthConfig.Issuer,
                NotBefore = GetNotBefore(),
                Expires = GetExpirationDate(durationMinutes),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(descriptor);
            return tokenHandler.WriteToken(token);
        }

        private static DateTime? GetExpirationDate(int durationMinutes) => DateTime.UtcNow.AddMinutes(durationMinutes);
        

        private static DateTime? GetNotBefore()=> DateTime.UtcNow;

        public static string GenerateRefreshToken()
        {
            string refreshToken = "";
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            refreshToken = Convert.ToBase64String(randomNumber);
            return refreshToken;
        }
        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null) { return null; }
                var key = Encoding.UTF8.GetBytes(TokenAuthConfig.JwtKey);
                var parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, parameters, out securityToken);
                return principal;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static List<Claim> GetClaims(CoreUser user,  bool actionsAsRoles = false, bool justActions = false, int durationMinutes = 10)
        {
            var claims = new List<Claim>();
            if (!justActions)
            {
                claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                //claims.Add(new Claim(ClaimTypes.Role, user.GroupName));
                claims.Add(new Claim(ClaimTypes.Expiration, GetExpirationDate(durationMinutes).ToString()));
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
                claims.Add(new Claim("CompanyId", user.CompanyId.ToString()));
                claims.Add(new Claim("CompaniesAccess", user.CompaniesAccess.ToString()));
                claims.Add(new Claim(ClaimTypes.GivenName, user.FullName));
                claims.Add(new Claim(ClaimTypes.Anonymous, "0"));
                claims.Add(new Claim(ClaimTypes.Authentication, "1"));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()));
                claims.Add(new Claim(ClaimTypes.AuthenticationMethod, "Claims"));
            }
            else
            {
                actionsAsRoles = true;
            }
            if (actionsAsRoles)
            {
                setActionRoles(user, claims);
            }
            return claims;
        }
        /// <summary>
        /// Aqui se determinan los roles
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claims"></param>
        private static void setActionRoles(CoreUser user, List<Claim> claims)
        {
            var isAdminUser = user.UserName.Equals("Ernesto", StringComparison.CurrentCultureIgnoreCase);
            var listFilters = isAdminUser ? ActionListFilters.All : ActionListFilters.Allowed;
            var justAllowed = user.Actions.Filter(listFilters);
            Func<ActionPermissionLevel, bool> isAllowed = pl => isAdminUser || (- 1 < pl.inSet(ActionPermissionLevel.Allow, ActionPermissionLevel.PermissionRequired, ActionPermissionLevel.Permitir_Autorizar));
            justAllowed.ForEach(a =>
            {
                var pl = isAdminUser ? ActionPermissionLevel.Allow : a.PermissionLevel;
                a.PermissionLevel = pl;
                if (isAllowed(pl))
                {
                    claims.Add(new Claim(ClaimTypes.Role, $"{a.Value}"));
                    a.SubActions.ForEach(sa =>
                    {
                        sa.PermissionLevel = isAdminUser ? ActionPermissionLevel.Allow : sa.PermissionLevel;
                        if (isAllowed(pl))
                        {
                            claims.Add(new Claim(ClaimTypes.Role, $"{a.Value}:{sa.Value}"));
                        }
                    });
                }
            });
        }

        
    }
}