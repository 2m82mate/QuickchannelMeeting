namespace QuickchannelMeeting.Models.DbModels
{
    public class Vote
    {
        public Guid Id { get; set; }
        public string Result { get; set; }
        public Guid ParticipantId { get; set; }
        public Participant Participant { get; set; }
        public Guid VotingId { get; set; }
        public Voting Voting { get; set; }
    }
}
