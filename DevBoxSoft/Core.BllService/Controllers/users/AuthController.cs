using DevBox.Core.Classes.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using System.Configuration;
using DevBox.Core.BLL.Identity;
using DevBox.Core.BllService.Models;
using DevBox.Core.Identity;

namespace DevBox.Core.BllService.Controllers.users
{    
    public class AuthController : ApiController
    {
        [HttpPost, AllowAnonymous, Route("api/user/login")]
        public LoginResult LogIn([FromBody] LoginCredentials credentials)
        {
            return UsersManager.ValidateCredentials(credentials);
        }
        [HttpPost, AuthFilter, Route("api/user")]
        public CoreUser Create([FromBody] NewUser user)
        {
            var result = UsersManager.CreateUser(user.UserName, user.FirstName, user.LastName, user.Email, user.GroupName, user.CreatedBy);
            return result;
        }
        [HttpGet, AuthFilter, Route("api/user")]
        public List<CoreUser> GetUsers(string UserName, string GroupName, string Email, bool? IsActive)
        {
            var result = UsersManager.GetUsers(UserName, GroupName, Email, IsActive);
            return result;
        }
        //[HttpGet, Route("api/actions")]
        //public object GetActions()
        //{
        //    var result = UsersManager.getDefaultActions().Select(a => new { a.ID, a.DisplayName, a.Value });
        //    return result;
        //}
    }
}
