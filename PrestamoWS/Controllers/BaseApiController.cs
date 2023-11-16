
using Microsoft.AspNetCore.Mvc;
using System;
using System.Configuration;

namespace PrestamoWS.Api
{
    public abstract class BaseApiController : Controller
    {
        protected string UserName => this.User.Identity.Name?? "No User,Debugging";
        
        protected void GetRequestInfo()
        {
            var scheme = this.Request.Scheme;
            var body = this.Request.Body;
            var headers = this.Request.Headers;
            var auth = headers["Authorization"];
            var actions = headers["actionsValues"];
        }

        private  string GetErrorNumber()
        {
            return new Random().Next(2000000).ToString();
        }

        protected string CreateLogErrorMessage(Exception e)
        {
            return $"error id {GetErrorNumber()}, {Environment.NewLine}{e.StackTrace}{Environment.NewLine} exception message {e.Message} {Environment.NewLine} ******************************************** {Environment.NewLine}";
        }

        protected int GetDurationToken()
        {
            var duration = 0;
            int.TryParse(ConfigurationManager.AppSettings["DefaultTokenDuration"], out duration);
            return duration == 0 ? 10 : duration; 
        }

        protected ActionResult<T> ManageRequestResponse<T>(Func<ActionResult<T>> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
