using AutoMapper;

using MongoDB.Bson;

using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Infrastructure.Persistence;
using Folks.ChannelsService.Application.Features.Groups.Commands.CreateGroupCommand;
using Folks.ChannelsService.Application.Extensions;

namespace Folks.ChannelsService.Application.Mappings.Resolvers;

public class CreateGroupCommandUsersValueResolver : IValueResolver<CreateGroupCommand, Group, ICollection<ObjectId>>
{
    private readonly ChatServiceDbContext _dbContext;

    public CreateGroupCommandUsersValueResolver(ChatServiceDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public ICollection<ObjectId> Resolve(CreateGroupCommand source, Group destination, ICollection<ObjectId> destMember, ResolutionContext context)
    {
        var users = _dbContext.Users.GetBySourceIds(source.UserIds);

        return users.AsEnumerable().Select(user => user.Id).ToList();
    }
}