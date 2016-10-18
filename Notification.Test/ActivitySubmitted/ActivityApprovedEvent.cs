namespace Notification.Test.ActivitySubmitted
{
    class ActivityApprovedEvent : NotificationEventBase<ActivitySubmittedNotification>
    {
        public int ApproverId { get; set; }

        public override void ProcessNotification(ActivitySubmittedNotification notification)
        {
            base.ProcessNotification(notification);
            notification.IsApproved = true;
            notification.ApproverId = ApproverId;

            //if we've read this, do we wanna mark it as not read since status changed?
            notification.IsRead = false;

            ////Don't reset the dismissed flag.  If the user dismissed this notification, why would they want
            //// to see if it was approved?
            //notification.IsDismissed = false
        }
    }
}