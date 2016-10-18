using System;

namespace Notification.Test.LearnerSkillProgress
{
    class LearnerSkillProgressEvent : NotificationEventBase<LearnerSkillStatusNotification>
    {
        public int LearnerId { get; set; }
        public int SkillId { get; set; }
        public LearnerSkillProgressStatus ProgressStatus { get; set; }

        public LearnerSkillProgressEvent(LearnerSkillProgressStatus status, DateTime date)
        {
            ProgressStatus = status;
            EventDate = date;
        }

        public override void ProcessNotification(LearnerSkillStatusNotification notification)
        {
            base.ProcessNotification(notification);
            notification.ProgressStatus = ProgressStatus;
            notification.IsDismissed = false;
            notification.IsRead = false;
        }
    }
}