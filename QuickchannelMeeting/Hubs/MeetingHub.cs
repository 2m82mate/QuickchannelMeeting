using Microsoft.AspNetCore.SignalR;
using QuickchannelMeeting.Models.DbModels;

namespace QuickchannelMeeting.Hubs
{
    public class MeetingHub : Hub, IMeetingHub
    {
        public async Task SendMeetingUpdate(Meeting meeting)
        {
            await Clients.All.SendAsync("ReceiveMeetingUpdate", meeting);
        }
    }
}
