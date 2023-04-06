using Microsoft.EntityFrameworkCore;
using QuickchannelMeeting.Contexts;
using QuickchannelMeeting.Models.DbModels;

namespace QuickchannelMeeting.Repositories
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly PgContext _context;

        public MeetingRepository(PgContext context)
        {
            _context = context;
        }

        public async Task<Meeting?> GetMeetingAsync(Guid meetingId)
        {
            return await _context.Meetings
                .Include(m => m.AgendaPoints)
                .Include(m => m.State)
                .FirstOrDefaultAsync(x => x.Id == meetingId);
        }

        public async Task UpdateMeetingAsync(Meeting meeting)
        {
            _context.Update(meeting);
            await _context.SaveChangesAsync();
        }
    }
}
