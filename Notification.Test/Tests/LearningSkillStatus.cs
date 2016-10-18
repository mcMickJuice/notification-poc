using System;
using System.Collections.Generic;
using System.Linq;
using Notification.Test.LearnerSkillProgress;
using NUnit.Framework;

namespace Notification.Test.Tests
{
    [TestFixture]
    class LearningSkillStatus: BaseNotificationTest
    {
        private IList<NotificationEventBase<LearnerSkillStatusNotification>> _notificationEvents;
        private DateTime _strugglingDate;
        private DateTime _failedOnceDate;
        private DateTime _failedTwiceDate;
        private LearnerSkillStatusNotification _notificationToBuild;

        [SetUp]
        public void Setup()
        {
            _strugglingDate = DateTime.Now;
            _failedOnceDate = _strugglingDate.AddMinutes(2);
            _failedTwiceDate = _strugglingDate.AddMinutes(4);
            _notificationToBuild = new LearnerSkillStatusNotification();
            _notificationToBuild.ProgressStatus = LearnerSkillProgressStatus.Unknown;

            _notificationEvents = new List<NotificationEventBase<LearnerSkillStatusNotification>>()
            {
                new LearnerSkillProgressEvent(LearnerSkillProgressStatus.Struggling, _strugglingDate),
                new LearnerSkillProgressEvent(LearnerSkillProgressStatus.FailedOnce, _failedOnceDate),
                new LearnerSkillProgressEvent(LearnerSkillProgressStatus.FailedTwice, _failedTwiceDate),

            };
        }

        [Test]
        public void HasUnknownStatus()
        {
            ProcessEventsOnNotification(_notificationEvents.Where(e => e.EventDate < _strugglingDate), _notificationToBuild);

            Assert.AreEqual(_notificationToBuild.ProgressStatus, LearnerSkillProgressStatus.Unknown);
        }

        [Test]
        public void HasStrugglingStatus()
        {
            ProcessEventsOnNotification(_notificationEvents.Where(e => e.EventDate <= _strugglingDate), _notificationToBuild);

            Assert.AreEqual(_notificationToBuild.ProgressStatus, LearnerSkillProgressStatus.Struggling);
            Assert.AreEqual(_notificationToBuild.NotificationDate, _strugglingDate);
        }

        [Test]
        public void HasFailedOnceStatus()
        {
            ProcessEventsOnNotification(_notificationEvents.Where(e => e.EventDate <= _failedOnceDate), _notificationToBuild);

            Assert.AreEqual(_notificationToBuild.ProgressStatus, LearnerSkillProgressStatus.FailedOnce);
            Assert.AreEqual(_notificationToBuild.NotificationDate, _failedOnceDate);
        }

        [Test]
        public void HasFailedTwiceStatus()
        {
            ProcessEventsOnNotification(_notificationEvents.Where(e => e.EventDate <= _failedTwiceDate), _notificationToBuild);

            Assert.AreEqual(_notificationToBuild.ProgressStatus, LearnerSkillProgressStatus.FailedTwice);
            Assert.AreEqual(_notificationToBuild.NotificationDate, _failedTwiceDate);
        }

        [Test]
        public void StrugglingDismissed()
        {
            var strugglingDismissedEvent = new NotificationDismissedEvent<LearnerSkillStatusNotification>();
            var dismissedDate = _strugglingDate.AddMinutes(1);
            strugglingDismissedEvent.EventDate = dismissedDate;


            _notificationEvents.Add(strugglingDismissedEvent);

            ProcessEventsOnNotification(_notificationEvents.Where(e => e.EventDate <= dismissedDate), _notificationToBuild);

            Assert.That(_notificationToBuild.IsDismissed);
            Assert.AreEqual(_notificationToBuild.NotificationDate, dismissedDate);
        }

        [Test]
        public void StrugglingRead()
        {
            var strugglingReadEvent = new NotificationReadEvent<LearnerSkillStatusNotification>();
            var strugglingReadDate = _strugglingDate.AddMinutes(1);
            strugglingReadEvent.EventDate = strugglingReadDate;

            _notificationEvents.Add(strugglingReadEvent);

            ProcessEventsOnNotification(_notificationEvents.Where(e => e.EventDate <= strugglingReadDate), _notificationToBuild);

            Assert.That(_notificationToBuild.IsRead);
            Assert.AreEqual(_notificationToBuild.NotificationDate, strugglingReadDate);
        }

        [Test]
        public void NewEventsResetRead()
        {
            var strugglingReadEvent = new NotificationReadEvent<LearnerSkillStatusNotification>();
            var strugglingReadDate = _strugglingDate.AddMinutes(1);
            strugglingReadEvent.EventDate = strugglingReadDate;

            _notificationEvents.Add(strugglingReadEvent);

            ProcessEventsOnNotification(_notificationEvents.Where(e => e.EventDate <= strugglingReadDate), _notificationToBuild);

            Assert.IsTrue(_notificationToBuild.IsRead);

            //events up to failed Once
            ProcessEventsOnNotification(_notificationEvents.Where(e => e.EventDate <= _failedOnceDate), _notificationToBuild);

            Assert.IsFalse(_notificationToBuild.IsRead);
            Assert.AreEqual(_notificationToBuild.NotificationDate, _failedOnceDate);
        }

        [Test]
        public void NewEventsResetDismissed()
        {
            var strugglingReadEvent = new NotificationDismissedEvent<LearnerSkillStatusNotification>();
            var strugglingReadDate = _strugglingDate.AddMinutes(1);
            strugglingReadEvent.EventDate = strugglingReadDate;

            _notificationEvents.Add(strugglingReadEvent);

            ProcessEventsOnNotification(_notificationEvents.Where(e => e.EventDate <= strugglingReadDate), _notificationToBuild);
            Assert.IsTrue(_notificationToBuild.IsDismissed);

            //events up to failed Once
            ProcessEventsOnNotification(_notificationEvents.Where(e => e.EventDate <= _failedOnceDate), _notificationToBuild);

            Assert.IsFalse(_notificationToBuild.IsDismissed);
            Assert.AreEqual(_notificationToBuild.NotificationDate, _failedOnceDate);
        }
    }
}
