using DevBox.Core.Access;
using DevBox.Core.Classes.Identity;
using DevBox.Core.Classes.Utils;
using DevBox.Core.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace DevBox.Core.BLL.Identity
{
    internal static class TokenManager
    {
        internal static string GenerateToken(CoreUser user)
        {
            //var key = Convert.FromBase64String(TokenAuthConfig.JwtKey);
            var key = Encoding.UTF8.GetBytes(TokenAuthConfig.JwtKey);
            var securityKey = new SymmetricSecurityKey(key);
            var claims = GetClaims(user);
            var subject = new ClaimsIdentity(claims);
            var expDate = DateTime.Now.AddMinutes(10);
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                NotBefore = DateTime.Now,
                Expires = expDate,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
        internal static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null) { return null; }
                //var key = Convert.FromBase64String(TokenAuthConfig.JwtKey);
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
            catch(Exception ex)
            {
                return null;
            }
        }
        private static List<Claim> GetClaims(CoreUser user)
        {
            var claims = new List<Claim>();
            var justAllowed = user.Actions.Filter(ActionListFilters.Allowed);             
            var dicc = new Dictionary<string, object>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.Role, user.GroupName));
            claims.Add(new Claim(ClaimTypes.Expiration, DateTime.Now.AddMinutes(10).ToUniversalTime().ToString()));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.GivenName, user.FullName));
            claims.Add(new Claim(ClaimTypes.Anonymous, "0"));
            claims.Add(new Claim(ClaimTypes.Authentication, "1"));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()));
            claims.Add(new Claim(ClaimTypes.AuthenticationMethod, "Claims"));
            claims.Add(new Claim("Actions", justAllowed.ToJSON()));
            return claims;
        }
        //internal static string GenerateJWT(CoreUser user)
        //{
        //    // Also consider using AsymmetricSecurityKey if you want the client to be able to validate the token
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenAuthConfig.JwtKey));

        //    var expDate = DateTime.Now.AddMinutes(10);
        //    var claims = GetClaims(user);

        //    var claimsArray = claims.ToArray();
        //    var token = new JwtSecurityToken(TokenAuthConfig.Issuer, TokenAuthConfig.Audience, claimsArray,
        //                                      notBefore: DateTime.Now, expires: expDate,
        //                                      signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    return tokenHandler.WriteToken(token);
        //}



        //static public IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        //{
        //    var payload = jwt.Split('.')[1];
        //    var jsonBytes = ParseBase64WithoutPadding(payload);
        //    var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        //    return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        //}
        //static private byte[] ParseBase64WithoutPadding(string base64)
        //{
        //    switch (base64.Length % 4)
        //    {
        //        case 2: base64 += "=="; break;
        //        case 3: base64 += "="; break;
        //    }
        //    return Convert.FromBase64String(base64);
        //}
    }
}