using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UIClient.Models;
using UIClient.Providers;
using UIClient.Services;

namespace UIClient.Shared
{
    public partial class MainLayout : LayoutComponentBase
    {
        [Inject] NavigationManager uriHelper { get; set; }
        [Inject] ApiAuthenticationStateProvider auth { get; set; }
        [Inject] protected NotificationService notificationService { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            notificationService.OnNotification += NotificationService_OnNotification;
        }
        Dictionary<NotificationType, Severity> notificationSeverityTable = new Dictionary<NotificationType, Severity>
        {
             {NotificationType.Info, Severity.Info },
             {NotificationType.Error, Severity.Warning}
        };
        private void NotificationService_OnNotification(Notification news)
        {
            var notify = news.ToUsers.Contains("*")
                        || news.ToGroups.Contains("*")
                        || news.ToActions.Contains("*");
            if (notify)
            {
                var notType = (news.NotificationType & NotificationType.Info) == NotificationType.Info
                               ? NotificationType.Info : NotificationType.Error;
                var severity = notificationSeverityTable[notType];
                var msg = $"{news.Message}";
                Snackbar.Add(msg, severity, config => getMsgConfig(config, news));
            }
        }

        private void getMsgConfig(SnackbarOptions config, Notification news)
        {
            config.ActionColor = Color.Primary;
            config.ActionVariant = Variant.Filled;
            config.ShowCloseIcon = true;
            config.CloseAfterNavigation = true;
            var mustReload = (news.NotificationType & NotificationType.MustReload) == NotificationType.MustReload;
            var mustRestart = (news.NotificationType & NotificationType.MustRestart) == NotificationType.MustRestart;
            var useForce = (news.NotificationType & NotificationType.Force) == NotificationType.Force;
            if (mustReload || mustRestart)
            {
                var timer = new Timer(new TimerCallback(_ => reload(mustRestart, useForce)), null, 1500, 500);
            }

        }

        private void reload(bool restart = false, bool logOut = false)
        {
            if (logOut)
            {
                auth.MarkUserAsLoggedOut();
                uriHelper.NavigateTo("/");
                return;
            }
            uriHelper.NavigateTo(uriHelper.Uri, forceLoad: true);
        }
        private void GoHome()
        {
            NavigationManager.NavigateTo("/");
        }
    }
}
