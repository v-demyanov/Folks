﻿using AutoMapper;

using Folks.ChatService.Application.Features.Users.Commands;
using Folks.EventBus.Messages.IdentityService;

namespace Folks.ChatService.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddUserCommand, UserRegisteredEvent>().ReverseMap();
    }
}