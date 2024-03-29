﻿// Copyright (c) v-demyanov. All rights reserved.

using FluentValidation;

using Folks.ChannelsService.Application.Extensions;
using Folks.ChannelsService.Domain.Common.Constants;
using Folks.ChannelsService.Infrastructure.Persistence;

namespace Folks.ChannelsService.Application.Features.Groups.Commands.CreateGroupCommand;

public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidator(ChannelsServiceDbContext dbContext)
    {
        this.RuleFor(command => command.Title)
            .NotEmpty()
            .MinimumLength(GroupSettings.TitleMinimumLength)
            .MaximumLength(GroupSettings.TitleMaximumLength);

        this.RuleFor(command => command.UserIds)
            .Must(userIds => userIds.Except(dbContext.Users
                .AsEnumerable()
                .Select(user => user.SourceId)).Count() == 0)
            .WithMessage("Some users don't exist.");

        this.RuleFor(command => command.OwnerId)
            .UserMustExist(dbContext);
    }
}
