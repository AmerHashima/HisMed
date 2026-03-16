using AutoMapper;
using HIS.Application.Commands.DoctorSchedule;
using HIS.Application.DTOs.DoctorSchedule;
using HIS.Domain.Entities;

namespace HIS.Application.Mappings
{
    public class DoctorScheduleProfile : Profile
    {
        public DoctorScheduleProfile()
        {
            // CreateDoctorScheduleDto → DoctorScheduleDetail (detail-level fields)
            CreateMap<CreateDoctorScheduleDto, DoctorScheduleDetail>()
                .ForMember(dest => dest.Oid, opt => opt.Ignore())
                .ForMember(dest => dest.MasterId, opt => opt.Ignore());

            // CreateDoctorScheduleDto → DoctorScheduleMaster (master-level fields + nested detail)
            CreateMap<CreateDoctorScheduleDto, DoctorScheduleMaster>()
                .ForMember(dest => dest.Oid, opt => opt.Ignore())
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => new List<DoctorScheduleDetail>
                {
                    new DoctorScheduleDetail
                    {
                        DayOfWeekId = src.DayOfWeekId,
                        StartTime = src.StartTime,
                        EndTime = src.EndTime,
                        SlotDurationMinutes = src.SlotDurationMinutes
                    }
                }));

            // UpdateDoctorSchdeuelDto → DoctorScheduleMaster
            CreateMap<UpdateDoctorSchdeuelDto, DoctorScheduleMaster>()
                .ForMember(dest => dest.Oid, opt => opt.Ignore());

            // CreateDoctorScheduleCommand → DoctorScheduleMaster
            CreateMap<CreateDoctorScheduleCommand, DoctorScheduleMaster>()
                .ForMember(dest => dest.Oid, opt => opt.Ignore());

            // DoctorScheduleMaster → DoctorScheduleDto (flatten master + first detail + nav properties)
            CreateMap<DoctorScheduleMaster, DoctorScheduleDto>()
                .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ValueNameEn))
                .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src => src.Specialty.NameEn))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.Details.First().StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.Details.First().EndTime))
                .ForMember(dest => dest.SlotDurationMinutes, opt => opt.MapFrom(src => src.Details.First().SlotDurationMinutes))
                .ForMember(dest => dest.DayOfWeekNameEn, opt => opt.MapFrom(src => src.Details.First().DayOfweek.ValueNameEn))
                .ForMember(dest => dest.DayOfWeekNameAr, opt => opt.MapFrom(src => src.Details.First().DayOfweek.ValueNameAr));

            // DoctorScheduleDetail → DoctorScheduleDetailDto
            CreateMap<DoctorScheduleDetail, DoctorScheduleDetailDto>()
                .ForMember(dest => dest.DayOfWeekNameEn, opt => opt.MapFrom(src => src.DayOfweek.ValueNameEn))
                .ForMember(dest => dest.DayOfWeekNameAr, opt => opt.MapFrom(src => src.DayOfweek.ValueNameAr));

            // DoctorScheduleMaster → GetDoctorScheduleMasterAndDetailDto
            CreateMap<DoctorScheduleMaster, GetDoctorScheduleMasterAndDetailDto>()
                .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ValueNameEn))
                .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src => src.Specialty.NameEn));

            // DoctorScheduleMaster → DoctorScheduleBulkResponseDto
            CreateMap<DoctorScheduleMaster, DoctorScheduleBulkResponseDto>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.Details.First().StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.Details.First().EndTime))
                .ForMember(dest => dest.SlotDurationMinutes, opt => opt.MapFrom(src => src.Details.First().SlotDurationMinutes));
        }
    }
}
