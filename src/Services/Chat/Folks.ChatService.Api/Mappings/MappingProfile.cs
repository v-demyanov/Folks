using AutoMapper;
using Folks.ChatService.Application.Features.Users.Commands.AddUserCommand;
using Folks.EventBus.Messages.IdentityService;

namespace Folks.ChatService.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserRegisteredEvent, AddUserCommand>();
    }
}
