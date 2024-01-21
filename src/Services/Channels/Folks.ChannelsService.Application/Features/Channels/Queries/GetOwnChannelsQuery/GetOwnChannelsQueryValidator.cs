using FluentValidation;

using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Infrastructure.Persistence;

namespace Folks.ChannelsService.Application.Features.Channels.Queries.GetOwnChannelsQuery;

public class GetOwnChannelsQueryValidator : AbstractValidator<GetOwnChannelsQuery>
{
    public GetOwnChannelsQueryValidator(ChannelsServiceDbContext dbContext)
    {
        RuleFor(query => query.OwnerId)
            .NotEmpty();

        RuleFor(query => query.OwnerId)
            .UserMustExist(dbContext);
    }
}
