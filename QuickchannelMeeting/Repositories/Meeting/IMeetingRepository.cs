using QuickchannelMeeting.Models.DbModels;
namespace QuickchannelMeeting.Repositories
{
    public interface IMeetingRepository
    {
        Task<Meeting?> GetMeetingAsync(Guid meetingId);
        Task UpdateMeetingAsync(Meeting meeting);
    }
}
