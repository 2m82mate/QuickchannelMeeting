using QuickchannelMeeting.Models;
using QuickchannelMeeting.Models.DbModels;

public class Account
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public MeetingSettings Settings { get; set; }
    public ICollection<Participant> Participants { get; set; }
    public ICollection<Meeting> Meetings { get; set; }
}