using AutoMapper;
using HIS.Application.DTOs.Diagnosis;
using HIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Application.Mappings
{
    public class DiagnosisProfile:Profile
    {
        public DiagnosisProfile()
        {
            CreateMap<CreateDiagnosisDto, Diagnosis>();
            CreateMap<Diagnosis, DiagnosisDto>();
            

        }
        

    }
}
