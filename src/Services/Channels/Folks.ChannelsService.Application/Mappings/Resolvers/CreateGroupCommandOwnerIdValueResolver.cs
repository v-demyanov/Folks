using AutoMapper;

using MongoDB.Bson;

using Folks.ChannelsService.Application.Features.Groups.Commands.CreateGroupCommand;
using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Infrastructure.Persistence;
using Folks.ChannelsService.Application.Extensions;

namespace Folks.ChannelsService.Application.Mappings.Resolvers;

internal class CreateGroupCommandOwnerIdValueResolver : IValueResolver<CreateGroupCommand, Group, ObjectId>
{
    private readonly ChannelsServiceDbContext _dbContext;

    public CreateGroupCommandOwnerIdValueResolver(ChannelsServiceDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public ObjectId Resolve(CreateGroupCommand source, Group destination, ObjectId destMember, ResolutionContext context)
    {
        var user = _dbContext.Users.GetBySourceId(source.OwnerId);
        return user.Id;
    }
}
