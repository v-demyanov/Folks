using AutoMapper;

using MongoDB.Bson;

using Folks.ChatService.Domain.Entities;
using Folks.ChatService.Application.Features.Channels.Dto;
using Folks.ChatService.Application.Mappings.Resolvers;
using Folks.ChatService.Application.Features.Groups.Commands.CreateGroupCommand;
using Folks.ChatService.Application.Features.Users.Commands.AddUserCommand;

namespace Folks.ChatService.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<string, ObjectId>().ConvertUsing(source => ObjectId.Parse(source));
        CreateMap<ObjectId, string>().ConvertUsing(source => source.ToString());

        CreateMap<Chat, ChannelDto>()
            .ForMember(destination => destination.Type, options => options.MapFrom(source => ChannelType.Chat))
            .ForMember(destination => destination.Title, options => options.MapFrom<ChannelTitleValueResolver>());

        CreateMap<Group, ChannelDto>()
            .ForMember(destination => destination.Type, options => options.MapFrom(source => ChannelType.Group));

        CreateMap<AddUserCommand, User>()
            .ForMember(destination => destination.Id, options => options.MapFrom(source => ObjectId.GenerateNewId()))
            .ForMember(destination => destination.SourceId, options => options.MapFrom(source => source.UserId));

        CreateMap<CreateGroupCommand, Group>()
            .ForMember(destination => destination.Id, options => options.MapFrom(source => ObjectId.GenerateNewId()))
            .ForMember(destination => destination.UserIds, options => options.MapFrom<CreateGroupCommandUsersValueResolver>());
    }
}