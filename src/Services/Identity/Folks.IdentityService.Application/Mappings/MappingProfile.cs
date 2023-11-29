using AutoMapper;

using Folks.IdentityService.Application.Features.Users.Dto;
using Folks.IdentityService.Domain.Entities;

namespace Folks.IdentityService.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile() 
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}
