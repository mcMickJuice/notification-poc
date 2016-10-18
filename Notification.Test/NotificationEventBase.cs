using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Test
{
    abstract class NotificationEventBase<T>
        where T: NotificationBase
    {
        //ties events together
        public int CorrelationId { get; set; }
        public int Id { get; set; }
        public DateTime EventDate { get; set; }

        public virtual void ProcessNotification(T notification)
        {
            notification.NotificationDate = EventDate;
        }
    }
}
