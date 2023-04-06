using QuickchannelMeeting.Enums;
using QuickchannelMeeting.Models.DbModels;

namespace QuickchannelMeeting.Services
{
    public interface IMachineService
    {
        Task<MeetingDto> Fire(Guid meetingId, MeetingTrigger trigger);
    }
}