using MediatR;

namespace Folks.ChatService.Application.Features.Users.Commands;

public class AddUserCommand : IRequest
{
    public required string UserId { get; init; }

    public required string UserName { get; init; }

    public required string Email { get; init; }
}
