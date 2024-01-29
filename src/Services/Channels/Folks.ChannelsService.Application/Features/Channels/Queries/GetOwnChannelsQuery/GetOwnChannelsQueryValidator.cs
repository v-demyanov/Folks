// Copyright (c) v-demyanov. All rights reserved.

using FluentValidation;

using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Infrastructure.Persistence;

namespace Folks.ChannelsService.Application.Features.Channels.Queries.GetOwnChannelsQuery;

public class GetOwnChannelsQueryValidator : AbstractValidator<GetOwnChannelsQuery>
{
    public GetOwnChannelsQueryValidator(ChannelsServiceDbContext dbContext)
    {
        this.RuleFor(query => query.OwnerId)
            .NotEmpty();

        this.RuleFor(query => query.OwnerId)
            .UserMustExist(dbContext);
    }
}
