using QuickchannelMeeting.Models.DbModels;

namespace QuickchannelMeeting.Hubs
{
    public interface IMeetingHub
    {
        Task SendMeetingUpdate(Meeting meeting);
    }
}