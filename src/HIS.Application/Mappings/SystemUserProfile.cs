using AutoMapper;
using HIS.Application.DTOs.SystemUserSpace;
using HIS.Domain.Entities;

namespace HIS.Application.Mappings;

public class SystemUserProfile : Profile
{
    public SystemUserProfile()
    {
        CreateMap<SystemUser, SystemUserDto>()
            .ReverseMap();

        CreateMap<CreateSystemUserDto, SystemUser>();

        CreateMap<UpdateSystemUserDto, SystemUser>()
            ;
    }
}