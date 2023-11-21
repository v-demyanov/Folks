using AutoMapper;

using Folks.EventBus.Messages.IdentityService;
using Folks.IdentityService.Domain.Entities;

namespace Folks.IdentityService.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserRegistered>()
            .ForMember(destination => destination.UserId, options => options.MapFrom(source => source.Id))
            .ReverseMap();
    }
}
