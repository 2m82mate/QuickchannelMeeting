namespace QuickchannelMeeting.Models.DbModels
{
    public class Participant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ParticipantRole Role { get; set; }
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
    }
}
