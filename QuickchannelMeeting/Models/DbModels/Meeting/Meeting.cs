using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickchannelMeeting.Models.DbModels
{
    public class Meeting
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; internal set; }
        public int StateId { get; set; }
        public State State { get; set; }
        public ICollection<AgendaPoint> AgendaPoints { get; set; }
        public ICollection<Participant> Participants { get; set; }
    }
}
