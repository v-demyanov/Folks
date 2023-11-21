using AutoMapper;

using Folks.ChatService.Application.Features.Chats.Queries.GetChatsQuery;
using Folks.ChatService.Application.Features.Users.Commands;
using Folks.ChatService.Domain.Entities;

namespace Folks.ChatService.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Chat, ChatDto>().ReverseMap();
        CreateMap<User, AddUserCommand>()
            .ForMember(destination => destination.UserId, options => options.MapFrom(source => source.SourceId))
            .ReverseMap();
    }
}
