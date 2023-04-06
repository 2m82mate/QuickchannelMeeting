using Microsoft.AspNetCore.Mvc;
using QuickchannelMeeting.Enums;
using QuickchannelMeeting.Services;

namespace QuickchannelMeeting.Controllers
{
    public class MeetingController : Controller
    {
        private readonly IMachineService _machine;

        public MeetingController(IMachineService machine)
        {
            _machine = machine;
        }
       // [HttpPost("{meetingId}/start")]
        [HttpPost("start")]
        public async Task<IActionResult> StartMeeting()
        {
            var meetingId = new Guid("4d336f24-e6de-4d6f-8932-0c6d7bce4993");
            return Ok(await _machine.Fire(meetingId, MeetingTrigger.StartMeeting));
        }
    }
}
