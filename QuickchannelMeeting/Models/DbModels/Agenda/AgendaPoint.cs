namespace QuickchannelMeeting.Models.DbModels
{
    public class AgendaPoint
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public Guid MeetingId { get; set; }
        public Meeting Meeting { get; set; }
        public ICollection<Voting> Votings { get; set; }
    }
}
