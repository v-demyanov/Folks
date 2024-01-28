using MediatR;

namespace Folks.ChannelsService.Application.Common.Contracts;

public abstract class Notification : INotification
{
    public required IEnumerable<string> Recipients { get; init; }
}
