namespace QuickchannelMeeting.Models.DbModels
{
    public class TransitionCondition
    {
        public int TransitionId { get; set; }
        public Transition Transition { get; set; }

        public int ConditionId { get; set; }
        public Condition Condition { get; set; }
    }
}
