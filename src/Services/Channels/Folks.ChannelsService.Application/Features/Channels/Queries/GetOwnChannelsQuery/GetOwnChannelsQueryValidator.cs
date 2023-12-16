using FluentValidation;

using Folks.ChannelsService.Infrastructure.Persistence;

namespace Folks.ChannelsService.Application.Features.Channels.Queries.GetOwnChannelsQuery;

public class GetOwnChannelsQueryValidator : AbstractValidator<GetOwnChannelsQuery>
{
    public GetOwnChannelsQueryValidator(ChatServiceDbContext dbContext)
    {
        RuleFor(query => query.OwnerId)
            .NotEmpty();

        RuleFor(query => query.OwnerId)
            .Must(ownerId => dbContext.Users.Any(user => user.SourceId == ownerId))
            .WithMessage(query => $"The user with id=\"{query.OwnerId}\" doesn't exist.");
    }
}
