using DevBox.Core.Access;
using DevBox.Core.Classes.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIClient.Pages.Components;
using UIClient.Services;
using Microsoft.AspNetCore.Mvc.Razor;

namespace UIClient.Pages
{
    [Authorize]
    public partial class Index : BasePage
    {
        public string welcomeMessage
        {
            get
            {
                var hour = DateTime.Now.Hour;
                if (hour.Between(1, 11))
                {
                    return "Buenos dias";
                }
                else
                {
                    if (hour.Between(12, 18))
                    {
                        return "Buenas tardes";
                    }
                    else
                    {
                        return "Buenas noches";
                    }
                }
            }
        }
        public string UserFullName => CurrentUser?.FullName ?? "";
        protected override void OnInitialized()
        {
            base.OnInitialized();
            var userName = CurrentUser?.UserName ?? "";
            var msg = $"{welcomeMessage} {userName}, todos los sistemas están activos";
            NotificationsService.Notify(msg, userName, "*", "*");
        }
    }
}
