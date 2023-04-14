using QuickchannelMeeting.Repositories;

namespace QuickchannelMeeting.Services
{
    public class MeetingService
    {
        private readonly IMeetingRepository _meetings;

        public MeetingService(IMeetingRepository meetings)
        {
            _meetings = meetings;
        }

        public async Task<Meeting> StartMeeting(Guid meetingId)
        {
            var meeting = await _meetings.GetMeeting(meetingId);
            
            meeting.StateId = (int)MeetingState.Started;
            await _meetings.UpdateMeeting(meeting);
            return meeting;
        }

    }
}
