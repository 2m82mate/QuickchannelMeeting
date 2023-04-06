using AutoMapper;
using QuickchannelMeeting.Enums;
using QuickchannelMeeting.Models.DbModels;
namespace QuickchannelMeeting.Mappers
{
    public class MeetingMapperProfile : Profile
    {
        public MeetingMapperProfile()
        {
            CreateMap<Meeting, MeetingDto>()
                .ForMember(dest => dest.State, opt => opt.MapFrom(x => ((MeetingState)x.State.Id).ToString()));
        }
    }
}
