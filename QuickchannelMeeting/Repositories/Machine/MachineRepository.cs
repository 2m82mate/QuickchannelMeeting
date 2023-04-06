using Microsoft.EntityFrameworkCore;
using QuickchannelMeeting.Contexts;
using QuickchannelMeeting.Enums;
using QuickchannelMeeting.Models.DbModels;

namespace QuickchannelMeeting.Repositories
{
    public class MachineRepository : IMachineRepository
    {
        private PgContext _context;

        public MachineRepository(PgContext context)
        {
            _context = context;
        }

        public async Task<Transition?> GetTransition(MeetingState state, MeetingTrigger trigger)
        {
            return await _context.Transitions
                .Include(x=>x.ToState)
                .Include(x => x.TransitionConditions)
                .ThenInclude(x => x.Condition)
                .Include(x=>x.TransitionOperations)
                .ThenInclude(x => x.Operation)
                .FirstOrDefaultAsync(x => x.TriggerId == (int)trigger && x.FromStateId == (int)state);
        }
    }
}
