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
            CreateMap<DoctorScheduleException,DoctorScheduleExceptionResponseDto>();
        }
    }
}
