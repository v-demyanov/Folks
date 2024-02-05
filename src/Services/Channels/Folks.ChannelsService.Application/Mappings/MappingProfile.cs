// Copyright (c) v-demyanov. All rights reserved.

using AutoMapper;

using Folks.ChannelsService.Application.Features.Channels.Common.Dto;
using Folks.ChannelsService.Application.Features.Channels.Common.Enums;
using Folks.ChannelsService.Application.Features.Groups.Commands.CreateGroupCommand;
using Folks.ChannelsService.Application.Features.Groups.Common.Dto;
using Folks.ChannelsService.Application.Features.Messages.Commands.CreateMessageCommand;
using Folks.ChannelsService.Application.Features.Messages.Common.Dto;
using Folks.ChannelsService.Application.Features.Users.Commands.AddUserCommand;
using Folks.ChannelsService.Application.Features.Users.Common.Dto;
using Folks.ChannelsService.Application.Mappings.Resolvers;
using Folks.ChannelsService.Domain.Entities;

using MongoDB.Bson;

namespace Folks.ChannelsService.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        this.CreateMap<string, ObjectId>().ConvertUsing(source => ObjectId.Parse(source));
        this.CreateMap<ObjectId, string>().ConvertUsing(source => source.ToString());

        this.CreateMap<Chat, ChannelDto>()
            .ForMember(destination => destination.Type, options => options.MapFrom(source => ChannelType.Chat))
            .ForMember(destination => destination.Title, options => options.MapFrom<ChatTitleValueResolver>())
            .ForMember(destination => destination.LastMessage, options => options.MapFrom<ChatLastMessageValueResolver>())
            .ForMember(destination => destination.UnreadMessagesCount, options => options.MapFrom<ChatUnreadMessagesCountValueResolver>());

        this.CreateMap<Group, ChannelDto>()
            .ForMember(destination => destination.Type, options => options.MapFrom(source => ChannelType.Group))
            .ForMember(destination => destination.LastMessage, options => options.MapFrom<GroupLastMessageValueResolver>())
            .ForMember(destination => destination.UnreadMessagesCount, options => options.MapFrom<GroupUnreadMessagesCountValueResolver>());

        this.CreateMap<CreateGroupCommand, Group>()
            .ForMember(destination => destination.Id, options => options.MapFrom(source => ObjectId.GenerateNewId()))
            .ForMember(destination => destination.UserIds, options => options.MapFrom<CreateGroupCommandUsersValueResolver>())
            .ForMember(destination => destination.OwnerId, options => options.MapFrom<CreateGroupCommandOwnerIdValueResolver>());

        this.CreateMap<Group, GroupDto>()
            .ForMember(destination => destination.OwnerId, options => options.MapFrom<GroupDtoOwnerIdValueResolver>())
            .ForMember(destination => destination.Members, options => options.MapFrom<GroupDtoMembersValueResolver>());

        this.CreateMap<AddUserCommand, User>()
            .ForMember(destination => destination.Id, options => options.MapFrom(source => ObjectId.GenerateNewId()))
            .ForMember(destination => destination.SourceId, options => options.MapFrom(source => source.UserId));

        this.CreateMap<User, UserDto>()
            .ForMember(destination => destination.Id, options => options.MapFrom(source => source.SourceId));

        this.CreateMap<CreateMessageCommand, Message>()
            .ForMember(destination => destination.Id, options => options.MapFrom(source => ObjectId.GenerateNewId()))
            .ForMember(destination => destination.GroupId, options => options.MapFrom(source =>
                source.ChannelType == ChannelType.Group ? source.ChannelId : null))
            .ForMember(destination => destination.ChatId, options => options.MapFrom(source =>
                source.ChannelType == ChannelType.Chat ? source.ChannelId : null))
            .ForMember(destination => destination.OwnerId, options => options.MapFrom<CreateMessageCommandOwnerIdValueResolver>());

        this.CreateMap<Message, MessageDto>()
            .ForMember(destination => destination.ChannelId, options => options.MapFrom(source => source.ChatId ?? source.GroupId))
            .ForMember(destination => destination.Owner, options => options.MapFrom<MessageOwnerIdValueResolver>())
            .ForMember(destination => destination.ReadBy, options => options.MapFrom<MessageReadByIdsValueResolver>());
    }
}