using AutoMapper;
using HIS.Application.DTOs.Diagnosis;
using HIS.Application.DTOs.Encounter;
using HIS.Application.DTOs.Prescription;
using HIS.Domain.Entities;

namespace HIS.Application.Mappings;

public class EncounterProfile : Profile
{
    public EncounterProfile()
    {
        CreateMap<Encounter, EncounterDto>()
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.FullNameEn))
            .ForMember(dest => dest.PatientMRN, opt => opt.MapFrom(src => src.Patient.MRN))
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.User.FullName))
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch != null ? src.Branch.Name : null));

        CreateMap<CreateEncounterDto, Encounter>()
            .ForMember(dest => dest.Oid, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Appointment, opt => opt.Ignore())
            .ForMember(dest => dest.Patient, opt => opt.Ignore())
            .ForMember(dest => dest.Doctor, opt => opt.Ignore())
            .ForMember(dest => dest.Branch, opt => opt.Ignore())
            .ForMember(dest => dest.Diagnoses, opt => opt.Ignore())
            .ForMember(dest => dest.Prescriptions, opt => opt.Ignore());

        CreateMap<Diagnosis, DiagnosisDto>();
        CreateMap<CreateDiagnosisDto, Diagnosis>()
            .ForMember(dest => dest.Oid, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Encounter, opt => opt.Ignore());

        CreateMap<Prescription, PrescriptionDto>();
        CreateMap<CreatePrescriptionDto, Prescription>()
            .ForMember(dest => dest.Oid, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Encounter, opt => opt.Ignore());
    }
}