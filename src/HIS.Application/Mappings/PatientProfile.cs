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
            .ForMember(dest => dest.IdentityTypeName, opt => opt.MapFrom(src => src.IdentityType.ValueNameEn))
            .ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => src.Gender.ValueNameEn))
            .ForMember(dest => dest.NationalityName, opt => opt.MapFrom(src => src.Nationality != null ? src.Nationality.ValueNameEn : null))
            .ForMember(dest => dest.MaritalStatusName, opt => opt.MapFrom(src => src.MaritalStatus != null ? src.MaritalStatus.ValueNameEn : null))
            .ForMember(dest => dest.BloodGroupName, opt => opt.MapFrom(src => src.BloodGroup != null ? src.BloodGroup.ValueNameEn : null))
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name));

        CreateMap<CreatePatientDto, Patient>()
            .ForMember(dest => dest.Oid, opt => opt.Ignore())
            .ForMember(dest => dest.MRN, opt => opt.Ignore())
            .ForMember(dest => dest.FullNameAr, opt => opt.Ignore())
            .ForMember(dest => dest.FullNameEn, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Age, opt => opt.Ignore())
            // Ignore navigation properties
            .ForMember(dest => dest.IdentityType, opt => opt.Ignore())
            .ForMember(dest => dest.Gender, opt => opt.Ignore())
            .ForMember(dest => dest.Nationality, opt => opt.Ignore())
            .ForMember(dest => dest.MaritalStatus, opt => opt.Ignore())
            .ForMember(dest => dest.BloodGroup, opt => opt.Ignore())
            .ForMember(dest => dest.Branch, opt => opt.Ignore())
            .ForMember(dest => dest.Appointments, opt => opt.Ignore())
            .ForMember(dest => dest.Encounters, opt => opt.Ignore());

        CreateMap<UpdatePatientDto, Patient>()
            .ForMember(dest => dest.MRN, opt => opt.Ignore())
            .ForMember(dest => dest.FullNameAr, opt => opt.Ignore())
            .ForMember(dest => dest.FullNameEn, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Age, opt => opt.Ignore())
            // Ignore navigation properties
            .ForMember(dest => dest.IdentityType, opt => opt.Ignore())
            .ForMember(dest => dest.Gender, opt => opt.Ignore())
            .ForMember(dest => dest.Nationality, opt => opt.Ignore())
            .ForMember(dest => dest.MaritalStatus, opt => opt.Ignore())
            .ForMember(dest => dest.BloodGroup, opt => opt.Ignore())
            .ForMember(dest => dest.Branch, opt => opt.Ignore())
            .ForMember(dest => dest.Appointments, opt => opt.Ignore())
            .ForMember(dest => dest.Encounters, opt => opt.Ignore());
    }
}