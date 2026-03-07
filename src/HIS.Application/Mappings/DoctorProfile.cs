using AutoMapper;
using HIS.Application.DTOs.Doctor;
using HIS.Domain.Entities;

namespace HIS.Application.Mappings;

public class DoctorProfile : Profile
{
    public DoctorProfile()
    {
        CreateMap<Doctor, DoctorDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User != null ? src.User.Username : null))
            .ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => src.Gender != null ? src.Gender.ValueNameEn : null))
            .ForMember(dest => dest.LicenseTypeName, opt => opt.MapFrom(src => src.LicenseType != null ? src.LicenseType.ValueNameEn : null))
            .ForMember(dest => dest.SpecialtyNameEn, opt => opt.MapFrom(src => src.Specialty != null ? src.Specialty.NameEn : null))
            .ForMember(dest => dest.SpecialtyNameAr, opt => opt.MapFrom(src => src.Specialty != null ? src.Specialty.NameAr : null))
            .ForMember(dest => dest.SubSpecialtyName, opt => opt.MapFrom(src => src.SubSpecialty != null ? src.SubSpecialty.ValueNameEn : null))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.ValueNameEn : null))
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch != null ? src.Branch.Name : null));

        CreateMap<CreateDoctorDto, Doctor>()
            .ForMember(dest => dest.Oid, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedBy, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Gender, opt => opt.Ignore())
            .ForMember(dest => dest.LicenseType, opt => opt.Ignore())
            .ForMember(dest => dest.Specialty, opt => opt.Ignore())
            .ForMember(dest => dest.SubSpecialty, opt => opt.Ignore())
            .ForMember(dest => dest.Department, opt => opt.Ignore())
            .ForMember(dest => dest.Branch, opt => opt.Ignore())
            .ForMember(dest => dest.Appointments, opt => opt.Ignore())
            .ForMember(dest => dest.Encounters, opt => opt.Ignore())
            .ForMember(dest => dest.Schedules, opt => opt.Ignore())
            .ForMember(dest => dest.ScheduleExceptions, opt => opt.Ignore())
            .ForMember(dest => dest.TimeSlots, opt => opt.Ignore())
            .ForMember(dest => dest.DoctorBranches, opt => opt.Ignore())
            .ForMember(dest => dest.DoctorAttachments, opt => opt.Ignore());

        CreateMap<UpdateDoctorDto, Doctor>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedBy, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Gender, opt => opt.Ignore())
            .ForMember(dest => dest.LicenseType, opt => opt.Ignore())
            .ForMember(dest => dest.Specialty, opt => opt.Ignore())
            .ForMember(dest => dest.SubSpecialty, opt => opt.Ignore())
            .ForMember(dest => dest.Department, opt => opt.Ignore())
            .ForMember(dest => dest.Branch, opt => opt.Ignore())
            .ForMember(dest => dest.Appointments, opt => opt.Ignore())
            .ForMember(dest => dest.Encounters, opt => opt.Ignore())
            .ForMember(dest => dest.Schedules, opt => opt.Ignore())
            .ForMember(dest => dest.ScheduleExceptions, opt => opt.Ignore())
            .ForMember(dest => dest.TimeSlots, opt => opt.Ignore())
            .ForMember(dest => dest.DoctorBranches, opt => opt.Ignore())
            .ForMember(dest => dest.DoctorAttachments, opt => opt.Ignore());

        // DoctorBranch mappings
        CreateMap<DoctorBranch, DoctorBranchDto>()
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch != null ? src.Branch.Name : null));

        CreateMap<CreateDoctorBranchDto, DoctorBranch>()
            .ForMember(dest => dest.Oid, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Doctor, opt => opt.Ignore())
            .ForMember(dest => dest.Branch, opt => opt.Ignore());

        // DoctorAttachment mappings
        CreateMap<DoctorAttachment, DoctorAttachmentDto>()
            .ForMember(dest => dest.AttachmentTypeName, opt => opt.MapFrom(src => src.AttachmentType != null ? src.AttachmentType.ValueNameEn : null));

        CreateMap<CreateDoctorAttachmentDto, DoctorAttachment>()
            .ForMember(dest => dest.Oid, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UploadedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Doctor, opt => opt.Ignore())
            .ForMember(dest => dest.AttachmentType, opt => opt.Ignore());
    }
}