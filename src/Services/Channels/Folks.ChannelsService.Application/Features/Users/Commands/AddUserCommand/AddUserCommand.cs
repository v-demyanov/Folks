// Copyright (c) v-demyanov. All rights reserved.

using MediatR;

namespace Folks.ChannelsService.Application.Features.Users.Commands.AddUserCommand;

public class AddUserCommand : IRequest
{
    required public string UserId { get; init; }

    required public string UserName { get; init; }

    required public string Email { get; init; }
}
