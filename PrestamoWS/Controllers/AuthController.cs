using DevBox.Core.BLL.Identity;

using DevBox.Core.Classes.Identity;
using DevBox.Core.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using DevBox.Core.Access;
using System;
using System.Threading.Tasks;
using IO = System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using PrestamoWS.Api;
using PrestamoWS.Services;
using PrestamoWS.Models;

namespace PrestamoWS.Controllers
{

    public class AuthController : BaseApiController
    {
        IMailService MailService { get; set; }
        private IWebHostEnvironment Environment;
        private readonly ILogger<AuthController> Logger;
        public AuthController(IWebHostEnvironment _environment, IMailService _mailService, ILogger<AuthController> logger)
        {
            Environment = _environment;
            MailService = _mailService;
            Logger = logger;
        }

        [HttpPost, AllowAnonymous, Route("api/user/login")]

        public LoginResult LogIn([FromBody] LoginCredentials credentials)
        {
            var result = new UsersManager().ValidateCredentials(credentials,this.GetDurationToken());
            return result;
        }

        [HttpPost, AllowAnonymous, Route("api/user/changepwd")]
        public async void ChangeUserPwd([FromBody] ChangeUserPwd changeUserPwd)
        {
            await Task.Run(() => new UsersManager().ResetUsrPass(changeUserPwd.id, changeUserPwd.Password));
        }
        [HttpPost, AllowAnonymous, Route("api/user/resetpassword")]
        [RequireHttps]
        public async void Reset([FromBody] LoginCredentialsReset credentialsReset)
        {
            var user = new UsersManager().GetUserByNationalID(credentialsReset.NationalID);
            if (user == null || user.UserName != credentialsReset.NationalID)
            {
                throw new InvalidOperationException("Invalid User");
            }

            
            var tokenUrl = genResetTokenUrl(user, MailService);
            var html = genResetPassEMail(tokenUrl, this.Environment);
            try
            {
                await MailService.SendEmailAsync(user.Email, "Cambio Contraseña", html);
                Logger.LogInformation("El correo fue enviado correctament"); 
            }
            catch (Exception e)
            {
                Logger.LogError(CreateLogErrorMessage(e));
                throw;
            }

        }
        [HttpPost, Route("api/user/forceresetpassword"), Authorize(Roles = "Usuarios")]
        public async void ForceReset([FromBody] LoginCredentialsResetForced credentialsReset)
        {
            var user = new UsersManager().GetUser(credentialsReset.id);
            if (user == null)
            {
                throw new InvalidOperationException("Invalid User");
            }

            await Task.Run(() => new UsersManager().SetUsrPassReset(user, true));
        }
        public static string genResetTokenUrl(CoreUser user, IMailService mailService)
        {
            var guid = new UsersManager().SetUsrPassReset(user);
            var passwordResetUrl = mailService.PasswordResetUrl;
            var result = $"{passwordResetUrl}/{guid}";
            return result;
        }

        
        public static string genResetPassEMail(string tokenUrl, IWebHostEnvironment environment)
        {
            string path = IO.Path.Combine(environment.ContentRootPath, "Resources/HtmlTemplates/") + "ResetUserPassword-DevBox.html";
            var html = IO.File.ReadAllText(path);
            var result = html.Replace("##URL##", tokenUrl);
            return result;
        }

        [Route("api/user"), HttpPost, Authorize(Roles = "Usuarios")]
        public CoreUser PostUser([FromBody] NewUser user)
        { 
            var result = new UsersManager().CreateUser(user.UserName, user.FirstName, user.LastName, user.Email,
                                   user.GroupName, user.NationalID, user.IsActive, user.CreatedBy,null);

            return result;
        }

        [Route("api/user"), HttpDelete, Authorize(Roles = "Usuarios")]
        public void DeleteUser(Guid userID) => new UsersManager().DeleteUser(userID);

        [HttpGet, Route("api/users"), Authorize(Roles = "Usuarios")]
        public List<CoreUser> GetUsers(Guid? UserID, string UserName, string GroupName, string Email, bool? IsActive)
        => new UsersManager().GetUsers(UserID, UserName, GroupName, Email, IsActive);

        [HttpGet, Route("api/user"), Authorize]
        public List<CoreUser> GetUser(Guid UserID)
        {
            var result = new List<CoreUser>() { new UsersManager().GetUser(UserID) };
            return result;
        }

        [HttpGet, Route("api/actions"), Authorize(Roles = "ActionManager")]
        public ActionList GetActions() => new UsersManager().GetDefaultActions();

        [HttpPost, Route("api/actions"), Authorize(Roles = "ActionManager")]
        public async Task<string> PostActions([FromBody] List<ActionPermission> actions)
            => await new UsersManager().SaveActions(actions);

        [HttpGet, Route("api/users/groups"), Authorize(Roles = "Grupos")]
        public List<CoreUserGroup> GetUserGroups(string GroupName = "") => new UsersGroupsManager().GetUserGroups(GroupName);

        [HttpPost, Route("api/users/groups"), Authorize(Roles = "Grupos")]
        public CoreUserGroup PostUsersGroup([FromBody] NewUserGroup userGroup) => new UsersGroupsManager().CreateUserGroup(userGroup.GroupName, userGroup.Description, User.Identity.Name);
    }
}
