using MediatR;

using Folks.ChannelsService.Infrastructure.Persistence;
using Folks.ChannelsService.Application.Extensions;

namespace Folks.ChannelsService.Application.Features.Messages.Commands.ReadMessageContentsCommand;

public class ReadMessageContentsCommandHandler : IRequestHandler<ReadMessageContentsCommand, bool>
{
    private readonly ChannelsServiceDbContext _dbContext;

    public ReadMessageContentsCommandHandler(ChannelsServiceDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public Task<bool> Handle(ReadMessageContentsCommand request, CancellationToken cancellationToken)
    {
        var messages = _dbContext.Messages
            .AsEnumerable()
            .Where(message => request.MessageIds.Any(x => x == message.Id.ToString()));
        var user = _dbContext.Users.GetBySourceId(request.UserId);

        foreach(var message in messages)
        {
            if (!message.ReadByIds.Contains(user.Id))
            {
                message.ReadByIds.Add(user.Id);
            }
        }

        _dbContext.Messages.UpdateRange(messages);
        _dbContext.SaveChanges();

        return Task.FromResult(true);
    }
}
