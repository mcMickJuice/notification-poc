namespace Notification.Test.ActivitySubmitted
{
    class ActivitySubmittedEvent : NotificationEventBase<ActivitySubmittedNotification>
    {
        public int ActivityId { get; set; }
        public int LearnerId { get; set; }
        public override void ProcessNotification(ActivitySubmittedNotification notification)
        {
            base.ProcessNotification(notification);

            notification.ActivityId = ActivityId;
            notification.LearnerId = LearnerId;
        }
    }
}