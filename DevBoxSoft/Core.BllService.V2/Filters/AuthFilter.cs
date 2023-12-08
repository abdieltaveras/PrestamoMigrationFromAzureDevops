using DevBox.Core.BLL.Identity;
//using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace DevBox.Core.BllService
{
    public class AuthFilter : AuthorizeAttribute
    {
        new public bool AllowMultiple => false;
        public void Authenticate(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var request = context.Request;
            var authorization = context.Request.Headers.Authorization;
            if (authorization == null)
            {
                context.ErrorResult = new AuthenticationFailiureResult("Missing Auth Info", request);
                return;
            }
            if (!authorization.Scheme.Equals("Bearer", StringComparison.InvariantCultureIgnoreCase))
            {
                context.ErrorResult = new AuthenticationFailiureResult("Invalid Auth Schema", request);
                return;
            }
            if (string.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailiureResult("Missing Token", request);
                return;
            }
            var principal = new UsersManager().ParseToken(authorization.Parameter);
            if (principal == null)
            {
                context.ErrorResult = new AuthenticationFailiureResult("Invalid Token", request);
                return;
            }
            var exp = principal.Claims.ToList().Find(c => c.Type == "exp").Value;
            var nbf = principal.Claims.ToList().Find(c => c.Type == "nbf").Value;

            var expireTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(exp));
            var notBeforeTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(nbf));
            var expDate = expireTime.LocalDateTime;
            var notBefore = notBeforeTime.LocalDateTime;
            if ((notBefore > DateTime.Now) && (DateTime.Now < expDate))
            {
                context.ErrorResult = new AuthenticationFailiureResult("Token Invalid", request);
                return;
            }
            context.Principal = principal;
        }

        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var result = await context.Result.ExecuteAsync(cancellationToken);
            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                result.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Basic", "realm=localhost"));
            }
            context.Result = new ResponseMessageResult(result);
        }
    }
    public class AuthenticationFailiureResult : IHttpActionResult
    {
        public string ReasonPhrase { get; set; }
        public HttpRequestMessage RequestMessage { get; set; }
        public AuthenticationFailiureResult(string reasonPhrase, HttpRequestMessage requestMessage)
        {
            ReasonPhrase = reasonPhrase;
            RequestMessage = requestMessage;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            var responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            responseMessage.RequestMessage = RequestMessage;
            responseMessage.ReasonPhrase = ReasonPhrase;
            return responseMessage;
        }
    }
}