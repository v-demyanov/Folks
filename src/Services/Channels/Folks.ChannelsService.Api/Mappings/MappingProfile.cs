using AutoMapper;
using Folks.ChannelsService.Application.Features.Users.Commands.AddUserCommand;
using Folks.EventBus.Messages.IdentityService;

namespace Folks.ChannelsService.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserRegisteredEvent, AddUserCommand>();
    }
}
