namespace QuickchannelMeeting.Models.DbModels
{
    public class TransitionOperation
    {
        public int TransitionId { get; set; }
        public Transition Transition { get; set; }

        public int OperationId { get; set; }
        public Operation Operation { get; set; }
    }
}
