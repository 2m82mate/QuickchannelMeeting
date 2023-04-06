using AutoMapper;
using QuickchannelMeeting.Enums;
using QuickchannelMeeting.Hubs;
using QuickchannelMeeting.Models.DbModels;
using QuickchannelMeeting.Repositories;

namespace QuickchannelMeeting.Services
{
    public class MachineService : IMachineService
    {
        private readonly IMeetingRepository _meetings;
        private readonly IMachineRepository _machineRepository;
        private readonly IMapper _mapper;
        private readonly IMeetingHub _hub;

        public MachineService(IMeetingRepository meetings, IMachineRepository machineRepository, IMeetingHub hub, IMapper mapper)
        {
            _meetings = meetings;
            _machineRepository = machineRepository;
            _mapper = mapper;
            _hub = hub;
        }
        public async Task<MeetingDto> Fire(Guid meetingId, MeetingTrigger trigger)
        {
           var meeting = await _meetings.GetMeetingAsync(meetingId);
            if (meeting == null)
            {
                throw new Exception($"Could not find meeting: {meetingId}");
            }

            var transition = await _machineRepository.GetTransition((MeetingState)meeting.StateId, trigger);
            if(transition==null)
            {
                throw new Exception($"Could not find transition for: state.{((MeetingState)meeting.StateId)}, trigger.{trigger}");
            }

            if (transition.TransitionConditions.Count() > 0)
            {
                var conditionPassed = transition.TransitionConditions.All(x => EvaluateCondition(x.Condition));
                if (!conditionPassed)
                {
                    throw new Exception("Could not pass condition");
                }
            }

            if (transition.TransitionOperations.Count() > 0)
            {
                foreach (var transOp in transition.TransitionOperations)
                {
                    meeting = DoOperation(meeting, transOp.Operation);
                }
            }
            meeting.StateId = transition.ToStateId;
            await _meetings.UpdateMeetingAsync(meeting);
            await _hub.SendMeetingUpdate(meeting);
            return _mapper.Map<MeetingDto>(meeting);
        }

        private bool EvaluateCondition(Condition condition)
        {
            switch ((MeetingCondition)condition.Id)
            {
                case MeetingCondition.Test:
                    return true;
                default:
                    return true;
            }
        }

        private Meeting DoOperation(Meeting meeting, Operation operation)
        {
            switch ((MeetingOperation)operation.Id)
            {
                case MeetingOperation.StartMeeting:
                    Console.WriteLine("Doing start meeting things");
                    break;
                default:
                    break;

            }

            return meeting;
        }
    }
}
