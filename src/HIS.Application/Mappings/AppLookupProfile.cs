using AutoMapper;
using HIS.Application.DTOs.AppLookup;
using HIS.Domain.Entities;

namespace HIS.Application.Mappings;

public class AppLookupProfile : Profile
{
    public AppLookupProfile()
    {
        // AppLookupMaster mappings
        CreateMap<AppLookupMaster, AppLookupMasterDto>()
            .ReverseMap();

        CreateMap<CreateAppLookupMasterDto, AppLookupMaster>()
            .ForMember(dest => dest.Oid, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.LookupDetails, opt => opt.Ignore());

        // AppLookupDetail mappings
        CreateMap<AppLookupDetail, AppLookupDetailDto>()
            .ForMember(dest => dest.LookupMasterID, opt => opt.MapFrom(src => src.MasterID))
            .ForMember(dest => dest.MasterLookupCode, opt => opt.MapFrom(src => src.Master.LookupCode))
            .ReverseMap();

        CreateMap<CreateAppLookupDetailDto, AppLookupDetail>()
            .ForMember(dest => dest.MasterID, opt => opt.MapFrom(src => src.LookupMasterID))
            .ForMember(dest => dest.Oid, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Master, opt => opt.Ignore());
    }
}