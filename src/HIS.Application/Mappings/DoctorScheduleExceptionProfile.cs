using AutoMapper;
using HIS.Application.DTOs.DoctorScheduleException;
using HIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Mappings
{
    public class DoctorScheduleExceptionProfile:Profile
    {
        public DoctorScheduleExceptionProfile()
        {
            CreateMap<CreateDoctorScheduleExceptionDto, DoctorScheduleException>().ForMember(dest => dest.Oid, opt => opt.Ignore()); 
            CreateMap<DoctorScheduleException,DoctorScheduleExceptionResponseDto>()
                .ForMember(dest => dest.DayOfWeekNameAr , opt=> opt.MapFrom(src => src.Days.ValueNameAr))
                .ForMember(dest => dest.DayOfWeekNameEn , opt=> opt.MapFrom(src => src.Days.ValueNameEn));


            CreateMap<UpdateDoctorScheduleExceptionDto, DoctorScheduleException>()
           .ForMember(dest => dest.Oid, opt => opt.Ignore())
           .ForMember(dest => dest.Days, opt => opt.Ignore()); 
         
           
        }
    }
}
