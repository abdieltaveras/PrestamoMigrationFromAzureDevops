using DevBox.Core.BLL.Identity;
using DevBox.Core.BllService.Models;
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

namespace DevBox.Core.BllService.Controllers.users
{
    public class AuthController : BaseApiController
    {
        IMailService MailService { get; set; }
        private IWebHostEnvironment Environment;
        public AuthController(IWebHostEnvironment _environment, IMailService _mailService)
        {
            Environment = _environment;
            MailService = _mailService;
        }

        [HttpPost, AllowAnonymous, Route("api/user/login")]
        public LoginResult LogIn([FromBody] LoginCredentials credentials)
            => new UsersManager().ValidateCredentials(credentials);

        [HttpPost, AllowAnonymous, Route("api/user/changepwd")]
        public async void ChangeUserPwd([FromBody] ChangeUserPwd changeUserPwd)
        {
            await Task.Run(() => new UsersManager().ResetUsrPass(changeUserPwd.id, changeUserPwd.Password));
        }
        [HttpPost, AllowAnonymous, Route("api/user/resetpassword")]
        public async void Reset([FromBody] LoginCredentialsReset credentialsReset)
        {
            var exceptionMessage = string.Empty;
            var user = new UsersManager().GetUserByNationalID(credentialsReset.NationalID);
            if (user == null)
            {
                throw new InvalidOperationException("Invalid User");
            }
            var tokenUrl = genResetTokenUrl(user);
            var html = genResetPassEMail(tokenUrl);
            try
            {
                await MailService.SendEmailAsync(user.Email, "Cambio Contraseña", html);
            }
            catch (Exception e)
            {

                exceptionMessage = e.Message;
                throw new Exception(e.Message);
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
        private string genResetTokenUrl(CoreUser user)
        {
            var guid = new UsersManager().SetUsrPassReset(user);
            var passwordResetUrl = MailService.PasswordResetUrl;
            var result = $"{passwordResetUrl}/{guid}";
            return result;
        }

        private string genResetPassEMail(string tokenUrl)
        {
            string path = IO.Path.Combine(this.Environment.ContentRootPath, "Resources/HtmlTemplates/") + "ResetUserPassword-DevBox.html";
            var html = IO.File.ReadAllText(path);
            var result = html.Replace("##URL##", tokenUrl);
            return result;
        }

        [Route("api/user"), HttpPost, Authorize(Roles = "Usuarios")]
        public CoreUser PostUser([FromBody] NewUser user)
        { 
            var userManager = new UsersManager();
            var validateConditions = new List<Func<CoreUser, bool>>();
            validateConditions.Add(userManager.ExistEmailForOtherUser);
            validateConditions.Add(userManager.ExistNationalIDForOtherUser);
            var result = userManager.CreateUser(user.UserName, user.FirstName, user.LastName, user.Email,
                                   user.GroupName, user.NationalID, user.IsActive, user.CreatedBy, validateConditions);

            return result;
        }

        [Route("api/user"), HttpDelete, Authorize(Roles = "Usuarios")]
        public void DeleteUser(Guid userID) => new UsersManager().DeleteUser(userID);

        [HttpGet, Route("api/users"), Authorize(Roles = "Usuarios")]
        public List<CoreUser> GetUsers(Guid? UserID, string UserName, string GroupName, string Email, bool? IsActive)
        => new UsersManager().GetUsers(UserID, UserName, GroupName, Email, IsActive);

        [HttpGet, Route("api/user"), Authorize]
        public List<CoreUser> GetUser(Guid UserID)=> new List<CoreUser>() { new UsersManager().GetUser(UserID) };

        [HttpGet, Route("api/actions"), Authorize(Roles = "ActionManager")]
        public ActionList GetActions() => new UsersManager().GetDefaultActions();

        [HttpPost, Route("api/actions"), Authorize(Roles = "ActionManager")]
        public async Task<string> PostActions([FromBody] List<ActionPermission> actions)
            => await new UsersManager().SaveActions(actions);

        [HttpGet, Route("api/users/groups"), Authorize(Roles = "Grupos")]
        public List<CoreUserGroup> GetUserGroups(string GroupName = "") => new UsersGroupsManager().GetUserGroups(GroupName);

        [HttpPost, Route("api/users/groups"), Authorize(Roles = "Grupos")]
        public CoreUserGroup PostUsersGroup([FromBody] NewUserGroup userGroup) => new UsersGroupsManager().CreateUserGroup(userGroup.GroupName, userGroup.Description, User.Identity.Name);

        [HttpPost, AllowAnonymous, Route("api/user/ExistMailForOtherUser")]
        public bool ExistMailForOtherUser(string email, string userName)
        {
            var user = new CoreUser { Email = email, UserName = userName };
            return new BLL.Identity.UsersManager().ExistEmailForOtherUser(user);
        }


    }
}
