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
    }
}
