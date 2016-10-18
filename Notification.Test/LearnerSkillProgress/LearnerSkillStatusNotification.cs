namespace Notification.Test.LearnerSkillProgress
{
    class LearnerSkillStatusNotification : NotificationBase
    {
        public int LearnerId { get; set; }
        public int SkillId { get; set; }
        public int UserSkillAssignmentId { get; set; }
        public LearnerSkillProgressStatus ProgressStatus { get; set; }
        public override NotificationType NotificationType => NotificationType.LearnerSkillStatus;

        public LearnerSkillStatusNotification()
        {
            
        }

        public LearnerSkillStatusNotification(int userSkillAssignmentId)
        {
            CorrelationId = userSkillAssignmentId;
        }
    }
}