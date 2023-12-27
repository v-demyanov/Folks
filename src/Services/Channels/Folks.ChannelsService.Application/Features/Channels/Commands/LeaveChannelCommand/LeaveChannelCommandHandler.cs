using MediatR;

namespace Folks.ChannelsService.Application.Features.Channels.Commands.LeaveChannelCommand;

public class LeaveChannelCommandHandler : IRequestHandler<LeaveChannelCommand, LeaveChannelCommandResult>
{
    public Task<LeaveChannelCommandResult> Handle(LeaveChannelCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
