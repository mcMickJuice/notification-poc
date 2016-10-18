using System.Collections.Generic;
using System.Linq;

namespace Notification.Test.Tests
{
    abstract class BaseNotificationTest
    {
        protected void ProcessEventsOnNotification<T>(
            IEnumerable<NotificationEventBase<T>> events,
            T notification)
            where T : NotificationBase
        {
            foreach (var e in events.OrderBy(e => e.EventDate))
            {
                e.ProcessNotification(notification);
            }
        }
    }
}