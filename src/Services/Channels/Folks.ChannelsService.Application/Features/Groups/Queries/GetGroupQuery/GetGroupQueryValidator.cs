// Copyright (c) v-demyanov. All rights reserved.

using FluentValidation;

using Folks.ChannelsService.Application.Common.Models;
using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Application.Features.Channels.Common.Enums;
using Folks.ChannelsService.Infrastructure.Persistence;

namespace Folks.ChannelsService.Application.Features.Groups.Queries.GetGroupQuery;

public class GetGroupQueryValidator : AbstractValidator<GetGroupQuery>
{
    public GetGroupQueryValidator(ChannelsServiceDbContext dbContext)
    {
        this.RuleFor(query => query.CurrentUserId)
            .NotEmpty();

        this.RuleFor(query => query.CurrentUserId)
            .UserMustExist(dbContext);

        this.RuleFor(query => query.CurrentUserId)
            .Must((query, currentUserId) => dbContext.Users
                .GetByGroupId(query.GroupId)
                .Any(user => user.SourceId == currentUserId))
            .WithMessage(query => $"User with id=\"{query.CurrentUserId}\" haven't joined this group.");

        this.RuleFor(query => query.GroupId)
            .NotEmpty();

        this.RuleFor(query => new ChannelMustExistCustomValidatorProperty
        {
            ChannelId = query.GroupId,
            ChannelType = ChannelType.Group,
        }).ChannelMustExist(dbContext);
    }
}
