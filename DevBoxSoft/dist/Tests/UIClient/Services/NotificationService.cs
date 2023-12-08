using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIClient.Models;

namespace UIClient.Services
{
    public class NotificationService
    {
        [Inject] ILocalStorageService _localStorage { get; set; }
        //List<Notification> notifications = new List<Notification>();
        public event Action<Notification> OnNotification;

        /// <summary>
        /// Raises the OnNotification event to all interested parties
        /// </summary>
        /// <param name="message">Object with the information</param>
        /// <param name="users">Comma separated list of UserNames to notify. * Apply to all</param>
        /// <param name="groups">Comma separated list of Groupnames to notify. * Apply to all</param>
        /// <param name="actions">Comma separated list of AtionNames to notify. * Apply to all</param>
        public void Notify(object message, string users, string groups, string actions, bool mustReload = false)
        {
            var notificationType = mustReload ? NotificationType.Info | NotificationType.MustReload //| NotificationType.Force
                                              : NotificationType.Info;
            var news = newNotification(message, users, groups, actions, notificationType);
            OnNotification?.Invoke(news);
        }
        /// <summary>
        /// Raises the OnNotification event to all interested parties
        /// </summary>
        /// <param name="message">Object with the information</param>
        /// <param name="users">Comma separated list of UserNames to notify. * Apply to all</param>
        /// <param name="groups">Comma separated list of Groupnames to notify. * Apply to all</param>
        /// <param name="actions">Comma separated list of AtionNames to notify. * Apply to all</param>
        /// <param name="mustReload">Prompts the user to reload the current page</param>
        /// <param name="mustRestart">Prompts the user to logout of the application and log back in</param>
        /// <param name="force">Does not prompt the user and reloads or restarts the application</param>
        public void NotifyError(object message, Exception exception, string users="*", string groups = "*", string actions = "*", bool mustReload = false, bool mustRestart = false, bool force = false)
        {
            var notificationType = mustReload ? NotificationType.Error | NotificationType.MustReload :
                                   mustRestart ? NotificationType.Error | NotificationType.MustRestart
                                                : NotificationType.Error;
            if (force)
            {
                notificationType |= NotificationType.Force;
            }
            _localStorage.SetItemAsStringAsync("Error", exception.Message);
            var news = newNotification(message, users, groups, actions, notificationType);
            OnNotification?.Invoke(news);
        }

        private static Notification newNotification(object message, string users, string groups, string actions, NotificationType notificationType)
        {
            return new Notification()
            {
                Message = message,
                ToUsers = users.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries),
                ToGroups = groups.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries),
                ToActions = actions.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries),
                IssuedAt = DateTime.Now,
                IssuedBy = "",
                NotificationType = notificationType
            };
        }
    }
}
