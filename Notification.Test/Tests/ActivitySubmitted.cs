using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Notification.Test.ActivitySubmitted;
using NUnit.Framework;

namespace Notification.Test.Tests
{
    [TestFixture]
    class ActivitySubmitted: BaseNotificationTest
    {
        private ActivitySubmittedNotification _notificationToUpdate;
        private DateTime _activitySubmittedDate;
        private DateTime _activityApprovedDate;
        private IList<NotificationEventBase<ActivitySubmittedNotification>> _activitySubmittedEvents;
        private int _activityId = 1;
        private int _learnerId = 2;
        private int _approverId = 3;

        [SetUp]
        public void Setup()
        {
            _notificationToUpdate = new ActivitySubmittedNotification();
            _activitySubmittedDate = DateTime.Now;
            _activityApprovedDate = _activitySubmittedDate.AddMinutes(4);

            _activitySubmittedEvents = new List<NotificationEventBase<ActivitySubmittedNotification>>
            {
                new ActivitySubmittedEvent {EventDate = _activitySubmittedDate,ActivityId = _activityId, LearnerId = _learnerId},
                new ActivityApprovedEvent {EventDate = _activityApprovedDate, ApproverId = _approverId}
            };
        }

        [Test]
        public void ActivitySubmittedState()
        {
            ProcessEventsOnNotification(_activitySubmittedEvents.Where(e => e.EventDate <= _activitySubmittedDate), _notificationToUpdate);

            Assert.AreEqual(_notificationToUpdate.ActivityId, _activityId);
            Assert.AreEqual(_notificationToUpdate.LearnerId, _learnerId);
            Assert.IsFalse(_notificationToUpdate.IsApproved);
            Assert.AreEqual(_notificationToUpdate.NotificationDate, _activitySubmittedDate);
        }

        [Test]
        public void ActivityApprovedState()
        {
            ProcessEventsOnNotification(_activitySubmittedEvents.Where(e => e.EventDate <= _activityApprovedDate), _notificationToUpdate);

            Assert.IsTrue(_notificationToUpdate.IsApproved);
            Assert.AreEqual(_notificationToUpdate.ApproverId, _approverId);
            Assert.AreEqual(_notificationToUpdate.NotificationDate, _activityApprovedDate);
        }

        [Test]
        public void ActivityReadResetOnApproval()
        {
            var readEvent = new NotificationReadEvent<ActivitySubmittedNotification>();
            var readEventDate = _activitySubmittedDate.AddMinutes(1);
            readEvent.EventDate = readEventDate;

            _activitySubmittedEvents.Add(readEvent);

            ProcessEventsOnNotification(_activitySubmittedEvents.Where(e => e.EventDate <= readEventDate), _notificationToUpdate);

            Assert.True(_notificationToUpdate.IsRead);

            ProcessEventsOnNotification(_activitySubmittedEvents.Where(e => e.EventDate <= _activityApprovedDate), _notificationToUpdate);

            Assert.False(_notificationToUpdate.IsRead);
        }

        [Test]
        public void ActivityDismissalNotResetOnApproval()
        {
            var dismissedEvent = new NotificationDismissedEvent<ActivitySubmittedNotification>();
            var dismissedDate = _activitySubmittedDate.AddMinutes(1);
            dismissedEvent.EventDate = dismissedDate;
            
            _activitySubmittedEvents.Add(dismissedEvent);

            ProcessEventsOnNotification(_activitySubmittedEvents.Where(e => e.EventDate <= dismissedDate), _notificationToUpdate);

            Assert.True(_notificationToUpdate.IsDismissed);

            ProcessEventsOnNotification(_activitySubmittedEvents.Where(e => e.EventDate <= _activityApprovedDate), _notificationToUpdate);

            Assert.True(_notificationToUpdate.IsDismissed);
        }
    }
}
