using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Test.ActivitySubmitted
{
    class ActivitySubmittedNotification: NotificationBase
    {
        public int LearnerId { get; set; }
        public int ActivityId { get; set; }
        public bool IsApproved { get; set; }
        public int ApproverId { get; set; }

        public ActivitySubmittedNotification()
        {
            
        }

        public ActivitySubmittedNotification(int activityId)
        {
            CorrelationId = activityId;
        }
        public override NotificationType NotificationType => NotificationType.ActivitySubmitted;
    }
}
