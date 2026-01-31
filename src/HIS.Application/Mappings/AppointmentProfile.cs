using AutoMapper;
using HIS.Application.DTOs.Appointment;
using HIS.Domain.Entities;

namespace HIS.Application.Mappings;

public class AppointmentProfile : Profile
{
    public AppointmentProfile()
    {
        CreateMap<Appointment, AppointmentDto>()
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.FullNameEn))
            .ForMember(dest => dest.PatientMRN, opt => opt.MapFrom(src => src.Patient.MRN))
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.User.FullName))
            .ForMember(dest => dest.SpecialtyName, opt => opt.MapFrom(src => src.Doctor.Specialty.NameEn))
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch != null ? src.Branch.Name : null));

        CreateMap<CreateAppointmentDto, Appointment>()
            .ForMember(dest => dest.Oid, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Patient, opt => opt.Ignore())
            .ForMember(dest => dest.Doctor, opt => opt.Ignore())
            .ForMember(dest => dest.Branch, opt => opt.Ignore())
            .ForMember(dest => dest.Encounter, opt => opt.Ignore())
            .ForMember(dest => dest.TimeSlot, opt => opt.Ignore());
    }
}