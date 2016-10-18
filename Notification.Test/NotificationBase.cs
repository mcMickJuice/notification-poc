using System;

namespace Notification.Test
{
    abstract class NotificationBase
    {
        public int CorrelationId { get; set; }
        public bool IsRead { get; set; }
        public bool IsDismissed { get; set; }
        public DateTime NotificationDate { get; set; }
        public abstract NotificationType NotificationType { get; }
    }
}