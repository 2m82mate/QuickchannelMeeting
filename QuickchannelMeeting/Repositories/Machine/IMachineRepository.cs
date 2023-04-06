using QuickchannelMeeting.Enums;
using QuickchannelMeeting.Models.DbModels;

namespace QuickchannelMeeting.Repositories
{
    public interface IMachineRepository
    {
        Task<Transition?> GetTransition(MeetingState state, MeetingTrigger trigger);
    }
}