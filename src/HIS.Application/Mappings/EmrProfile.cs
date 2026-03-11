using AutoMapper;
using HIS.Application.Commands.EmrIcd110;
using HIS.Application.DTOs.Emr_Icd110;
using HIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Mappings
{
    public class EmrProfile:Profile
    {
        public EmrProfile()
        {
            CreateMap<CreateEmrIcd110Command,emr_icd110>();
            CreateMap<emr_icd110, EmrResponseDto>();
        }
    }
}
