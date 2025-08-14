namespace HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Mappers
{
    using AutoMapper;
    using HustleAddiction.Platform.CalendarApi.Domain.Aggregate.Calendar;
    using HustleAddiction.Platform.CalendarApi.Domain.Services.EventOccurrenceService;
    using HustleAddiction.Platform.CalendarApi.Presentation.WebAPI.Dto.Response;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            this.CreateMap<Calendar, CalendarSummary>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.UUId));

            this.CreateMap<EventDetails, EventSummary>()
                .ForMember(x => x.EventId, opt => opt.MapFrom(src => src.EventId));
        }
    }
}