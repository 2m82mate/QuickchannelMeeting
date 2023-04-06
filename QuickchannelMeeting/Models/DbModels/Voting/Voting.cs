namespace QuickchannelMeeting.Models.DbModels
{
    public class Voting
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid AgendaPointId { get; set; }
        public AgendaPoint AgendaPoint { get; set; }
        public ICollection<Vote> Votes { get; set; }
    }
}
