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
            //CreateMap<DoctorScheduleMaster, DoctorScheduleDto>()
            //.ForMember(dest => dest.DayOfWeekNameEn, opt => opt.MapFrom(src => src.DayOfweek.ValueNameEn))
            //.ForMember(dest => dest.DayOfWeekNameAr, opt => opt.MapFrom(src => src.DayOfweek.ValueNameAr))
            //.ReverseMap();

            // Map a single-create DTO into a master with one detail item.
            // Define a direct mapping for the detail and let AutoMapper project a single-element
            // collection when creating the master, keeping mapping logic concise and testable.
            CreateMap<CreateDoctorScheduleDto, DoctorScheduleDetail>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                .ForMember(dest => dest.SlotDurationMinutes, opt => opt.MapFrom(src => src.SlotDurationMinutes))
                .ForMember(dest => dest.DayOfWeekId, opt => opt.MapFrom(src => src.DayOfWeekId));

            CreateMap<CreateDoctorScheduleDto, DoctorScheduleMaster>()
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => new[] { src }));
            CreateMap<UpdateDoctorSchdeuelDto, DoctorScheduleMaster>().ForMember(dest => dest.Oid, opt => opt.Ignore());
            CreateMap<CreateDoctorScheduleCommand, DoctorScheduleMaster>().ForMember(dest => dest.Oid, opt => opt.Ignore());
            //CreateMap<List<CreateDoctorScheduleDto>, List<DoctorSchedule>>();
            //CreateMap<List<DoctorSchedule>, List<DoctorScheduleDto>>();

            
            
            //CreateMap<DoctorSchedulesListDto, DoctorSchedule>()
            // .ForMember(dest => dest.DayOfWeekId, opt => opt.MapFrom(src => src.DayOfWeekId))
            // .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
            // .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
            // .ForMember(dest => dest.SlotDurationMinutes, opt => opt.MapFrom(src => src.SlotDurationMinutes));
            CreateMap<DoctorSchedulesListDto, DoctorScheduleDetail>();
            CreateMap<CreateDoctorScheduleBulkDto, DoctorScheduleMaster>()
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.DoctorScheduleDetailList));
            CreateMap<DoctorScheduleMaster,DoctorScheduleBulkResponseDto>();
            
                

            CreateMap<DoctorScheduleDetail, DoctorScheduleDetailResponseDto>()
                .ForMember(dest => dest.DayOfWeekNameEn, opt => opt.MapFrom(src => src.DayOfweek != null ? src.DayOfweek.ValueNameEn : null))
                .ForMember(dest => dest.DayOfWeekNameAr, opt => opt.MapFrom(src => src.DayOfweek != null ? src.DayOfweek.ValueNameAr : null));

            CreateMap<DoctorScheduleMaster, DoctorScheduleMasterDetailDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status != null ? src.Status.ValueNameEn : null))
                .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch != null ? src.Branch.Name : null))
                .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src => src.Specialty != null ? src.Specialty.NameEn : null));
            CreateMap<DoctorScheduleMaster, ScheduleWithNoDetailsDto>()
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor != null && src.Doctor.User != null ? src.Doctor.User.FullName : src.Doctor.FirstNameAr))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status != null ? src.Status.ValueNameEn : null))
                .ForMember(dest => dest.SpecialtyName, opt => opt.MapFrom(src => src.Specialty != null ? src.Specialty.NameEn : (src.Doctor != null && src.Doctor.Specialty != null ? src.Doctor.Specialty.NameEn : null)))
                .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch != null ? src.Branch.Name : null))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.BranchId))
                .ForMember(dest => dest.SpecialtyId, opt => opt.MapFrom(src => src.SpecialtyId));


              CreateMap<DoctorScheduleMaster, DoctorScheduleDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status != null ? src.Status.ValueNameEn : null))
                .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch != null ? src.Branch.Name : null))
                .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src => src.Specialty != null ? src.Specialty.NameEn : null));
            CreateMap<DoctorScheduleDetail, DoctorSchedulesListDto>()
                .ForMember(dest => dest.DayOfWeekNameAr,opt => opt.MapFrom(src => src.DayOfweek.ValueNameAr))
                .ForMember(dest => dest.DayOfWeekNameEn, opt => opt.MapFrom(src => src.DayOfweek.ValueNameEn));
            CreateMap<DoctorScheduleMaster, CreateSingleScheduleResponse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status != null ? src.Status.ValueNameEn : null))
                .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch != null ? src.Branch.Name : null))
                .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src => src.Specialty != null ? src.Specialty.NameEn : null));
            
            CreateMap<UpdateDetailsDto, DoctorScheduleDetail>();
            
            CreateMap<CreateDetailsDto, DoctorScheduleDetail>();
            CreateMap<CreateDetailDto, DoctorScheduleDetail>();
            
        }
    }
          
}
