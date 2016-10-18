using System;

namespace Notification.Test
{
    class NotificationReadEvent<T> : NotificationEventBase<T>
        where T: NotificationBase
    {
        public int UserId { get; set; }
        public override void ProcessNotification(T notification)
        {
            notification.IsRead = true;
            notification.NotificationDate = this.EventDate;
        }
    }
}