﻿using AutoMapper;

using Folks.ChatService.Application.Features.Users.Dto;
using Folks.ChatService.Application.Features.Users.Commands;
using Folks.ChatService.Domain.Entities;
using Folks.ChatService.Application.Features.Channels.Dto;

namespace Folks.ChatService.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Chat, ChannelDto>()
            .ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id.ToString()))
            .ForMember(destination => destination.Type, options => options.MapFrom(source => ChannelType.Chat))
            .ReverseMap();
        CreateMap<Group, ChannelDto>()
            .ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id.ToString()))
            .ForMember(destination => destination.Type, options => options.MapFrom(source => ChannelType.Group))
            .ReverseMap();
        CreateMap<User, UserDto>()
            .ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id.ToString()))
            .ReverseMap();
        CreateMap<User, AddUserCommand>()
            .ForMember(destination => destination.UserId, options => options.MapFrom(source => source.SourceId))
            .ReverseMap();
    }
}