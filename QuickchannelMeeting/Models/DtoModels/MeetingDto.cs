using QuickchannelMeeting.Enums;

namespace QuickchannelMeeting.Models.DbModels
{
    public class MeetingDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; internal set; }
        public string State { get; set; }
    }
}
