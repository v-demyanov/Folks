using AutoMapper;
using Folks.ChannelsService.Api.Common.Models;
using Folks.ChannelsService.Api.Mappings.Resolvers;
using Folks.ChannelsService.Application.Features.Channels.Commands.LeaveChannelCommand;
using Folks.ChannelsService.Application.Features.Messages.Commands.CreateMessageCommand;
using Folks.ChannelsService.Application.Features.Users.Commands.AddUserCommand;
using Folks.EventBus.Messages.IdentityService;

namespace Folks.ChannelsService.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserRegisteredEvent, AddUserCommand>();
        CreateMap<SendMessageRequest, CreateMessageCommand>();
        CreateMap<LeaveChannelRequest, LeaveChannelCommand>()
            .ForMember(destination => destination.UserId, options => options.MapFrom<LeaveChannelCommandUserIdValueResolver>());
    }
}
