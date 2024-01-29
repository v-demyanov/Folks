// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Infrastructure.Persistence;

using MediatR;

namespace Folks.ChannelsService.Application.Features.Messages.Commands.ReadMessageContentsCommand;

public class ReadMessageContentsCommandHandler : IRequestHandler<ReadMessageContentsCommand, bool>
{
    private readonly ChannelsServiceDbContext dbContext;

    public ReadMessageContentsCommandHandler(ChannelsServiceDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public Task<bool> Handle(ReadMessageContentsCommand request, CancellationToken cancellationToken)
    {
        var messages = this.dbContext.Messages
            .AsEnumerable()
            .Where(message => request.MessageIds.Any(x => x == message.Id.ToString()));
        var user = this.dbContext.Users.GetBySourceId(request.UserId);

        foreach (var message in messages)
        {
            if (!message.ReadByIds.Contains(user.Id))
            {
                message.ReadByIds.Add(user.Id);
            }
        }

        this.dbContext.Messages.UpdateRange(messages);
        this.dbContext.SaveChanges();

        return Task.FromResult(true);
    }
}
