using AutoMapper;

using MongoDB.Bson;

using Folks.ChannelsService.Application.Features.Messages.Commands.CreateMessageCommand;
using Folks.ChannelsService.Domain.Entities;
using Folks.ChannelsService.Infrastructure.Persistence;
using Folks.ChannelsService.Application.Extensions;

namespace Folks.ChannelsService.Application.Mappings.Resolvers;

public class CreateMessageCommandOwnerIdValueResolver : IValueResolver<CreateMessageCommand, Message, ObjectId>
{
    private readonly ChannelsServiceDbContext _dbContext;

    public CreateMessageCommandOwnerIdValueResolver(ChannelsServiceDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public ObjectId Resolve(CreateMessageCommand source, Message destination, ObjectId destMember, ResolutionContext context)
    {
        var owner = _dbContext.Users.GetBySourceId(source.OwnerId);
        return owner.Id;
    }
}
