using AutoMapper;
using Nnow.Api.Dtos;
using Nnow.Application.Commands;
using Nnow.Domain.Entities;

namespace Nnow.Api;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<PermissionType, PermissionTypeDto>().ForSourceMember(p => p.Permissions, a => a.DoNotValidate());
        CreateMap<Permission, PermissionDto>().ForSourceMember(p => p.PermissionType, a => a.DoNotValidate());
        CreateMap(typeof(ApplicationResponse<>), typeof(ApplicationResponseDto<>));
        CreateMap<ApplicationResponse<Permission>, ApplicationResponseDto<PermissionDto>>();
    }
}