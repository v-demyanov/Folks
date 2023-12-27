using MediatR;

using MongoDB.Bson;

using Folks.ChannelsService.Application.Features.Channels.Common.Enums;
using Folks.ChannelsService.Infrastructure.Persistence;
using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Domain.Entities;

namespace Folks.ChannelsService.Application.Features.Channels.Commands.LeaveChannelCommand;

public class LeaveChannelCommandHandler : IRequestHandler<LeaveChannelCommand, LeaveChannelCommandResult>
{
    private readonly ChannelsServiceDbContext _dbContext;

    public LeaveChannelCommandHandler(ChannelsServiceDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public Task<LeaveChannelCommandResult> Handle(LeaveChannelCommand request, CancellationToken cancellationToken)
    {
        return request.ChannelType switch
        {
            ChannelType.Group => Task.FromResult(LeaveFromGroup(request)),
            _ => throw new ArgumentOutOfRangeException(nameof(request.ChannelType)),
        };
    }

    private LeaveChannelCommandResult LeaveFromGroup(LeaveChannelCommand request)
    {
        var group = _dbContext.Groups.GetById(ObjectId.Parse(request.ChannelId));
        var users = _dbContext.Users.GetByGroupId(group.Id);
        var currentUser = _dbContext.Users.GetBySourceId(request.UserId);
        var events = new List<LeaveChannelCommandInternalEvent>();

        RemoveUserFromGroup(currentUser, group);
        events.Add(LeaveChannelCommandInternalEvent.UserLeft);

        if (group.UserIds.Any() && group.OwnerId == currentUser.Id)
        {
            SetNewGroupOwner(group);
            events.Add(LeaveChannelCommandInternalEvent.NewGroupOwnerSet);
        }

        if (group.UserIds.Count() == 0)
        {
            RemoveGroup(group, users);
            events = new List<LeaveChannelCommandInternalEvent> 
            { 
                LeaveChannelCommandInternalEvent.ChannelRemoved 
            };
        }

        _dbContext.SaveChanges();

        return new LeaveChannelCommandSuccessResult
        {
            ChannelId = request.ChannelId,
            ChannelType = request.ChannelType,
            ChannelTitle = group.Title,
            Events = events,
            Recipients = users.Select(user => user.SourceId),
        };
    }

    private void RemoveUserFromGroup(User user, Group group)
    {
        user.GroupIds.Remove(group.Id);
        _dbContext.Users.Update(user);

        group.UserIds.Remove(user.Id);
    }

    private void SetNewGroupOwner(Group group)
    {
        var newOwnerId = group.UserIds.First();
        group.OwnerId = newOwnerId;

        _dbContext.Groups.Update(group);
    }

    private void RemoveGroup(Group group, IEnumerable<User> users)
    {
        var messages = _dbContext.Messages.GetByGroupId(group.Id);
        _dbContext.Messages.RemoveRange(messages);
        _dbContext.Groups.Remove(group);

        foreach (var user in users)
        {
            user.GroupIds.Remove(group.Id);
        }
        _dbContext.Users.UpdateRange(users);
    }
}
