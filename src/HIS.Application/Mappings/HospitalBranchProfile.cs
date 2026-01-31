using AutoMapper;
using HIS.Application.DTOs.HospitalBranch;
using HIS.Domain.Entities;

namespace HIS.Application.Mappings;

public class HospitalBranchProfile : Profile
{
    public HospitalBranchProfile()
    {
        CreateMap<HospitalBranch, HospitalBranchDto>().ReverseMap();

        CreateMap<CreateHospitalBranchDto, HospitalBranch>()
            .ForMember(dest => dest.Oid, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Doctors, opt => opt.Ignore())
            .ForMember(dest => dest.Patients, opt => opt.Ignore())
            .ForMember(dest => dest.Appointments, opt => opt.Ignore());

        CreateMap<UpdateHospitalBranchDto, HospitalBranch>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Doctors, opt => opt.Ignore())
            .ForMember(dest => dest.Patients, opt => opt.Ignore())
            .ForMember(dest => dest.Appointments, opt => opt.Ignore());
    }
}