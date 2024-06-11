
using DevBox.Core.Access;
using DevBox.Core.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace apiBonosElectronicos.Authorization.Attributtes
{

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class AuthorizeAttribute : Attribute, Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter
    {
		private readonly string _GroupName;
        private readonly string _Action;
        private readonly string _SubAction;
        private readonly string _Roles;
        
        private readonly string _rolesSplit;


        public void OnAuthorization(AuthorizationFilterContext context)
		{
			// skip authorization if action is decorated with [AllowAnonymous] attribute
			var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
			if (allowAnonymous)
				return;

			// authorization
			var user = (CoreUser?)context.HttpContext.Items["User"];
			var actions = (ActionList)context.HttpContext.Items["UserActions"];

            if (user == null || actions.Count() <= 0)
			{
				// not logged in or role not authorized
				context.Result = new ObjectResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                //context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized }; .NET7-8, quizas 6
            }
			if (!string.IsNullOrEmpty(_GroupName)) // si utilizan un rol, acceso o lo que sea, se validará en los accesos asignados al usuario.
			{
				var group = actions.Where(m => m.GroupName == _GroupName);
				if(group.Count() < 0)
				{
                    context.Result = new ObjectResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
            if (!string.IsNullOrEmpty(_Action))
            {
                bool accessToAction = false;
                var group = actions.Where(m => m.Value == _Action);
                if (group.Count() < 0)
                {
                    context.Result = new ObjectResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
                //foreach (var item in group)
                //{
                //    foreach (var act in item.SubActions)
                //    {
                //        if (act.Value == _Action) accessToAction = true; break;
                //    }
                //}
                if (accessToAction == false) context.Result = new ObjectResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            if (!string.IsNullOrEmpty(_SubAction))
            {
                bool accessToAction = false;
                var acti = actions.Where(m => m.Value == _Action);
                foreach (var item in acti)
                {
                    foreach (var act in item.SubActions)
                    {
                        if (act.Value == _SubAction) accessToAction = true; break;
                    }
                }
                if (accessToAction == false) context.Result = new ObjectResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
		public AuthorizeAttribute(string Roles = "",string GroupName = "", string Action = "")
		{
			_GroupName = GroupName;
			_Action = Action;
            _Roles = Roles;
		}
        public AuthorizeAttribute(string Action, string SubAction)
        {
            _Action = Action;
            _SubAction = SubAction;
        }

    }
}