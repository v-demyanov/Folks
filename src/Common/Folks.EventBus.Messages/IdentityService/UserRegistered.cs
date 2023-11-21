namespace Folks.EventBus.Messages.IdentityService;

public record UserRegistered
{
    public required string UserId { get; init; }

    public required string UserName { get; init; }

    public required string Email { get; init; }
}
