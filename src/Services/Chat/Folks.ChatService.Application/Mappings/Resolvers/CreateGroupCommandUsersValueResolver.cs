using AutoMapper;

using MongoDB.Bson;

using Folks.ChatService.Domain.Entities;
using Folks.ChatService.Infrastructure.Persistence;
using Folks.ChatService.Application.Features.Groups.Commands.CreateGroupCommand;

namespace Folks.ChatService.Application.Mappings.Resolvers;

public class CreateGroupCommandUsersValueResolver : IValueResolver<CreateGroupCommand, Group, ICollection<ObjectId>>
{
    private readonly ChatServiceDbContext _dbContext;

    public CreateGroupCommandUsersValueResolver(ChatServiceDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public ICollection<ObjectId> Resolve(CreateGroupCommand source, Group destination, ICollection<ObjectId> destMember, ResolutionContext context)
    {
        var users = _dbContext.Users
            .Where(user => source.UserIds.Any(userId => userId == user.SourceId))
            .ToList();

        return users.Select(user => user.Id).ToList();
    }
}
