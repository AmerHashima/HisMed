using AutoMapper;
using HIS.Application.DTOs.Patient;
using HIS.Domain.Entities;

namespace HIS.Application.Mappings;

public class PatientProfile : Profile
{
    public PatientProfile()
    {
        CreateMap<Patient, PatientDto>()
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
            .ReverseMap();

        CreateMap<CreatePatientDto, Patient>()
            .ForMember(dest => dest.Oid, opt => opt.Ignore())
            .ForMember(dest => dest.MRN, opt => opt.Ignore())
            .ForMember(dest => dest.FullNameAr, opt => opt.Ignore())
            .ForMember(dest => dest.FullNameEn, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

        CreateMap<UpdatePatientDto, Patient>()
            .ForMember(dest => dest.MRN, opt => opt.Ignore())
            .ForMember(dest => dest.FullNameAr, opt => opt.Ignore())
            .ForMember(dest => dest.FullNameEn, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());
    }
}