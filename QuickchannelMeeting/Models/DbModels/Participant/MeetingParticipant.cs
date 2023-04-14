namespace QuickchannelMeeting.Models.DbModels
{
    public class MeetingParticipant : Participant
    {
        public Guid MeetingId { get; set; }
        public Meeting Meeting { get; set; }
        public ICollection<Vote> VoteResults { get; set; }
    }
}
