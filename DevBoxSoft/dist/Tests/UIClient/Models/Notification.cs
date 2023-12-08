using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UIClient.Models
{
    public class Notification
    {
        public object Message { get; set; }
        public NotificationType  NotificationType { get; set; }
        public DateTime IssuedAt { get; set; }
        public string IssuedBy { get; set; }
        public string[] ToUsers { get; set; } = new string[] { "*" };
        public string[] ToGroups{ get; set; } = new string[] { "*" };
        public string[] ToActions { get; set; } = new string[] { "*" };
    }
}
