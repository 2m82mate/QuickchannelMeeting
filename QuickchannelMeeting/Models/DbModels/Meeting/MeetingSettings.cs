using System.ComponentModel.DataAnnotations;
using QuickchannelMeeting.Models.DbModels;

namespace QuickchannelMeeting.Models
{
    public class MeetingSettings
    {
        [Key]
        public int Id { get; set; }
        public Guid MeetingId { get; set; }
        public Meeting Meeting { get; set; }
        public bool RequireRollCall { get; set; } = false;
    }
}


