namespace QuickchannelMeeting.Models.DbModels
{
    public class Participant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ParticipantRole Role { get; set; }
        public Guid MeetingId { get; set; }
        public Meeting Meeting { get; set; }
        public ICollection<Vote> VoteResults { get; set; }
    }
}
