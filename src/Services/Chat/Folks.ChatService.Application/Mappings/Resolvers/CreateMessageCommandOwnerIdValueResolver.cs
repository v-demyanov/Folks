using AutoMapper;

using MongoDB.Bson;

using Folks.ChatService.Application.Features.Messages.Commands.CreateMessageCommand;
using Folks.ChatService.Domain.Entities;
using Folks.ChatService.Infrastructure.Persistence;
using Folks.ChatService.Application.Extensions;

namespace Folks.ChatService.Application.Mappings.Resolvers;

public class CreateMessageCommandOwnerIdValueResolver : IValueResolver<CreateMessageCommand, Message, ObjectId>
{
    private readonly ChatServiceDbContext _dbContext;

    public CreateMessageCommandOwnerIdValueResolver(ChatServiceDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public ObjectId Resolve(CreateMessageCommand source, Message destination, ObjectId destMember, ResolutionContext context)
    {
        var owner = _dbContext.Users.GetBySourceId(source.OwnerId);
        return owner.Id;
    }
}
