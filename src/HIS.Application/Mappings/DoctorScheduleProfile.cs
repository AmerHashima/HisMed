using AutoMapper;
using HIS.Application.Commands.DoctorSchedule;
using HIS.Application.DTOs.DoctorSchedule;
using HIS.Domain.Entities;
namespace HIS.Application.Mappings
{
    public class DoctorScheduleProfile: Profile
    {
        public DoctorScheduleProfile()
        {
            CreateMap<DoctorSchedule, DoctorScheduleDto>()
            .ForMember(dest => dest.DayOfWeekNameEn, opt => opt.MapFrom(src => src.DayOfweek.ValueNameEn))
            .ForMember(dest => dest.DayOfWeekNameAr, opt => opt.MapFrom(src => src.DayOfweek.ValueNameAr))
            .ReverseMap();

            CreateMap<CreateDoctorScheduleDto, DoctorSchedule>();
            //    .ForMember(dest => dest.day, opt => opt.MapFrom(src => src.IdentityType.ValueNameEn))
            CreateMap<UpdateDoctorSchdeuelDto, DoctorSchedule>().ForMember(dest => dest.Oid, opt => opt.Ignore());
            CreateMap<CreateDoctorScheduleCommand, DoctorSchedule>().ForMember(dest => dest.Oid, opt => opt.Ignore());
            //CreateMap<List<CreateDoctorScheduleDto>, List<DoctorSchedule>>();
            //CreateMap<List<DoctorSchedule>, List<DoctorScheduleDto>>();

            CreateMap<CreateDoctorScheduleBulkDto, DoctorSchedule>();
            CreateMap<DoctorScheduleBulkResponseDto, DoctorSchedule>();
        }
    }
}
