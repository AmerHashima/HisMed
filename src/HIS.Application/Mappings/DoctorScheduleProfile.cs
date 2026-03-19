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

            CreateMap<CreateDoctorScheduleDto, DoctorScheduleMaster>()
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => new List<DoctorScheduleDetail>()
                {
                    new DoctorScheduleDetail
                    {
                        
                        StartTime =src.StartTime,
                        EndTime =src.EndTime,
                        SlotDurationMinutes = src.SlotDurationMinutes,
                        DayOfWeekId = src.DayOfWeekId
                    }
                }));
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
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.DoctorSchedulesList));
            CreateMap<DoctorScheduleMaster,DoctorScheduleBulkResponseDto>();
            
                

            CreateMap<DoctorScheduleDetail, DoctorScheduleDetailResponseDto>()
                .ForMember(dest => dest.DayOfWeekNameEn, opt => opt.MapFrom(src => src.DayOfweek != null ? src.DayOfweek.ValueNameEn : null))
                .ForMember(dest => dest.DayOfWeekNameAr, opt => opt.MapFrom(src => src.DayOfweek != null ? src.DayOfweek.ValueNameAr : null));

            CreateMap<DoctorScheduleMaster, DoctorScheduleMasterDetailDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status != null ? src.Status.ValueNameEn : null))
                .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch != null ? src.Branch.Name : null))
                .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src => src.Specialty != null ? src.Specialty.NameEn : null));
            CreateMap<DoctorScheduleMaster, ScheduleWithNoDetailsDto>()
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.User.FullName))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ValueNameEn))
                .ForMember(dest => dest.SpecialtyName, opt => opt.MapFrom(src => src.Doctor.Specialty.NameEn))
                .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch != null ? src.Branch.Name : null)); 
                
              CreateMap<DoctorScheduleMaster, DoctorScheduleDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status != null ? src.Status.ValueNameEn : null))
                .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch != null ? src.Branch.Name : null))
                .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src => src.Specialty != null ? src.Specialty.NameEn : null))
                .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src => src.Specialty != null ? src.Specialty.NameEn : null));
            CreateMap<DoctorScheduleDetail, DoctorSchedulesListDto>()
                .ForMember(dest => dest.DayOfWeekNameAr,opt => opt.MapFrom(src => src.DayOfweek.ValueNameAr))
                .ForMember(dest => dest.DayOfWeekNameEn, opt => opt.MapFrom(src => src.DayOfweek.ValueNameEn));
            CreateMap<DoctorScheduleMaster, CreateSingleScheduleResponse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status != null ? src.Status.ValueNameEn : null))
                .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch != null ? src.Branch.Name : null))
                .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src => src.Specialty != null ? src.Specialty.NameEn : null))
                .ForMember(dest => dest.Specialty, opt => opt.MapFrom(src => src.Specialty != null ? src.Specialty.NameEn : null))
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => new List<DoctorScheduleDetailResponseDto>
                {
                    new DoctorScheduleDetailResponseDto
                    {
                        Oid = src.Details.FirstOrDefault().Oid,
                        StartTime =src.Details.FirstOrDefault().StartTime,
                        EndTime= src.Details.FirstOrDefault().EndTime,
                        SlotDurationMinutes = src.Details.FirstOrDefault().SlotDurationMinutes,
                        DayOfWeekId = src.Details.FirstOrDefault().DayOfWeekId,
                        DayOfWeekNameAr = src.Details.FirstOrDefault().DayOfweek.ValueNameAr,
                        DayOfWeekNameEn = src.Details.FirstOrDefault().DayOfweek.ValueNameEn,
                    }
                }));
            
            CreateMap<UpdateDetailsDto, DoctorScheduleDetail>();
            
            CreateMap<CreateDetailsDto, DoctorScheduleDetail>();
            
        }
    }
          
}
