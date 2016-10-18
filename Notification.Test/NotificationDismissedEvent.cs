namespace Notification.Test
{
    class NotificationDismissedEvent<T> : NotificationEventBase<T>
        where T: NotificationBase
    {
        public int UserId { get; set; }
        public override void ProcessNotification(T notification)
        {
            notification.NotificationDate = EventDate;
            notification.IsDismissed = true;
        }
    }
}