// Copyright (c) v-demyanov. All rights reserved.

using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Application.Features.Channels.Common.Enums;
using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Infrastructure.Persistence;

using MediatR;
using MongoDB.Bson;

namespace Folks.ChannelsService.Application.Features.Channels.Commands.LeaveChannelCommand;

public class LeaveChannelCommandHandler : IRequestHandler<LeaveChannelCommand, LeaveChannelCommandResult>
{
    private readonly ChannelsServiceDbContext dbContext;

    public LeaveChannelCommandHandler(ChannelsServiceDbContext dbContext)
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public Task<LeaveChannelCommandResult> Handle(LeaveChannelCommand request, CancellationToken cancellationToken)
    {
        return request.ChannelType switch
        {
            ChannelType.Group => Task.FromResult(this.LeaveFromGroup(request)),
            _ => throw new ArgumentOutOfRangeException(nameof(request.ChannelType)),
        };
    }

    private LeaveChannelCommandResult LeaveFromGroup(LeaveChannelCommand request)
    {
        var group = this.dbContext.Groups.GetById(ObjectId.Parse(request.ChannelId));
        var users = this.dbContext.Users.GetByGroupId(group.Id).ToList();
        var usersIds = users.Select(user => user.SourceId);
        var currentUser = this.dbContext.Users.GetBySourceId(request.UserId);
        var events = new Dictionary<LeaveChannelCommandInternalEvent, HashSet<string>>();

        this.RemoveUserFromGroup(currentUser, group);
        events.Add(LeaveChannelCommandInternalEvent.UserLeftChannel, new HashSet<string>(usersIds));
        events.Add(LeaveChannelCommandInternalEvent.ChannelRemoved, new HashSet<string> { currentUser.SourceId });

        if (group.UserIds.Any() && group.OwnerId == currentUser.Id)
        {
            this.SetNewGroupOwner(group);
            events.Add(LeaveChannelCommandInternalEvent.NewGroupOwnerSet, new HashSet<string>(usersIds));
        }

        if (group.UserIds.Count() == 0)
        {
            this.RemoveGroup(group, users);
            events = new Dictionary<LeaveChannelCommandInternalEvent, HashSet<string>>
            {
                {
                    LeaveChannelCommandInternalEvent.ChannelRemoved,
                    new HashSet<string>(usersIds)
                },
            };
        }

        this.dbContext.SaveChanges();

        return new LeaveChannelCommandSuccessResult
        {
            ChannelId = request.ChannelId,
            ChannelType = request.ChannelType,
            ChannelTitle = group.Title,
            Events = events,
        };
    }

    private void RemoveUserFromGroup(User user, Group group)
    {
        user.GroupIds.Remove(group.Id);
        this.dbContext.Users.Update(user);

        group.UserIds.Remove(user.Id);
    }

    private void SetNewGroupOwner(Group group)
    {
        var newOwnerId = group.UserIds.First();
        group.OwnerId = newOwnerId;

        this.dbContext.Groups.Update(group);
    }

    private void RemoveGroup(Group group, IEnumerable<User> users)
    {
        var messages = this.dbContext.Messages.GetByGroupId(group.Id);
        this.dbContext.Messages.RemoveRange(messages);
        this.dbContext.Groups.Remove(group);

        foreach (var user in users)
        {
            user.GroupIds.Remove(group.Id);
        }

        this.dbContext.Users.UpdateRange(users);
    }
}
