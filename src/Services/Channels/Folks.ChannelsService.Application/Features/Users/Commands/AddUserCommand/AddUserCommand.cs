using MediatR;

namespace Folks.ChannelsService.Application.Features.Users.Commands.AddUserCommand;

public class AddUserCommand : IRequest
{
    public required string UserId { get; init; }

    public required string UserName { get; init; }

    public required string Email { get; init; }
}
