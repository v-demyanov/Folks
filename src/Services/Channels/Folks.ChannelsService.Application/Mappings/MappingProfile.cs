using AutoMapper;

using MongoDB.Bson;

using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Application.Features.Channels.Dto;
using Folks.ChannelsService.Application.Mappings.Resolvers;
using Folks.ChannelsService.Application.Features.Groups.Commands.CreateGroupCommand;
using Folks.ChannelsService.Application.Features.Users.Commands.AddUserCommand;
using Folks.ChannelsService.Application.Features.Channels.Enums;
using Folks.ChannelsService.Application.Features.Messages.Commands.CreateMessageCommand;
using Folks.ChannelsService.Application.Features.Messages.Dto;
using Folks.ChannelsService.Application.Features.Users.Dto;

namespace Folks.ChannelsService.Application.Mappings;

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

        CreateMap<CreateGroupCommand, Group>()
            .ForMember(destination => destination.Id, options => options.MapFrom(source => ObjectId.GenerateNewId()))
            .ForMember(destination => destination.UserIds, options => options.MapFrom<CreateGroupCommandUsersValueResolver>());

        CreateMap<AddUserCommand, User>()
            .ForMember(destination => destination.Id, options => options.MapFrom(source => ObjectId.GenerateNewId()))
            .ForMember(destination => destination.SourceId, options => options.MapFrom(source => source.UserId));
        CreateMap<User, UserDto>()
            .ForMember(destination => destination.Id, options => options.MapFrom(source => source.SourceId));

        CreateMap<CreateMessageCommand, Message>()
            .ForMember(destination => destination.Id, options => options.MapFrom(source => ObjectId.GenerateNewId()))
            .ForMember(destination => destination.GroupId, options => options.MapFrom(source =>
                source.ChannelType == ChannelType.Group ? source.ChannelId : null))
            .ForMember(destination => destination.ChatId, options => options.MapFrom(source =>
                source.ChannelType == ChannelType.Chat ? source.ChannelId : null))
            .ForMember(destination => destination.OwnerId, options => options.MapFrom<CreateMessageCommandOwnerIdValueResolver>());

        CreateMap<Message, MessageDto>()
            .ForMember(destination => destination.ChannelId, options => options.MapFrom(source => source.ChatId ?? source.GroupId))
            .ForMember(destination => destination.Owner, options => options.MapFrom<MessageOwnerIdValueResolver>());
    }
}